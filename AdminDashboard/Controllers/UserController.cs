using AdminDashboard.Models.Users;
using AdminDashboard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreCore.G02.Entites.Identity;

namespace AdminDashboard.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<Appuser> _usermanager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserController(UserManager<Appuser> usermanager,RoleManager<IdentityRole> roleManager)
		{
			_usermanager = usermanager;
			_roleManager = roleManager;
		}
		public async Task<IActionResult> Index()
		{
			var users = await _usermanager.Users.Select(u=>new UserViewModel()
			{
				Id = u.Id,
				UserName = u.UserName ?? "Invaild User Name",
				DisplayName = u.DisplayName ?? "invaild Display Name",
				PhoneNumber = u.PhoneNumber ??"Invaild Phone Number !",
				Email = u.Email ?? "Invaild Email!!",
				Roles = _usermanager.GetRolesAsync(u).Result
				
			}).ToListAsync();
			return View(users);
		}
		public async Task<IActionResult> Edit(string id)
		{
			var user = await _usermanager.FindByIdAsync(id);
			var allroles = await _roleManager.Roles.ToListAsync();
			var viewModel = new UserRoleViewModel()
			{
				UserId = user?.Id ?? "Invaild UserId",
				UserName = user?.UserName??"Invaild User Name",
				Roles = allroles.Select(r=>new RoleViewModel() 
				{
					Id = r.Id,
					Name = r?.Name ?? "Invaild Role Name",
					IsSelected = _usermanager.IsInRoleAsync(user,r?.Name ?? "Invaild Role Name").Result
				}).ToList()
			};
			return View(viewModel);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(string id,UserRoleViewModel model)
		{
			var user = await _usermanager.FindByIdAsync(model.UserId);
			var userRoles = await _usermanager.GetRolesAsync(user);
			foreach(var role in model.Roles)
			{
				if(userRoles.Any(r=>r == role.Name)&& !role.IsSelected)
				{
					await  _usermanager.RemoveFromRoleAsync(user, role.Name);
				}
				if (!userRoles.Any(r => r == role.Name) && role.IsSelected)
				{
					await _usermanager.AddToRoleAsync(user, role.Name);
				}
			}
			return RedirectToAction("Index");
		}
	}
}
