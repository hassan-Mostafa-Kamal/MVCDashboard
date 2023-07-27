using DmoPL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DmoPL.Controllers
{
	public class RoleController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public RoleController(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}
		public async Task<IActionResult> Index(string name) // action for main page of Employee
		{
			if (string.IsNullOrEmpty(name))
			{
				var roles = await _roleManager.Roles.Select(R => new RoleViewModel()
				{
					Id = R.Id,
					RoleName = R.Name

				}).ToListAsync();
				return View(roles);
			}
			else
			{
				var roles = await _roleManager.FindByNameAsync(name);
				var RoleView = new RoleViewModel()
				{
					Id = roles.Id,
					RoleName = roles.Name
				};
				return View(new List<RoleViewModel>() { RoleView });

			}

		}

		public async Task<IActionResult> Details(string id, string ViewName = "Details") // action for Details of Employee
		{
			if (id is null)
			{
				return BadRequest();
			}
			var role = await _roleManager.FindByIdAsync(id);
			if (role is null)
				return NotFound();
			var userVm = new RoleViewModel()
			{
				Id = role.Id,
				RoleName = role.Name,

			};
			return View(ViewName, userVm);
		}


		public async Task<IActionResult> Edit(string id) // action for Update Employee
		{

			return await Details(id, "Edit");

		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel UpdatedRole) // action for Update Employee using post method
		{
			if (id != UpdatedRole.Id) return BadRequest();
			if (ModelState.IsValid)
			{
				try
				{
					var mappedUser = await _roleManager.FindByIdAsync(id);
					mappedUser.Name = UpdatedRole.RoleName;
					var result = await _roleManager.UpdateAsync(mappedUser);
					return RedirectToAction(nameof(Index));

				}
				catch (Exception Ex)
				{
					ModelState.AddModelError(string.Empty, Ex.Message);
				}
			}
			return View(UpdatedRole);

		}

		public IActionResult Create()
		{

			return View();

		}
		[HttpPost]
		public async Task<IActionResult> Create(RoleViewModel model)
		{
			if (ModelState.IsValid)
			{
				var role = new IdentityRole()
				{
					Name = model.RoleName
				};
				await _roleManager.CreateAsync(role);
				return RedirectToAction(nameof(Index));
			}
			return View(model);

		}
		public async Task<IActionResult> Delete(string id)
		{

			return await Details(id, "Delete");

		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]

		public async Task<IActionResult> Delete([FromRoute] string id, RoleViewModel DeletedRole) // action for Update Employee
		{
			if (id != DeletedRole.Id) return BadRequest();
			try
			{
				var mappedRole = await _roleManager.FindByIdAsync(id);

				var result = await _roleManager.DeleteAsync(mappedRole);
				return RedirectToAction(nameof(Index));

			}
			catch (Exception Ex)
			{
				ModelState.AddModelError(string.Empty, Ex.Message);
			}
			return View(DeletedRole);


		}

	}
}
