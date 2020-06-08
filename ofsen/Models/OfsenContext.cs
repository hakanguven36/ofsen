using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ofsen.ViewModels;

namespace ofsen.Models
{
	public class OfsenContext : IdentityDbContext<Users>
	{
		public OfsenContext(DbContextOptions<OfsenContext> options):base(options)
		{
		}

		public DbSet<Users> Kullar { get; set; }
		public DbSet<Pano> Pano { get; set; }
		public DbSet<Sayfa> Sayfa { get; set; }
		public DbSet<Comment> Comment { get; set; }
		public DbSet<ofsen.ViewModels.HesaplarÜyeOlModel> HesaplarÜyeOlModel { get; set; }
		public DbSet<ofsen.ViewModels.HesaplarGirişYapModel> HesaplarGirişYapModel { get; set; }


	}
}
