
public class DestinationValidator: IValidator<Destination> {
  public void Validate(Destination entity) {
    if(entity.Name.Equals(""))
      throw new ValidationError("Invalid destination name!");
  }
}
