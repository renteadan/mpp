using csharp.Services;
//using grpcService.Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace csharp.Server.Server
{
	class ProtoServerWorker
	{
		private bool connected = false;
		private NetworkStream stream;
		//private TestService service = new TestService();
		private TcpClient client;
		private Logger logger = new Logger("CApp");

		//public ProtoServerWorker(TestService service, TcpClient client)
		//{
		//	this.service = service;
		//	this.client = client;
		//	try
		//	{
		//		stream = client.GetStream();
		//		connected = true;
		//	} catch(Exception e)
		//	{
		//		logger.Error(e);
		//	}
		//}

		public void run()
		{
		}
	}
}
