
using System;

public class TripValidator: IValidator<Trip> {
  public void Validate(Trip entity) {
    if(entity.Departure.CompareTo(DateTime.Now) < 0)
      throw new ValidationError("Can't create a trip in the past");
  }
}
