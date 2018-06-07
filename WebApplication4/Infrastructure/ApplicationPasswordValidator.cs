using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Infrastructure
{
	public class ApplicationPasswordValidator : PasswordValidator
	{
		public string[] WeakPasswords = new string[] { };
		public override async Task<IdentityResult> ValidateAsync(string pass)
		{
			IdentityResult result = await base.ValidateAsync(pass);
			int i = WeakPasswords.Count();

			if (WeakPasswords.Contains(pass))
			{
				var errors = result.Errors.ToList(); errors.Add("Weak Passwords");
				result = new IdentityResult(errors);
			}
			return result;
		}
	}
}