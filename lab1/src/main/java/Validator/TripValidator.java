package Validator;

import Domain.Trip;
import Errors.ValidationError;
import java.sql.Timestamp;

public class TripValidator implements ValidatorInterface<Trip> {
  @Override
  public void Validate(Trip entity) throws ValidationError {
    if(entity.getDeparture().before(new Timestamp(System.currentTimeMillis())))
      throw new ValidationError("Can't create a trip in the past");
  }
}
