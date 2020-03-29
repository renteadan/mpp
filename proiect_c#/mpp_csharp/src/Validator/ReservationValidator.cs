
public class ReservationValidator: IValidator<Reservation> {
  public void Validate(Reservation entity) {
    string errors = "";
    if(entity.SeatsNr < 0 || entity.SeatsNr > 18)
      errors += "You can't reserve that many seats!\n";
    if(entity.ClientName.Equals(""))
      errors += "Invalid client name!";
    if(!errors.Equals(""))
      throw new ValidationError(errors);
  }
}
