using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace ofsen.ViewModels
{
	public class PanolarIndexMenu
	{
		[Required]
		public string kategori { get; set; }

		public string awesome { get; set; }

		public List<PanolarIndexAltMenu> alt { get; set; }
	}

	public class PanolarIndexAltMenu
	{
		[Required]
		public string isim { get; set; }

		[Required]
		public string url { get; set; }

		public string awesome { get; set; }
	}

}
