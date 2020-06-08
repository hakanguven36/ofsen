using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			var zaman = DateTime.Now.ToString("yyMMddHHmm");
			Console.WriteLine(zaman);
			Console.ReadLine();
		}
	}
}
