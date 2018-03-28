using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OrderParser;

namespace Egode
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (null != args && args.Length > 0)
			{
				if (args[0].ToLower().Equals("rate"))
					Application.Run(new RateForm(null));
				else if (args[0].ToLower().Equals("local"))
					Application.Run(new MainForm(true));
				else if (args[0].ToLower().Equals("stocksh"))
					Application.Run(new StockStatForm());
			}
			else
				Application.Run(new MainForm(false));
		}
	}
}