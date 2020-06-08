using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ofsen.Models
{
	[Table("Sayfa")]
	public partial class Sayfa
	{
		public int ID { get; set; }

		public string icerik { get; set; }
	}
}
