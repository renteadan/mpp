using csharp.Domain;
using System.Collections.Generic;

public class ReservationService : BaseService<Reservation, IReservationRepository>
{
	public ReservationService(ReservationRepository repository, ReservationValidator validator) : base(repository, validator)
	{
	}

	public new Reservation Insert(Reservation reservation)
	{
		int seats = CountReservedSeatsOnTrip(reservation.Trip);
		if (18 - seats < reservation.SeatsNr)
		{
			throw new ValidationError("Not enough seats!");
		}
		return base.Insert(reservation);
	}

	public List<Reservation> GetReservationsByTrip(Trip trip)
	{
		return repository.GetReservationsByTrip(trip);
	}

	public int CountReservedSeatsOnTrip(Trip trip)
	{
		List<Reservation> reservations = repository.GetReservationsByTrip(trip);
		int seats = 0;
		foreach (var reservation in reservations)
		{
			seats += reservation.SeatsNr;
		}
		return seats;
	}

	public int CountRemainingSeatsOnTrip(Trip trip)
	{
		return 18 - CountReservedSeatsOnTrip(trip);
	}
}