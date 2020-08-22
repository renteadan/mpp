using csharp.Domain;
using System;
using System.Collections.Generic;

namespace csharp.Networking
{
	public interface IRemotableObject
	{
		void AddObservable(IObserver observable);
		void AddReservation(Reservation res);
		List<Destination> GetDestinations();
		List<Reservation> GetReservations(Trip trip);
		List<Trip> GetTrips(Destination dest, DateTime time);
		int GetRemainingSeats(Trip trip);
		bool Login(string username, string password);
		void NotifyObservables();
		void RemoveObservable(IObserver observable);
	}
}