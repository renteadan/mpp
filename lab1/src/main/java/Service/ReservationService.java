package Service;

import Domain.Reservation;
import Gateway.ReservationGateway;

public class ReservationService extends BaseService<Reservation, ReservationGateway> {
  protected ReservationService() {
    super(new ReservationGateway());
  }
}
