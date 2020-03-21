package Service;

import Domain.Destination;
import Gateway.DestinationGateway;
import Validator.DestinationValidator;

public class DestinationService extends BaseService<Destination, DestinationGateway> {
  public DestinationService(DestinationGateway gateway, DestinationValidator validator) {
    super(gateway, validator);
  }
}
