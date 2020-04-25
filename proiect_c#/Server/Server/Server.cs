using csharp.Networking;
using csharp.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Threading;

namespace csharp.Server
{
	internal class Server
	{
		private readonly RemotableObject appState;
		private TcpChannel channel;

		public Server()
		{
			BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
			serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
			BinaryClientFormatterSinkProvider clientProv = new BinaryClientFormatterSinkProvider();
			IDictionary props = new Hashtable();

			props["port"] = 45000;
			TcpChannel channel = new TcpChannel(props, clientProv, serverProv);
			appState = new RemotableObject();
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