package Service;

import Domain.Reservation;
import Domain.Trip;
import Errors.ValidationError;
import Gateway.ReservationGateway;
import Validator.ReservationValidator;

import java.util.Vector;

public class ReservationService extends BaseService<Reservation, ReservationGateway> {
  protected ReservationService() {
    super(new ReservationGateway(), new ReservationValidator());
  }

  public Reservation insert(Reservation reservation) throws ValidationError {
    int seats = countSeatsOnTrip(reservation.getTrip());
    if(18 - seats < reservation.getSeatsNr()) {
      throw new ValidationError("Not enough seats!");
    }
    return super.insert(reservation);
  }

  public Vector<Reservation> getReservationsByTrip(Trip trip) {
    return gateway.getReservationsByTrip(trip);
  }

  private int countSeatsOnTrip(Trip trip) {
    Vector<Reservation> reservations = gateway.getReservationsByTrip(trip);
    int seats = 0;
    for(Reservation reservation: reservations) {
      seats+=reservation.getSeatsNr();
    }
    return seats;
  }
}
