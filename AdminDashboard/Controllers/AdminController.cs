using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreCore.G02.Dto.Autho;
using StoreCore.G02.Entites.Identity;

namespace AdminDashboard.Controllers
{
	public class AdminController : Controller
	{
		private readonly UserManager<Appuser> _userManager;
		private readonly SignInManager<Appuser> _signInManager;

		public AdminController(UserManager<Appuser> userManager,SignInManager<Appuser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}
        public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginDto model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user is null)
			{
				ModelState.AddModelError("Email", "InVaild!!");
				return RedirectToAction("Login");
			}
			var Result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
			if (!Result.Succeeded || !await _userManager.IsInRoleAsync(user, "Admin"))
			{
				ModelState.AddModelError(string.Empty, "You are not Authorize");
				return RedirectToAction("Login");
			}

			return RedirectToAction("Index", "Home");

		}
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Login");
		}
    }
}
