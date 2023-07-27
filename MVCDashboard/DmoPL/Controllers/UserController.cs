using AutoMapper;
using DemoDAL.Models;
using DmoPL.Helper;
using DmoPL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DmoPL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IMapper _mapper;

		public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_mapper = mapper;
		}
		public async Task<IActionResult> Index(string email) // action for main page of Employee
		{
			if (string.IsNullOrEmpty(email))
			{
				var users = await _userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					FName = U.FName,
					LName = U.LName,
					Email = U.Email,
					PhoneNumber = U.PhoneNumber,
					Roles = _userManager.GetRolesAsync(U).Result
				}).ToListAsync();
				return View(users);
			}
			else
			{
				var user = await _userManager.FindByEmailAsync(email);
				var usersView = new UserViewModel()
				{
					Id = user.Id,
					FName = user.FName,
					LName = user.LName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber,
					Roles = _userManager.GetRolesAsync(user).Result
				};
				return View(new List<UserViewModel>() { usersView });

			}

		}

		public async Task<IActionResult> Details(string id, string ViewName = "Details") // action for Details of Employee
		{
			if (id is null)
			{
				return BadRequest();
			}
			var user = await _userManager.FindByIdAsync(id);
			if (user is null)
				return NotFound();
			var userVm = new UserViewModel()
			{
				Id = user.Id,
				FName = user.FName,
				LName = user.LName,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				Roles = _userManager.GetRolesAsync(user).Result
			};
			return View(ViewName, userVm);
		}


		public async Task<IActionResult> Edit(string id) // action for Update Employee
		{

			return await Details(id, "Edit");

		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel Updateduser) // action for Update Employee using post method
		{
			if (id != Updateduser.Id) return BadRequest();
			if (ModelState.IsValid)
			{
				try
				{
					var mappedUser = await _userManager.FindByIdAsync(id);
					mappedUser.FName = Updateduser.FName;
					mappedUser.LName= Updateduser.LName;
					mappedUser.PhoneNumber= Updateduser.PhoneNumber;
					var result= await _userManager.UpdateAsync(mappedUser);
					return RedirectToAction(nameof(Index));

				}
				catch (Exception Ex)
				{
					ModelState.AddModelError(string.Empty, Ex.Message);
				}
			}
			return View(Updateduser);

		}

		public async Task<IActionResult> Delete(string id) 
		{

			return await Details(id, "Delete");

		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]

		public async Task<IActionResult> Delete([FromRoute] string id, UserViewModel DeletedUser) // action for Update Employee
		{
			if (id != DeletedUser.Id) return BadRequest();
			try
			{
				var mappedUser = await _userManager.FindByIdAsync(id);
				
				var result = await _userManager.DeleteAsync(mappedUser);
				return RedirectToAction(nameof(Index));

			}
			catch (Exception Ex)
			{
				ModelState.AddModelError(string.Empty, Ex.Message);
			}
			return View(DeletedUser);
 

		}
	}
}
