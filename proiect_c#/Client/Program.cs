using chsarp.Client;
using System;
using System.Windows.Forms;

namespace csharp.Client
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			ClientListener listener = new ClientListener();
			LoginForm form = new LoginForm(listener);
			Application.Run(form);
		}
	}
}
