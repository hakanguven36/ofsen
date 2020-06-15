using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ofsen.Araclar;

namespace ofsen.ViewModels
{
	public class HesaplarGirisModel
	{
		[Required(ErrorMessage = "Bu alan gerekli")]
		[Display(Name = "E-posta")]
		[DataType(DataType.EmailAddress)]
		public string eposta { get; set; }

		[Required(ErrorMessage = "Bu alan gerekli")]
		[Display(Name = "Şifre")]
		[DataType(DataType.Password)]
		[StringLength(12, MinimumLength = 4, ErrorMessage = "Şifre 4-12 karakter uzunluğunda olmalı")]
		public string sifre { get; set; }

		public bool hatirla { get; set; }
	}
}
