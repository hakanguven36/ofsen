using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Core;
using ofsen.Araclar;
using ofsen.Models;
using ofsen.ViewModels;


namespace ofsen.Controllers
{
    public class HesaplarController : Controller
    {
		private UserManager<AppUsers> userManager { get; }
		private SignInManager<AppUsers> signInManager { get; }
		private IEmailService emailService { get; }

		public HesaplarController(UserManager<AppUsers> userManager, SignInManager<AppUsers> signInManager, IEmailService emailService)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.emailService = emailService;
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
				AppUsers user = await userManager.FindByNameAsync(model.email);
				if(user == null)
				{
					user = new AppUsers();
					user.UserName = model.email;
					user.Email = model.email;
					user.kullanıcıAdı = model.username;
					user.uyelikTarihi = DateTime.Now;

					var result = await userManager.CreateAsync(user, model.password);

					if (result.Succeeded)
					{
						var theToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
						var link = Url.Action(nameof(EpostaDogrula), "Hesaplar", new { userID = user.Id, code = theToken });

						await emailService.SendAsync(user.Email, "E-posta adresini doğrulayın", "", true);

						return RedirectToAction("EpostanızıDoğrulayın", "Hesaplar", new { thatusername = model.email });
					}
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
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
				// TODO: kullanıcı ip sini kontrol et.. üst üste 5 kez hata olduysa ipyi blokla gitsin...
				var findUser = userManager.FindByNameAsync(model.username);
				if(findUser.Result == null)
				{
					ModelState.AddModelError("", "Hatalı giriş teşebbüsü");
					return View(model);
				}
				if (findUser.Result.silindi)
				{
					ModelState.AddModelError("", "Bu kullanıcı silindi!");
					return View(model);
				}
				if (!findUser.Result.EmailConfirmed)
				{
					ModelState.AddModelError("", "EmailConfirmationNeeded"); //EmailConfirmationNeeded söz öbeği js tarafında kullanılıyor!
					return View(model);
				}

				var signInResult = await signInManager.PasswordSignInAsync(model.username, model.password, model.hatırla, false);
				if (signInResult.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					findUser.Result.AccessFailedCount++;
				}
				ModelState.AddModelError("", "Hatalı giriş teşebbüsü");

			}
			return View(model);
		}

		public IActionResult EpostanızıDoğrulayın(string thatusername)
		{
			ViewBag.epostaniz = thatusername;
			ViewBag.epostaSaglayici = "http://www." + thatusername.Substring(thatusername.IndexOf('@') + 1);
			return View();
		}

		public IActionResult EpostaDoğrulamaGerekli(string thatusername)
		{
			ViewBag.epostaSaglayici = "http://www." + thatusername.Substring(thatusername.IndexOf('@') + 1);
			return PartialView();
		}

		public	IActionResult EpostaDogrula(string userID, string code)
		{
			// TODO : doğrula...
			ViewBag.message = "Tebrikler...";
			return View();

		}

		[HttpPost]
		public IActionResult TekrarGonder(string thatusername)
		{
	
			// TODO: düzenlenecek..

			return Json("Doğrulama e-postası yeniden gönderildi. Lütfen e-posta hesabınızı kontrol ediniz.");
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

		public async Task<IActionResult> Deneme()
		{
			var model = new FreeTokenModel() { userID="1", code="falanfilantokenKodu2343234234" };
			ViewBag.deneme = await this.RenderViewAsync("EmailConfirmationPage", model);
			return View();
		}

		public IActionResult EmailConfirmationPage(FreeTokenModel model)
		{

			var link = Url.Action(nameof(EpostaDogrula), "Hesaplar", new { userID = model.userID, code = model.code });
			if (string.IsNullOrWhiteSpace(model.confirmationLink))
				return null;
			ViewBag.confirmationString = model.confirmationLink;
			return View();
		}
    }
}