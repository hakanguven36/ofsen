using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ofsen.Araclar;

namespace ofsen.ViewModels
{
	public class HesaplarUyelikModel
	{
		[Required(ErrorMessage = "Bu alan gerekli")]
		[Display(Name ="Kullanıcı Adı")]
		[StringLength(12,MinimumLength =4 , ErrorMessage ="Kullanıcı adı 4-12 karakter uzunluğunda olmalı")]
		public string kullaniciAdi { get; set; }

		[Required(ErrorMessage = "Bu alan gerekli")]
		[Display(Name = "E-posta")]
		[DataType(DataType.EmailAddress)]
		public string eposta { get; set; }

		[ScaffoldColumn(true)]
		public string epostaDomain { get; set; }

		[Required(ErrorMessage = "Bu alan gerekli")]
		[Display(Name = "Şifre")]
		[DataType(DataType.Password)]
		[StringLength(12, MinimumLength = 4, ErrorMessage = "Şifre 4-12 karakter uzunluğunda olmalı")]
		public string sifre { get; set; }


		[Display(Name = "Şifre(Tekrar)")]
		[DataType(DataType.Password)]
		[Compare("sifre", ErrorMessage = "Girdiğiniz şifreler uyuşmuyor")]
		public string sifreTekrar{ get; set; }
	}
}
