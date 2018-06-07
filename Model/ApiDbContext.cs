using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Model.Models;
using System.Data.Entity;

namespace Model
{
    public class ApiDbContext : IdentityDbContext
    {
        public ApiDbContext() : base("ApiConnection")
        {
        }

        static ApiDbContext()
        {
            Database.SetInitializer<ApiDbContext>(new IdentityDbInit());
        }

        public static ApiDbContext Create()
        {
            return new ApiDbContext();
        }

        public DbSet<Classes> Classes { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Students> Students { get; set; }


        public override int SaveChanges()
        {
            //

            return base.SaveChanges();
        }
    }

    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<ApiDbContext>
    {
        protected override void Seed(ApiDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        public void PerformInitialSetup(ApiDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<Accounts>(new UserStore<Accounts>(context));

            roleManager.Create(new IdentityRole()
            {
                Name = "Admin"
            });

            roleManager.Create(new IdentityRole()
            {
                Name = "User"
            });

            Accounts account = new Accounts()
            {
                UserName = "admin",
                Email = "admin@test.com",
                EmailConfirmed = true
            };

            userManager.Create(account, "Chitestthoi0pASS:D");

            userManager.AddToRole(account.Id, "Admin");
        }
    }
}