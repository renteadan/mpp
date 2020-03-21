package Service;

import Domain.Destination;
import Domain.Trip;
import Gateway.TripGateway;
import Validator.TripValidator;

import java.util.Vector;

public class TripService extends BaseService<Trip, TripGateway> {
  public TripService() {
    super(new TripGateway(), new TripValidator());
  }

  public Vector<Trip> getTripsByDestination(Destination destination) {
    return gateway.getTripsByDestination(destination);
  }
}
