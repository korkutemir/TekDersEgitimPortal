using BuildBackEnd.Core.Models.Bridges;
using BuildBackEnd.Core.Services;
using BuildBackEnd.WebApi.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BuildBackEnd.WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SignalController : Controller
    {

        private readonly ICourseService _courseService;
        private readonly IUserCourseBridgeService _userCourseBridgeService;
        private readonly IHubContext<CourseHub> _hubContext;
        private readonly IMemberService _memberService;

        private IResponseService _responseService;

        public SignalController(ICourseService courseService, IUserCourseBridgeService userCourseBridgeService, IHubContext<CourseHub> hubContext, IResponseService responseService, IMemberService memberService)
        {
            _courseService = courseService;
            _userCourseBridgeService = userCourseBridgeService;
            _hubContext = hubContext;
            _responseService = responseService;
            _memberService = memberService;
        }


        [HttpPost]
        public async Task<IActionResult> UserRegisterToCourse([FromBody] UserCourseRequest request)
        {
            var data = await _courseService.GetByIdAsync(request.CourseId);
            var user = await _memberService.FindByUserIdAsync(request.UserId);
            UserCourseBridge userCourseBridge = new UserCourseBridge();
            userCourseBridge.CourseId = request.CourseId;
            userCourseBridge.UserId = request.UserId;
            await _userCourseBridgeService.AddAsync(userCourseBridge);

            await _hubContext.Clients.All.SendAsync("ReceiveStudentRegistration", user.Name + " " + user.Surname, data.Name);

            return Ok(_responseService.HandleSuccess("Kayıt olundu"));
        }

        public class UserCourseRequest
        {
            public int CourseId { get; set; }
            public int UserId { get; set; }
        }


    }
}
