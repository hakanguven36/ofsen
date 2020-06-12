using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using ofsen.Models;


namespace ofsen
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration Configuration) =>
			this.Configuration = Configuration;

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddIdentity<AppUsers, AppRole>(option =>
			{
				option.User.RequireUniqueEmail = true;
				option.User.AllowedUserNameCharacters = "ABC�DEFG�HI�JKLMNO�PRS�TU�VYZWXQabc�defg�h�ijklmno�prs�tu�vyzwxq1234567890_.-@";
				option.SignIn.RequireConfirmedEmail = true;
				option.Password.RequireDigit = false;
				option.Password.RequiredLength = 4;
				option.Password.RequireLowercase = false;
				option.Password.RequireUppercase = false;
				option.Password.RequiredUniqueChars = 2;
				option.Password.RequireNonAlphanumeric = false;
			}).AddEntityFrameworkStores<OfsenContext>();
			services.AddDbContext<OfsenContext>(option => option.UseSqlServer(Configuration.GetConnectionString("MyConn")));
			services.AddMvc(option => option.EnableEndpointRouting = false).AddRazorRuntimeCompilation();
			services.AddSession(option => option.IdleTimeout = TimeSpan.FromMinutes(30));
			services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();  // getip of cli
			services.AddMailKit(config => config.UseMailKit(Configuration.GetSection("Eposta").Get<MailKitOptions>()));
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}
			app.UseStatusCodePagesWithRedirects("/Home/StatusCodeErrors/{0}");

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSession();

			app.UseRouting();


			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "areas",
					template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
				);
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
