using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ofsen.Models;
using ofsen.ViewModels;


namespace ofsen.Controllers
{
    public class HesaplarController : Controller
    {
		UserManager<IdentityUser> userManager;
		SignInManager<IdentityUser> signInManager;

		public HesaplarController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

        public IActionResult Index()
        {
            return RedirectToAction("GirişYap","Hesaplar");
        }

		[HttpGet]
		public IActionResult ÜyeOl()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ÜyeOl(HesaplarÜyeOlModel model)
		{

			if (ModelState.IsValid)
			{
				var user = new IdentityUser { UserName = model.email, Email = model.email};
				var result = await userManager.CreateAsync(user, model.password);
				if (result.Succeeded)
				{
					await signInManager.SignInAsync(user, false);
					return RedirectToAction("Index", "Home");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			return View(model);
		}
		[HttpGet]
		public IActionResult GirişYap()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> GirişYap(HesaplarGirişYapModel model)
		{
			if (ModelState.IsValid)
			{
				var user = signInManager.UserManager.Users.FirstOrDefault(u => u.Email == model.username);
				var denemeResult = await signInManager.CheckPasswordSignInAsync(user, model.password, false);

				
				var result = await signInManager.PasswordSignInAsync(model.username, model.password, model.hatırla, false);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
				
				ModelState.AddModelError("", "Hatalı giriş teşebbüsü");
			}
			return View(model);
		}

		public IActionResult ŞifremiUnuttum()
		{
			return View();
		}

		public async Task<IActionResult> ÇıkışYap()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
    }
}