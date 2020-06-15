using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using ofsen.Araclar;
using ofsen.Models;
using ofsen.ViewModels;

namespace ofsen.Controllers
{
    public class HesaplarController : Controller
    {
		#region ctor
		private UserManager<AppUsers> userManager {get;}
		private SignInManager<AppUsers> signInManager { get; }
		private IEmailService emailService { get; }
		public HesaplarController(UserManager<AppUsers> userManager, SignInManager<AppUsers> signInManager, IEmailService emailService)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.emailService = emailService;
		}
		#endregion

		public IActionResult Index() => RedirectToAction("GirişYap", "Hesaplar");
		
		[HttpGet]
		public IActionResult GirişYap()
		{
			if (signInManager.IsSignedIn(User))
				return RedirectToAction("Index", "Home");
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> GirişYap(HesaplarGirisModel model)
		{
			if (ModelState.IsValid)
			{
				var findUser = userManager.FindByNameAsync(model.eposta);
				if (findUser.Result == null)
				{
					ModelState.AddModelError("", "Hatalı giriş teşebbüsü");
					return View(model);
				}
				if (findUser.Result.silindi)
				{
					ModelState.AddModelError("", "Kullanıcı silindi!");
					return View(model);
				}
				var isLockedOut = await userManager.IsLockedOutAsync(findUser.Result);
				if(isLockedOut)
				{
					ModelState.AddModelError("", "Maksimum sayıda hatalı giriş yaptınız! Hesabınız 5 dakikalığına bloklandı.");
					return View(model);
				}
				if (!findUser.Result.EmailConfirmed)
				{
					return RedirectToAction("EpostaAdresiniDoğrulayın", "Hesaplar", new { email = model.eposta});
				}

				var signInResult = await signInManager.PasswordSignInAsync(model.eposta, model.sifre, model.hatirla, false);

				if (signInResult.Succeeded)
				{
					
					await userManager.ResetAccessFailedCountAsync(findUser.Result);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					await userManager.AccessFailedAsync(findUser.Result);
					ModelState.AddModelError("", "Hatalı giriş teşebbüsü");
				}
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult ÜyeOl() => View();
		[HttpPost]
		public async Task<IActionResult> ÜyeOl(HesaplarUyelikModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new AppUsers()
				{
					UserName = model.eposta,
					Email = model.eposta,
					kullanıcıAdı = model.kullaniciAdi,
					uyelikTarihi = DateTime.Now
				};

				var result = await userManager.CreateAsync(user, model.sifre);

				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, "user");
					var confirmationEmailSent = await SendConfirmationEmail(user);
					if (confirmationEmailSent)
					{
						return RedirectToAction("EpostaAdresiniDoğrulayın", "Hesaplar", new { email = model.eposta });
					}
					else
						ModelState.AddModelError("", "Eposta gönderme hatası!");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			return View(model);
		}

		private async Task<bool> SendConfirmationEmail(AppUsers user)
		{
			try
			{
				var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
				var link = Url.Action(nameof(EpostaDogrula), "Hesaplar", new { userID = user.Id, code = code }, Request.Scheme, Request.Host.tooString());
				FreeTokenModel tokenModel = new FreeTokenModel()
				{ 
					userID = user.Id.tooString(),
					code = code,
					confirmationLink = link
				};
				string sayfa = this.RenderViewAsync("EmailConfirmationPage", tokenModel).Result;
				await emailService.SendAsync(user.Email, "E-posta adresini doğrulayın", sayfa, true);
				return true;
			}
			catch(Exception e)
			{
				string mesaj = e.Message;
				return false;
			}
		}
		public IActionResult EmailConfirmationPage(FreeTokenModel tokenModel)
		{
			return View(tokenModel);
		}

		public async Task<IActionResult> EpostaDogrula(string userID, string code)
		{
			var user = await userManager.FindByIdAsync(userID);
			if (user != null)
			{
				var confirmation = await userManager.ConfirmEmailAsync(user, code);
				if (confirmation.Succeeded)
				{
					await signInManager.SignInAsync(user, false);
					return View();
				}
			}
			return BadRequest();
		}
		public async Task<IActionResult> ÇıkışYap()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
		public async Task<IActionResult> EpostaAdresiniDoğrulayın(string email)
		{
			var user = await userManager.FindByEmailAsync(email);
			if (signInManager.IsSignedIn(User) || user.EmailConfirmed)
				return RedirectToAction("Index", "Home");
			FreeTokenModel model = new FreeTokenModel();
			model.email = email;
			model.emailDomain = "http://www." + model.email.Substring(email.IndexOf('@') + 1);
			return View(model);
		}

	}
}