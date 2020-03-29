using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ReservationService: BaseService<Reservation, ReservationRepository>
{
	public ReservationService(): base(new ReservationRepository(), new ReservationValidator()) { }

  public  new Reservation Insert(Reservation reservation)
  {
    int seats =  countReservedSeatsOnTrip(reservation.Trip);
    if(18 - seats<reservation.SeatsNr) {
      throw new ValidationError("Not enough seats!");
    }
    return  base.Insert(reservation);
  }

  public List<Reservation> getReservationsByTrip(Trip trip)
{
  return repository.GetReservationsByTrip(trip);
}

public  int countReservedSeatsOnTrip(Trip trip)
{
  List<Reservation> reservations =  repository.GetReservationsByTrip(trip);
  int seats = 0;
  foreach (var reservation in reservations)
  {
    seats += reservation.SeatsNr;
  }
  return seats;
}

public  int CountRemainingSeatsOnTrip(Trip trip)
{
  return 18 -  countReservedSeatsOnTrip(trip);
}
}

