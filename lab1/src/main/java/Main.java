import Domain.Destination;
import Domain.Trip;
import Gateway.DestinationGateway;
import Gateway.TripGateway;

import java.time.LocalDateTime;

public class Main {
  public static void main(String[] args) {
    Destination destination =  new Destination("Cluj2");
    DestinationGateway destinationGateway = new DestinationGateway();
    destination = destinationGateway.insert(destination);
    Trip trip = new Trip( LocalDateTime.now(), destination);
    TripGateway gateway = new TripGateway();
    trip = gateway.insert(trip);
    Trip trip2 = gateway.find(trip.getId());
    System.out.println(trip2.getDestination().getName());
  }
}
