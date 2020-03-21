package Service;

import Domain.Destination;
import Gateway.DestinationGateway;
import Validator.DestinationValidator;

public class DestinationService extends BaseService<Destination, DestinationGateway> {
  public DestinationService() {
    super(new DestinationGateway(), new DestinationValidator());
  }
}
