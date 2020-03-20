package Service;

import Domain.Destination;
import Domain.Trip;
import Gateway.TripGateway;

import java.util.Vector;

public class TripService extends BaseService<Trip, TripGateway> {
  public TripService() {
    super(new TripGateway());
  }

  public Vector<Trip> getTripsByDestination(Destination destination) {
    return gateway.getTripsByDestination(destination);
  }
}
