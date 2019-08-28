using Mvc.DynamicMenu.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mvc.DynamicMenu.Services
{
	public interface IDataAccessService
	{
		Task<List<NavigationMenuViewModel>> GetMenuItemsAsync(ClaimsPrincipal principal);
	}
}