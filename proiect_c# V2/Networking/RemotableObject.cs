using csharp.Domain;
using csharp.Services;
using System;
using System.Collections.Generic;

namespace csharp.Networking
{
	[Serializable]
	public class RemotableObject : MarshalByRefObject, IRemotableObject
	{
		private ServiceImplementation service = new ServiceImplementation();
		private HashSet<IObserver> observables = new HashSet<IObserver>();
		public RemotableObject()
		{
		}

		public List<Destination> GetDestinations()
		{
			return service.GetDestinations();
		}

		public List<Trip> GetTrips(Destination dest, DateTime time)
		{
			return service.GetTrips(dest, time);
		}

		public List<Reservation> GetReservations(Trip trip)
		{
			return service.GetReservations(trip);
		}

		public bool Login(string username, string password)
		{
			return service.Login(username, password);
		}

		public int GetRemainingSeats(Trip trip)
		{
			return service.GetRemainingSeats(trip);
		}

		public void AddReservation(Reservation res)
		{
			service.AddReservation(res);
			NotifyObservables();
		}

		public void AddObservable(IObserver observable)
		{
			observables.Add(observable);
		}

		public void RemoveObservable(IObserver observable)
		{
			observables.Remove(observable);
		}

		public void NotifyObservables()
		{
			foreach (IObserver obs in observables)
			{
				obs.UpdateObs();
			}
		}
	}
}
