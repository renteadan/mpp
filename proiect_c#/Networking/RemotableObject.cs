using csharp.Domain;
using csharp.Services.Service;
using System;
using System.Collections.Generic;

namespace csharp.Networking
{
	[Serializable]
	public class RemotableObject : MarshalByRefObject, IRemotableObject
	{
		private readonly ReservationService reservationService = new ReservationService(new ReservationRepository(), new ReservationValidator());
		private readonly TripService tripService = new TripService(new TripRepository(), new TripValidator());
		private readonly DestinationService destinationService = new DestinationService(new DestinationRepository(), new DestinationValidator());
		private readonly LoginService loginService = new LoginService();
		private HashSet<IObserver> observables = new HashSet<IObserver>();
		public RemotableObject()
		{
		}

		public List<Destination> GetDestinations()
		{
			return destinationService.FindAll();
		}

		public List<Trip> GetTrips(Destination dest, DateTime time)
		{
			return tripService.GetTripsByDestinationAndDate(dest, time);
		}

		public List<Reservation> GetReservations(Trip trip)
		{
			return reservationService.GetReservationsByTrip(trip);
		}

		public bool Login(string username, string password)
		{
			return loginService.Login(username, password);
		}

		public int GetRemainingSeats(Trip trip)
		{
			return reservationService.CountRemainingSeatsOnTrip(trip);
		}

		public void AddReservation(Reservation res)
		{
			reservationService.Insert(res);
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
