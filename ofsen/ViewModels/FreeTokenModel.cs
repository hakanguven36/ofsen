using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofsen.ViewModels
{
	public class FreeTokenModel
	{
		public string userID { get; set; }
		public string code { get; set; }
		public string confirmationLink { get; set; }
	}
}
