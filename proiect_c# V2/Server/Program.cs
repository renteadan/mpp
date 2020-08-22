using System;

namespace csharp.Server
{
	class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			new TCPServer().Start();
		}
	}
}