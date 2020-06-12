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

		#region Üyelik ve Eposta doğrulama
		[HttpGet]
		public IActionResult ÜyeOl() => View();
		[HttpPost]
		public async Task<IActionResult> ÜyeOl(HesaplarÜyeOlModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new AppUsers()
				{
					UserName = model.email,
					Email = model.email,
					kullanıcıAdı = model.username,
					uyelikTarihi = DateTime.Now
				};

				var result = await userManager.CreateAsync(user, model.password);

				if (result.Succeeded)
				{
					return RedirectToAction("EpostaAdresiniDoğrulayın", "Hesaplar", new {user});
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
				
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult EpostaAdresiniDoğrulayın() => View();
		[HttpPost]
		public IActionResult EpostaAdresiniDoğrulayın(AppUsers user)
		{
			Task.Run(() => SendConfirmationEmail(user));
			return View();
		}

		public async Task< bool> SendConfirmationEmail(AppUsers user)
		{
			try
			{
				// token yarat
				// sayfayı string olarak hazırla

				var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
				var link = Url.Action(nameof(EpostaDogrula), "Hesaplar", new { userID = user.Id, code = token });
				FreeTokenModel tokenModel = new FreeTokenModel() { confirmationLink = link };
				string sayfa = this.RenderViewAsync("EmailConfirmationPage", tokenModel).Result;
				await emailService.SendAsync(user.Email, "E-posta adresini doğrulayın", sayfa, true);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public IActionResult EmailConfirmationPage(FreeTokenModel model)
		{
			return View(model);
		}

		#endregion

		#region Giriş ve şifremi unuttum.. Ayrıca doğrumayı yeniden gönder
		[HttpGet]
		public IActionResult GirişYap() => View();
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
		#endregion


		public IActionResult EpostanızıDoğrulayın(string thatusername)
		{
			ViewBag.epostaniz = thatusername;
			ViewBag.epostaSaglayici = "http://www." + thatusername.Substring(thatusername.IndexOf('@') + 1);
			return View();
		}

		public	IActionResult EpostaDogrula(string userID, string code)
		{
			// TODO : doğrula...
			ViewBag.message = "Tebrikler...";
			return View();

		}

    }
}