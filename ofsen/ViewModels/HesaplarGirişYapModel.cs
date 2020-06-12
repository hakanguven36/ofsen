using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ofsen.ViewModels
{
	
	public class HesaplarGirişYapModel
	{
		[Required(ErrorMessage = "Bu alan gerekli")]
		[Display(Name = "E-posta adresiniz")]
		public string username { get; set; }

		[Required(ErrorMessage = "Bu alan gerekli")]
		[Display(Name = "Şifre")]
		[DataType(DataType.Password)]
		[StringLength(12, MinimumLength = 4, ErrorMessage = "Şifre 4-12 karakter uzunluğunda olmalı")]
		public string password { get; set; }

		[Display(Name = "Beni Hatırla")]
		public bool hatırla { get; set; }
	}
}
