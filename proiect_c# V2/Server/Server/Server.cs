using csharp.Networking;
using csharp.Services;
using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace csharp.Server
{
	internal class TCPServer
	{
		public TCPServer()
		{
			BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
			serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
			BinaryClientFormatterSinkProvider clientProv = new BinaryClientFormatterSinkProvider();
			IDictionary props = new Hashtable();

			props["port"] = 45000;
			TcpChannel channel = new TcpChannel(props, clientProv, serverProv);
			ChannelServices.RegisterChannel(channel, false);
			RemotingConfiguration.RegisterWellKnownServiceType(
				typeof(RemotableObject), "appstate", WellKnownObjectMode.Singleton);
		}

		public void Start()
		{
			Console.WriteLine("Press enter to exit");
			Console.ReadLine();
		}
	}
}