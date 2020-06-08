using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ofsen.Models
{
	[Table("Comment")]
	public partial class Comment
	{
		public int ID { get; set; }

		[Required]
		[StringLength(60)]
		public string eposta { get; set; }

		[Required]
		[StringLength(maximumLength: 600, MinimumLength = 6, ErrorMessage ="Bu alan 6-600 karakter olmalı.")]
		public string mesaj { get; set; }

		[StringLength(20)]
		public string ipno { get; set; }

		public DateTime? tarih { get; set; }

		public bool okundu { get; set; }

	}
}
