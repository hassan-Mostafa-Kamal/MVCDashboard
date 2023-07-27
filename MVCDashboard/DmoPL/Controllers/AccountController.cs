using DemoDAL.Models;
using DmoPL.Helper;
using DmoPL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Threading.Tasks;

namespace DmoPL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser>userManager,SignInManager<ApplicationUser>signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		#region Register
		public IActionResult Rigster()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Rigster(RegisterViewModel model)
		{
			if(ModelState.IsValid)
			{
				var AUser = new ApplicationUser()
				{
					FName= model.FName,
					LName= model.LName,
					UserName = model.Email.Split('@')[0],
					Email = model.Email,
					IsAgree=model.IsAgree,
				};
				var result=await _userManager.CreateAsync(AUser, model.Password);
				if (result.Succeeded)
					return RedirectToAction(nameof(Login));
				foreach (var Err in result.Errors)
				{
					ModelState.AddModelError(string.Empty, Err.Description);
				}
			}
			return View(model);
		}
		#endregion

		#region Login
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var AUser = await _userManager.FindByEmailAsync(model.Email);
				if(AUser is not null)
				{
					var Flag = await _userManager.CheckPasswordAsync(AUser, model.Password);
					if (Flag == true)
					{
						var result = await _signInManager.PasswordSignInAsync(AUser, model.Password, model.RememberMe, false);
						if (result.Succeeded) { return RedirectToAction("Index", "Home"); }
					}
				}
				ModelState.AddModelError(string.Empty, "Email Or Password Is Invalid");
			}
			return View(model);
		}
		#endregion

		#region SignOut
		public new async Task<IActionResult> SignOut()
		{
		 await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
		#endregion

		#region ForgetPassword
		public IActionResult ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var AUser = await _userManager.FindByEmailAsync(model.Email);
				if (AUser is not null)
				{
					var token=await _userManager.GeneratePasswordResetTokenAsync(AUser);
					var PasswordResetLink=Url.Action("ResetPassword","Account",new {email=model.Email,token=token}, Request.Scheme);
					var email = new Email()
					{
						Subject = "Reset Password",
						To = model.Email,
						Body = PasswordResetLink
					};
					EmailSettings.SendEmail(email);
					return RedirectToAction(nameof(CheckYourInbox));
				}
				ModelState.AddModelError(string.Empty, "Email Is Not Exist");
			}
			return View(model);
		}

		public IActionResult CheckYourInbox()
		{
			return View();
		}
		#endregion

		#region Reset Password
		public IActionResult ResetPassword(string email,string token)
		{
			TempData["email"]=email;
			TempData["token"] = token;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				string email = TempData["email"] as string;
				string token = TempData["token"] as string;
				var user = await _userManager.FindByEmailAsync(email);
				var res = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
				if (res.Succeeded)
					return RedirectToAction(nameof(Login));
				foreach (var Err in res.Errors)
				{
					ModelState.AddModelError(string.Empty, Err.Description);
				}
			}
			return View(model);
		}
		#endregion
	}
}
