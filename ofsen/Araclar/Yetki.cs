using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ofsen.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;

namespace ofsen.Araclar
{
	public class YetkiAttribute : ActionFilterAttribute
	{
		public YetkiAttribute(Role controllerRole)
		{
			this.controllerRole = controllerRole;
		}
  
		public Role controllerRole { get; set; }

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			Role userRole = (Role)(context.HttpContext.Session.GetInt32("yetki")??0);

			switch (controllerRole)
			{
				case Role.admin:
					if (userRole != Role.admin)
						Yetkisiz(context);
					break;
				case Role.user:
					if (userRole != Role.admin & userRole != Role.user & userRole != Role.newuser)
						Yetkisiz(context);
					break;
				case Role.newuser:
					if (userRole != Role.user & userRole != Role.newuser)
						Yetkisiz(context);
					break;
				default:
					break;
			}
		}

		private void Yetkisiz(ActionExecutingContext context)
		{
			context.Result =
				new RedirectToRouteResult(
				new RouteValueDictionary {
								{ "Controller", "Giriş" },
								{ "Area", null },
								{ "Action", "Index" }});
		}
	}
}
