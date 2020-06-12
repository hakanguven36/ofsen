using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ofsen.Araclar;

namespace ofsen.Models
{
	public partial class AppUsers : IdentityUser<int>
	{
		[Required(ErrorMessage ="Bu alan gerekli!")]
		[StringLength(12,MinimumLength = 6,ErrorMessage = "Bu alan 6-12 karakter uzunluğunda olmalı")]
		public string kullanıcıAdı { get; set; }

		public DateTime? uyelikTarihi { get; set; }

		public bool silindi { get; set; }
	}

	public enum Role
	{
		none,
		admin,
		user,
		newuser
	}
}
