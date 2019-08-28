using Microsoft.AspNetCore.Mvc;
using Mvc.DynamicMenu.Services;
using System.Threading.Tasks;

namespace Mvc.DynamicMenu.ViewComponents
{
	public class NavigationMenuViewComponent : ViewComponent
	{
		private readonly IDataAccessService _dataAccessService;

		public NavigationMenuViewComponent(IDataAccessService dataAccessService)
		{
			_dataAccessService = dataAccessService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var items = await _dataAccessService.GetMenuItemsAsync(HttpContext.User);

			return View(items);
		}
	}
}