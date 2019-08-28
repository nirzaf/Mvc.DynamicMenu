using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mvc.DynamicMenu.Models;

namespace Mvc.DynamicMenu.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}
		
		public DbSet<RoleMenuPermission> RoleMenuPermission { get; set; }

		public DbSet<NavigationMenu> NavigationMenu { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			//builder.Entity<IdentityRole>().ToTable("MyRoles");
			//builder.Entity<IdentityUser>(entity => 
			//{
			//	entity.ToTable("Users");
			//	entity.Property(p => p.Id).HasColumnName("UserId");
			//});
		}

		public DbSet<Mvc.DynamicMenu.Models.NavigationMenuViewModel> NavigationMenuViewModel { get; set; }

		public DbSet<Mvc.DynamicMenu.Models.UserViewModel> UserViewModel { get; set; }
	}
}