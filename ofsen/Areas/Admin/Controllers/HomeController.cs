﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ofsen.Araclar;
using ofsen.Models;

namespace ofsen.Areas.Admin.Controllers
{

	// değişiklik github...
	[Area("Admin")]
	//[Yetki(Role.admin)]
	public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}