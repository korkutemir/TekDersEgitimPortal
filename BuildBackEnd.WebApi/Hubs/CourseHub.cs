using Microsoft.AspNetCore.SignalR;

namespace BuildBackEnd.WebApi.Hubs
{
    public class CourseHub : Hub
    {
        public async Task SendRegistration(string studentName, string courseName)
        {
            await Clients.All.SendAsync("ReceiveStudentRegistration", studentName, courseName);
        }
    }
}