using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc.DynamicMenu.Data
{
	[Table(name: "AspNetRoleMenuPermission")]
	public class RoleMenuPermission
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[ForeignKey("ApplicationRole")]
		public string RoleId { get; set; }

		[ForeignKey("NavigationMenu")]
		public Guid NavigationMenuId { get; set; }

		public NavigationMenu NavigationMenu { get; set; }
	}
}