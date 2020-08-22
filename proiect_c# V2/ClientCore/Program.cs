using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csharp.ClientCore
{
	static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			ProtoClientWorker listener = new ProtoClientWorker();
			LoginForm form = new LoginForm(listener);
			Application.Run(form);
			listener.Unsubcribe();
		}
	}
}
