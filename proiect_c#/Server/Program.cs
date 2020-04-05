using csharp.Server.Server;
using System;

namespace csharp.Server
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			new ServerImpl().Start();
		}
	}
}