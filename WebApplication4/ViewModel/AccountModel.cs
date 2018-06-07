using Model.Models;

namespace WebApplication4.ViewModel
{
    public class CreateAccountModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }

    public class UpdateAccountModel : CreateAccountModel
    {
        public string Id { get; set; }
    }

    public class AccountModel
    {
        public string username { get; set; }
        public string email { get; set; }
        public string Id { get; set; }

        public AccountModel()
        {
        }

        public AccountModel(Accounts account)
        {
            this.email = account.Email;
            this.username = account.UserName;
            this.Id = account.Id;
        }
    }
}