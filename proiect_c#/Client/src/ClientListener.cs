using csharp.Domain;
using csharp.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace chsarp.Client
{
	[Serializable]
	public class ClientListener : MarshalByRefObject, IObserver, IClientWorker
	{
		private readonly IRemotableObject appState;
		private readonly HashSet<IObserver> forms = new HashSet<IObserver>();
		private readonly TcpChannel channel;

		public ClientListener()
		{
			try
			{
				BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
				serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
				BinaryClientFormatterSinkProvider clientProv = new BinaryClientFormatterSinkProvider();
				IDictionary props = new Hashtable();

				props["port"] = 0;
				TcpChannel channel = new TcpChannel(props, clientProv, serverProv);
				ChannelServices.RegisterChannel(channel, false);
				appState = (IRemotableObject)Activator.GetObject(typeof(RemotableObject), "tcp://localhost:45000/appstate");
				appState.AddObservable(this);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}

		public bool Login(string user, string pass)
		{
			if (appState.Login(user, pass))
			{
				appState.AddObservable(this);
				return true;
			}
			return false;
		}

		public List<Reservation> GetReservations(Trip trip)
		{
			return appState.GetReservations(trip);
		}

		public int GetRemainingSeats(Trip trip)
		{
			return appState.GetRemainingSeats(trip);
		}

		public void AddReservation(Reservation res)
		{
			appState.AddReservation(res);
		}

		public List<Destination> GetDestinations()
		{
			return appState.GetDestinations();
		}

		public List<Trip> GetTrips(Destination dest, DateTime time)
		{
			return appState.GetTrips(dest, time);
		}

		public void AddObservable(IObserver observable)
		{
			forms.Add(observable);
		}

		public void RemoveObservable(IObserver observable)
		{
			forms.Remove(observable);
		}

		public event EventHandler<ReloadDataEventArgs> ReloadData;

		public void ReloadForms()
		{
			EventHandler<ReloadDataEventArgs> handler = ReloadData;
			handler?.Invoke(this, new ReloadDataEventArgs());
		}

		public void UpdateObs()
		{
			ReloadForms();
		}
	}
}