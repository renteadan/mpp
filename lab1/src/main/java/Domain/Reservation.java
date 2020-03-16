package Domain;

import org.jooq.Field;
import org.jooq.Table;
import org.jooq.impl.DSL;

import java.util.Objects;

public class Reservation extends BaseEntity {
  private String clientName;
  private Integer seatsNr;
  private Trip trip;

  private static final String tableName = "reservation";
  public static Table<?> TABLE = DSL.table(tableName);
  public static Field<Integer> ID = DSL.field("id", Integer.class);
  public static Field<String> CLIENT_NAME = DSL.field("client_name", String.class);
  public static Field<Integer> SEATS_NR = DSL.field("seats_nr", Integer.class);
  public static Field<Integer> TRIP_ID = DSL.field("trip_id", Integer.class);

  public Reservation(int id, String clientName, Integer seatsNr, Trip trip) {
    super(id);
    this.clientName = clientName;
    this.seatsNr = seatsNr;
    this.trip = trip;
  }

  public Reservation(String clientName, Integer seatsNr, Trip trip) {
    this.clientName = clientName;
    this.seatsNr = seatsNr;
    this.trip = trip;
  }

  @Override
  public boolean equals(Object o) {
    if (this == o) return true;
    if (!(o instanceof Reservation)) return false;
    Reservation that = (Reservation) o;
    return getClientName().equals(that.getClientName()) && getId() == that.getId();
  }

  @Override
  public int hashCode() {
    return Objects.hash(getClientName(), getId());
  }

  public String getClientName() {
    return clientName;
  }

  public void setClientName(String clientName) {
    this.clientName = clientName;
  }

  public int getSeatsNr() {
    return seatsNr;
  }

  public void setSeatsNr(Integer seatsNr) {
    this.seatsNr = seatsNr;
  }

  public Trip getTrip() {
    return trip;
  }

  public void setTrip(Trip trip) {
    this.trip = trip;
  }
}
