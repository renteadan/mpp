package Validator;

import Domain.Reservation;
import Errors.ValidationError;

public class ReservationValidator implements ValidatorInterface<Reservation> {
  @Override
  public void Validate(Reservation entity) throws ValidationError {
    String errors = "";
    if(entity.getSeatsNr() < 0 || entity.getSeatsNr() > 18)
      errors = errors.concat("You can't reserve that many seats!\n");
    if(entity.getClientName().equals(""))
      errors = errors.concat("Invalid client name!");
    if(!errors.equals(""))
      throw new ValidationError(errors);
  }
}
