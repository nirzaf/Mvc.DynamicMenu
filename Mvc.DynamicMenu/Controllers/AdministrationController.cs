using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mvc.DynamicMenu.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mvc.DynamicMenu.Controllers
{
	public class AdministrationController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ILogger<AdministrationController> _logger;

		public AdministrationController(
				UserManager<IdentityUser> userManager,
				RoleManager<IdentityRole> roleManager,
				ILogger<AdministrationController> logger)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_logger = logger;
		}

		public async Task<IActionResult> Roles()
		{
			var roleViewModel = new List<RoleViewModel>();

			try
			{
				var roles = await _roleManager.Roles.ToListAsync();
				foreach (var item in roles)
				{
					roleViewModel.Add(new RoleViewModel()
					{
						Id = item.Id,
						Name = item.Name,
					});
				}
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, ex.GetBaseException().Message);
			}

			return View(roleViewModel);
		}

		public async Task<IActionResult> Users()
		{
			var userViewModel = new List<UserViewModel>();

			try
			{
				var users = await _userManager.Users.ToListAsync();
				foreach (var item in users)
				{
					userViewModel.Add(new UserViewModel()
					{
						Id = item.Id,
						Email = item.Email,
						UserName = item.UserName,
					});
				}
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, ex.GetBaseException().Message);
			}

			return View(userViewModel);
		}
	}
}