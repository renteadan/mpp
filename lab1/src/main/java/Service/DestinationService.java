package Service;

import Domain.Destination;
import Gateway.DestinationGateway;

public class DestinationService extends BaseService<Destination> {
  public DestinationService() {
    super(new DestinationGateway());
  }

  public int count() {
    return super.findAll().size();
  }
}
