using csharp.Domain;
using System;
using System.Collections.Generic;

namespace csharp.Services.Service
{
	public class TripService : BaseService<Trip, ITripRepository>
	{
		private readonly ReservationService reservationService = new ReservationService(new ReservationRepository(), new ReservationValidator());

		public TripService(TripRepository repository, TripValidator validator) : base(repository, validator)
		{
		}

		public List<Trip> GetTripsByDestination(Destination destination)
		{
			List<Trip> trips = repository.GetTripsByDestination(destination);
			trips.ForEach(trip => trip.LeftSeats = reservationService.CountRemainingSeatsOnTrip(trip));
			return trips;
		}

		public List<Trip> GetTripsByDestinationAndDate(Destination destination, DateTime date)
		{
			List<Trip> trips = repository.GetTripsByDestinationAndDate(destination, date.Date);
			trips.ForEach(trip => trip.LeftSeats = reservationService.CountRemainingSeatsOnTrip(trip));
			return trips;
		}
	}
}