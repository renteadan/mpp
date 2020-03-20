package Service;

import Domain.Destination;
import Gateway.DestinationGateway;

public class DestinationService extends BaseService<Destination, DestinationGateway> {
  public DestinationService() {
    super(new DestinationGateway());
  }
}
