
using csharp.Domain;
using System;
using System.Collections.Generic;

namespace csharp.Networking
{
	public interface IResponse
	{
	}

	[Serializable]
	public class ResponseDestinations: IResponse
	{
		public List<Destination> destinations;

		public ResponseDestinations(List<Destination> destinations)
		{
			this.destinations = destinations;
		}
	}

	[Serializable]
	public class ResponseTrips: IResponse
	{
		public List<Trip> trips;

		public ResponseTrips(List<Trip> trips)
		{
			this.trips = trips;
		}
	}

	[Serializable]
	public class ResponseReservations: IResponse
	{
		public List<Reservation> reservations;
		public int RemainingSeats;

		public ResponseReservations(List<Reservation> reservations, int remainingSeats)
		{
			this.reservations = reservations;
			RemainingSeats = remainingSeats;
		}
	}

	[Serializable]
	public class ResponseReloadData: IResponse
	{

	}

	[Serializable]
	public class ResponseLogin: IResponse
	{
		public bool Success;

		public ResponseLogin(bool success)
		{
			Success = success;
		}
	}
}