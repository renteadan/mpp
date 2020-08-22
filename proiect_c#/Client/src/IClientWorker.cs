using csharp.Domain;
using csharp.Networking;
using System;
using System.Collections.Generic;

namespace chsarp.Client
{
	public interface IClientWorker
	{
		event EventHandler<ReloadDataEventArgs> ReloadData;

		void AddReservation(Reservation res);
		List<Destination> GetDestinations();
		int GetRemainingSeats(Trip trip);
		List<Reservation> GetReservations(Trip trip);
		List<Trip> GetTrips(Destination dest, DateTime time);
		bool Login(string user, string pass);

		void ReloadForms();
	}
}