using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ofsen.Models
{
	[Table("Pano")]
	public partial class Pano
	{
		public int ID { get; set; }

		public Anatab anatab { get; set; }

		[StringLength(500)]
		public string title { get; set; }

		[NotMapped]
		public IFormFile thumb { get; set; }

		[StringLength(500)]
		public string thumbName { get; set; }

		[StringLength(5000)]
		[DataType(DataType.MultilineText)]
		public string metin { get; set; }

		[DataType(DataType.Date)]
		public DateTime? yayinTarihi { get; set; }

		public bool aktif { get; set; }
	
		public Sayfa sayfa { get; set; }
	}

	public enum Anatab
	{
		anasayfa,
		proje,
		blog
	}
}
