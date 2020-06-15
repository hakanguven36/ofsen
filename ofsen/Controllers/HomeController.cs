using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ofsen.Models;
using ofsen.Araclar;
using Newtonsoft.Json;

namespace ofsen.Controllers
{
    public class HomeController : Controller
    {
		private readonly OfsenContext db;
		private readonly IActionContextAccessor accesor;

		public HomeController(OfsenContext db, IActionContextAccessor accesor)
		{
			this.db = db;
			this.accesor = accesor;
		}

        public IActionResult Index()
        {
			var sayfa = db.Pano.Where(u => u.aktif == true & u.anatab == Anatab.anasayfa).ToList();
            return View(sayfa);
        }

		public IActionResult Projeler()
		{
			var sayfa = db.Pano.Where(u => u.aktif == true & u.anatab == Anatab.proje).ToList();
			return View(sayfa);
		}

		public IActionResult Blog()
		{
			var sayfa = db.Pano.Where(u => u.aktif == true & u.anatab == Anatab.blog).ToList();
			return View(sayfa);
		}

		[HttpGet]
		public IActionResult İletişim()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> İletişim(Comment comment)
		{
			try
			{
				comment.ipno = GetIP();
				comment.okundu = false;
				comment.tarih = DateTime.Now;
				db.Comment.Add(comment);
				await db.SaveChangesAsync();
				return Json("Mesajınız alındı. Teşekkürler.");
			}
			catch
			{
				return Json("Sistem hatası.");
			}
		}

		private string GetIP()
		{
			return accesor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
		}

		public IActionResult Error()
		{
			return View();
		}

		public	IActionResult StatusCodeErrors(int id)
		{
			ViewBag.statusCode = id;
			return View();
		}
	}
}