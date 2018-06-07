using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Model.Models
{
    public class Accounts : IdentityUser
    {
        public Accounts()
            : base()
        {
            Avatar = FullName = "";
        }

        public Accounts(string userName) : base(userName)
        {
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Accounts> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public bool IsRoot { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Role { get; set; }
    }
}