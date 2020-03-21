package Domain;

import Logger.LoggerManager;
import org.jooq.Field;
import org.jooq.Record;
import org.jooq.Table;
import org.jooq.impl.DSL;

import java.sql.Timestamp;
import java.util.Objects;

public class Trip extends BaseEntity {
  private Timestamp departure;
  private Destination destination;

  private static String tableName = "trip";
  public static Table<?> TABLE = DSL.table(tableName);
  public static Field<Integer> ID = DSL.field("id", Integer.class);
  public static Field<Timestamp> DEPARTURE = DSL.field("departure", Timestamp.class);
  public static Field<Integer> DESTINATION_ID = DSL.field("destination_id", Integer.class);

  public Trip(int id, Timestamp departure, Destination destination) {
    super(id);
    this.departure = departure;
    this.destination = destination;
  }

  public Trip(Timestamp departure, Destination destination) {
    super();
    this.departure = departure;
    this.destination = destination;
  }

  public Timestamp getDeparture() {
    return departure;
  }

  public void setDeparture(Timestamp departure) {
    this.departure = departure;
  }

  public Destination getDestination() {
    return destination;
  }

  public void setDestination(Destination destination) {
    this.destination = destination;
  }

  @Override
  public boolean equals(Object o) {
    if (this == o) return true;
    if (!(o instanceof Trip)) return false;
    Trip trip = (Trip) o;
    return getId() == trip.getId();
  }

  @Override
  public int hashCode() {
    return Objects.hash(getDestination(), getId());
  }
}
