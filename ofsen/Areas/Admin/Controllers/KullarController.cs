using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ofsen.Models;

namespace ofsen.Areas.Admin.Controllers
{
	[Area("Admin")]
    public class KullarController : Controller
    {
		OfsenContext db;
		UserManager<AppUsers> userManager;
		RoleManager<AppRole> roleManager;

		public KullarController(OfsenContext db, UserManager<AppUsers> userManager, RoleManager<AppRole> roleManager)
		{
			this.db = db;
			this.userManager = userManager;
			this.roleManager = roleManager;
		}

        public IActionResult Index()
        {
            return View(db.Users.ToList());
        }

		public IActionResult Detay(int id)
		{
			return PartialView(db.Users.Find(id));
		}

		public async Task<IActionResult> Düzenle(int id)
		{
			var user = await userManager.FindByIdAsync(id.ToString());
			if(user != null)
			{
				ViewBag.userRoles = userManager.GetRolesAsync(user).Result;
			}

			return View(user);
		}

		public IActionResult SiteRolleriniGetir()
		{
			return Json(roleManager.Roles.Select(u => u.Name));
		}

		[HttpPost]
		public async Task< IActionResult> UserRolEkle(int id, string[] roles)
		{
			var user = await userManager.FindByIdAsync(id.ToString());
			var tümRoller = roleManager.Roles.Select(u => u.Name).ToList();

			var tümRolleriKaldır = await userManager.RemoveFromRolesAsync(user, tümRoller);
			if (tümRolleriKaldır.Succeeded)
			{
				var addRoles = await userManager.AddToRolesAsync(user, roles);
				if (addRoles.Succeeded)
				{
					return Json("Roller düzenlendi.");
				}
			}

			return Json("Hata: Roller düzenlenemedi!");
		}
    }
}