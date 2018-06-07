using Microsoft.AspNet.Identity;
using Model;
using Model.Models;
using System.Linq;
using System.Web.Http;
using WebApplication4.Infrastructure;
using WebApplication4.ViewModel;

namespace WebApplication4.Controllers
{
    public class AccountsController : ApiController
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private ApiDbContext db;

        public AccountsController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            db = new ApiDbContext();
        }

        [HttpPost]
        public IHttpActionResult Create(CreateAccountModel acc)
        {
            IHttpActionResult httpActionResult;

            Accounts account = new Accounts()
            {
                UserName = acc.username,
                Email = acc.email,
            };

            IdentityResult result = _userManager.Create(account, acc.password);

            /*
             *
             */
            if (!result.Succeeded)
            {
                ErrorsModel error = new ErrorsModel();

                error.Errors = result.Errors.ToList();

                httpActionResult = new ErrorActionResult(Request, System.Net.HttpStatusCode.BadRequest, error);
            }
            else
            {
                var result_2 = _userManager.AddToRole(account.Id, "User");

                if (!result_2.Succeeded)
                {
                    ErrorsModel error = new ErrorsModel();

                    error.Errors = result.Errors.ToList();

                    httpActionResult = new ErrorActionResult(Request, System.Net.HttpStatusCode.BadRequest, error);
                }
                else
                {
                    AccountModel accountModel = new AccountModel(account);

                    httpActionResult = Ok(accountModel);
                }
            }

            return httpActionResult;
        }
    }
}