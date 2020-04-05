using csharp.Domain;
using System;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using csharp.Networking;
using System.Net;
using System.Collections.Generic;
using System.Threading;

namespace csharp.Server.Server
{
	class ServerImpl
	{
    private readonly HashSet<ClientWorker> workers = new HashSet<ClientWorker>();
    private Logger logger = new Logger("CApp");
    public void Start()
		{
      TcpListener listener = new TcpListener(IPAddress.Loopback, 45000);
      TcpClient client;
			listener.Start();
      while (true)
			{
        try
        {
          client = listener.AcceptTcpClient();
          Console.WriteLine("New client!");
          var worker = new ClientWorker(client, this);
          workers.Add(worker);
          new Thread(worker.Run).Start();
        } catch(Exception e)
        {
          logger.Error(e);
        }
			}
    }

    public void NotifiyWorkers()
    {
      foreach(var woker in workers)
      {
        woker.NotifyClient();
      }
    }
  }
}
