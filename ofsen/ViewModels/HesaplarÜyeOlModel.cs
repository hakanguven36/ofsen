using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ofsen.Araclar;

namespace ofsen.ViewModels
{
	public class HesaplarÜyeOlModel
	{
		[Required(ErrorMessage = "Bu alan gerekli")]
		[Display(Name ="Kullanıcı Adı")]
		[StringLength(12,MinimumLength =4 , ErrorMessage ="Kullanıcı adı 4-12 karakter uzunluğunda olmalı")]
		[UserName]
		public string username { get; set; }

		[Required(ErrorMessage = "Bu alan gerekli")]
		[Display(Name = "E-posta")]
		[DataType(DataType.EmailAddress)]
		public string email { get; set; }

		[Required(ErrorMessage = "Bu alan gerekli")]
		[Display(Name = "Şifre")]
		[DataType(DataType.Password)]
		[StringLength(12, MinimumLength = 4, ErrorMessage = "Şifre 4-12 karakter uzunluğunda olmalı")]
		public string password { get; set; }

		[Display(Name = "Şifre(Tekrar)")]
		[DataType(DataType.Password)]
		[Compare("password", ErrorMessage = "Girdiğiniz şifreler uyuşmuyor")]
		public string confirmpassword{ get; set; }
	}

	public class UserNameAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value.tooString().Contains('@'))
				return new ValidationResult("Kullanıcı adı '@' içeremez.");
			return ValidationResult.Success;
		}
	}
}
