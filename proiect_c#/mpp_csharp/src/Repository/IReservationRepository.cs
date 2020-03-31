using System.Collections.Generic;

public interface IReservationRepository: IRepository<Reservation>
{
	List<Reservation> GetReservationsByTrip(Trip trip);
}