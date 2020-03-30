using System;
using System.Collections.Generic;

class TripService: BaseService<Trip, TripRepository>
{
	private readonly ReservationService reservationService = new ReservationService();
	public TripService() : base(new TripRepository(), new TripValidator())
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

