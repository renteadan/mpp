package Service;

import Domain.Destination;
import Domain.Trip;
import Gateway.TripGateway;
import Validator.TripValidator;

import java.util.Vector;

public class TripService extends BaseService<Trip, TripGateway> {
  public TripService() {
    super();
  }

  public Vector<Trip> getTripsByDestination(Destination destination) {
    return super.getGateway().getTripsByDestination(destination);
  }

  public void setGateway(TripGateway gateway) {
    super.setGateway(gateway);
  }

  public void setValidator(TripValidator validator) {
    super.setValidator(validator);
  }
}
