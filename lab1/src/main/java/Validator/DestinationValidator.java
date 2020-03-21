package Validator;

import Domain.Destination;
import Errors.ValidationError;

public class DestinationValidator implements ValidatorInterface<Destination> {
  @Override
  public void Validate(Destination entity) throws ValidationError {
    if(entity.getName().equals(""))
      throw new ValidationError("Invalid destination name!");
  }
}
