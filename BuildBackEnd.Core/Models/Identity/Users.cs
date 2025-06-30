using Microsoft.AspNetCore.Identity;

namespace BuildBackEnd.Core.Models
{
    public class Users : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
