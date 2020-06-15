using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ofsen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofsen
{
	public static class Kurulum
	{
		private static UserManager<AppUsers> _userManager;
		private static RoleManager<AppRole> _roleManager;
		private static IConfiguration _Configuration;

		public static void Kur(UserManager<AppUsers> userManager, RoleManager<AppRole> roleManager, IConfiguration Configuration)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_Configuration = Configuration;
			
			var rollerYaratıldı = StandartRolleriYarat();

			if (rollerYaratıldı)
			{
				BirinciAdminiYarat();
			}
		}

		private static bool StandartRolleriYarat()
		{
			try
			{
				List<string> standartRolList = new List<string>() { "admin", "user", "musteri", "superadmin" };
				foreach (var rol in standartRolList)
				{
					if(!Task.Run(()=> _roleManager.RoleExistsAsync(rol)).Result){
						var sonuc = Task.Run(()=> _roleManager.CreateAsync(new AppRole() { Name = rol })).Result;
					}
				}
				return true;
			}
			catch (Exception e)
			{
				string hata = e.Message;
				return false;
			}
		}

		private static void BirinciAdminiYarat()
		{
			try
			{
				AppUsers userAdmin = new AppUsers()
				{
					UserName = _Configuration.GetSection("Kurulum").GetSection("AdminEmail").Value,
					Email = _Configuration.GetSection("Kurulum").GetSection("AdminEmail").Value,
					EmailConfirmed = true,
					kullanıcıAdı = _Configuration.GetSection("Kurulum").GetSection("AdminKulAdi").Value,
					LockoutEnabled = true,
					uyelikTarihi = new DateTime(2000, 1, 1)
				};

				var adminZatenVar = _userManager.FindByNameAsync(_Configuration.GetSection("Kurulum").GetSection("AdminEmail").Value);
				if (adminZatenVar.Result == null)
				{
					_userManager.CreateAsync(userAdmin, _Configuration.GetSection("Kurulum").GetSection("AdminPass").Value).Wait();
					_userManager.AddToRoleAsync(userAdmin, "admin").Wait();
				}
			}
			catch (Exception e)
			{
				string hata = e.Message;
				return;
			}
			
		}
	}
}
