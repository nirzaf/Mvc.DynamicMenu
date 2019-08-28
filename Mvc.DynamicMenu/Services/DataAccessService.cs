using Microsoft.EntityFrameworkCore;
using Mvc.DynamicMenu.Data;
using Mvc.DynamicMenu.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mvc.DynamicMenu.Services
{
	public class DataAccessService : IDataAccessService
	{
		private readonly ApplicationDbContext _context;

		public DataAccessService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<NavigationMenuViewModel>> GetMenuItemsAsync(ClaimsPrincipal principal)
		{
			var isAuthenticated = principal.Identity.IsAuthenticated;
			if (!isAuthenticated)
				return new List<NavigationMenuViewModel>();

			var roleIds = await GetUserRoleIds(principal);
			var data = await (from menu in _context.RoleMenuPermission
							  where roleIds.Contains(menu.RoleId)
							  select menu)
							  .Select(m => new NavigationMenuViewModel()
							  {
								  Id = m.NavigationMenu.Id,
								  Name = m.NavigationMenu.Name,
								  ActionName = m.NavigationMenu.ActionName,
								  ControllerName = m.NavigationMenu.ControllerName,
								  ParentMenuId = m.NavigationMenu.ParentMenuId,
							  }).Distinct().ToListAsync();

			return data;
		}

		private async Task<List<string>> GetUserRoleIds(ClaimsPrincipal ctx)
		{
			var userId = GetUserId(ctx);
			var data = await (from role in _context.UserRoles
							  where role.UserId == userId
							  select role.RoleId).ToListAsync();

			return data;
		}

		private string GetUserId(ClaimsPrincipal user)
		{
			return ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}
	}
}