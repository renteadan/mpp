
using csharp.Networking;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace chsarp.Client
{
	public class ClientListener
	{
		private readonly TcpClient client;
		private readonly BinaryFormatter formatter = new BinaryFormatter();
		private readonly HashSet<IForm> forms = new HashSet<IForm>();
		private readonly Queue<IResponse> responses = new Queue<IResponse>();
		private readonly EventWaitHandle waitHandle = new AutoResetEvent(false);
		private bool running = true;

		public ClientListener()
		{
			try
			{
				client = new TcpClient("localhost", 45000);
			} catch(Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}

		public void AddForm(IForm form)
		{
			forms.Add(form);
		}
		public void Start()
		{
			Thread t = new Thread(Run);
			t.Start();
		}

		private void Run()
		{
			while (running)
			{
				try
				{
					IResponse response = (IResponse)formatter.Deserialize(client.GetStream());
					Console.WriteLine($"New response {response}");
					if (response is ResponseReloadData)
					{
						EventHandler<ReloadDataEventArgs> handler = ReloadData;
						handler?.Invoke(this, new ReloadDataEventArgs());
					} else
					{
						responses.Enqueue(response);
						waitHandle.Set();
					}
				} catch(Exception e)
				{
					Console.WriteLine(e.StackTrace);
					Dispose();
				}
			}
		}

		public void SendQuery(IQuery query)
		{
			formatter.Serialize(client.GetStream(), query);
			client.GetStream().Flush();
			NotifiyForms();
		}

		public void SendAdd(IAdd add)
		{
			formatter.Serialize(client.GetStream(), add);
			client.GetStream().Flush();
		}

		private void NotifiyForms()
		{
			try
			{
				waitHandle.WaitOne();
				lock (responses)
				{
					IResponse response = responses.Dequeue();
					foreach (IForm form in forms)
					{
						form.HandleResponse(response);
					}
				}
			} catch(Exception e)
			{
				Console.WriteLine(e.StackTrace);
				Dispose();
			}
		}

		public void Dispose()
		{
			running = false;
			client?.Close();
		}

		public event EventHandler<ReloadDataEventArgs> ReloadData;
	}
}
