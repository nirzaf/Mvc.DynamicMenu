using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Mvc.DynamicMenu.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mvc.DynamicMenu
{
	public static class DbInitializer
	{
		public static void Initialize(IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices.CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
				context.Database.EnsureCreated();

				var _userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
				var _roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				if (!context.Users.Any(usr => usr.UserName == "admin@test.com"))
				{
					var user = new IdentityUser()
					{
						UserName = "admin@test.com",
						Email = "admin@test.com",
						EmailConfirmed = true,
					};

					var userResult = _userManager.CreateAsync(user, "P@ssw0rd").Result;
				}

				if (!context.Users.Any(usr => usr.UserName == "manager@test.com"))
				{
					var user = new IdentityUser()
					{
						UserName = "manager@test.com",
						Email = "manager@test.com",
						EmailConfirmed = true,
					};

					var userResult = _userManager.CreateAsync(user, "P@ssw0rd").Result;
				}

				if (!context.Users.Any(usr => usr.UserName == "employee@test.com"))
				{
					var user = new IdentityUser()
					{
						UserName = "employee@test.com",
						Email = "employee@test.com",
						EmailConfirmed = true,
					};

					var userResult = _userManager.CreateAsync(user, "P@ssw0rd").Result;
				}

				if (!_roleManager.RoleExistsAsync("Admin").Result)
				{
					var role = _roleManager.CreateAsync(new IdentityRole { Name = "Admin" }).Result;
				}

				if (!_roleManager.RoleExistsAsync("Manager").Result)
				{
					var role = _roleManager.CreateAsync(new IdentityRole { Name = "Manager" }).Result;
				}

				if (!_roleManager.RoleExistsAsync("Employee").Result)
				{
					var role = _roleManager.CreateAsync(new IdentityRole { Name = "Employee" }).Result;
				}

				var adminUser = _userManager.FindByNameAsync("admin@test.com").Result;
				var adminRole = _userManager.AddToRolesAsync(adminUser, new string[] { "Admin" }).Result;

				var managerUser = _userManager.FindByNameAsync("manager@test.com").Result;
				var managerRole = _userManager.AddToRolesAsync(managerUser, new string[] { "Manager" }).Result;

				var employeeUser = _userManager.FindByNameAsync("employee@test.com").Result;
				var userRole = _userManager.AddToRolesAsync(employeeUser, new string[] { "Employee" }).Result;

				var permissions = GetPermissions();
				foreach (var item in permissions)
				{
					if (!context.NavigationMenu.Any(n => n.Name == item.Name))
					{
						context.NavigationMenu.Add(item);
					}
				}

				var _adminRole = _roleManager.Roles.Where(x => x.Name == "Admin").FirstOrDefault();
				var _managerRole = _roleManager.Roles.Where(x => x.Name == "Manager").FirstOrDefault();
				var _employeeRole = _roleManager.Roles.Where(x => x.Name == "Employee").FirstOrDefault();

				context.RoleMenuPermission.Add(new RoleMenuPermission() { RoleId = _adminRole.Id, NavigationMenuId = new Guid("13e2f21a-4283-4ff8-bb7a-096e7b89e0f0") });
				context.RoleMenuPermission.Add(new RoleMenuPermission() { RoleId = _adminRole.Id, NavigationMenuId = new Guid("283264d6-0e5e-48fe-9d6e-b1599aa0892c") });
				context.RoleMenuPermission.Add(new RoleMenuPermission() { RoleId = _adminRole.Id, NavigationMenuId = new Guid("7cd0d373-c57d-4c70-aa8c-22791983fe1c") });

				context.RoleMenuPermission.Add(new RoleMenuPermission() { RoleId = _managerRole.Id, NavigationMenuId = new Guid("13e2f21a-4283-4ff8-bb7a-096e7b89e0f0") });
				context.RoleMenuPermission.Add(new RoleMenuPermission() { RoleId = _managerRole.Id, NavigationMenuId = new Guid("283264d6-0e5e-48fe-9d6e-b1599aa0892c") });

				context.SaveChanges();
			}
		}

		private static List<NavigationMenu> GetPermissions()
		{
			return new List<NavigationMenu>()
			{
				new NavigationMenu()
				{
					Id = new Guid("13e2f21a-4283-4ff8-bb7a-096e7b89e0f0"),
					Name = "Admin",
					ControllerName = "",
					ActionName = "",
					ParentMenuId = null,
				},

				new NavigationMenu()
				{
					Id = new Guid("283264d6-0e5e-48fe-9d6e-b1599aa0892c"),
					Name = "Roles",
					ControllerName = "Administration",
					ActionName = "Roles",
					ParentMenuId = new Guid("13e2f21a-4283-4ff8-bb7a-096e7b89e0f0"),
				},

				new NavigationMenu()
				{
					Id = new Guid("7cd0d373-c57d-4c70-aa8c-22791983fe1c"),
					Name = "Users",
					ControllerName = "Administration",
					ActionName = "Users",
					ParentMenuId = new Guid("13e2f21a-4283-4ff8-bb7a-096e7b89e0f0"),
				}
			};
		}
	}
}