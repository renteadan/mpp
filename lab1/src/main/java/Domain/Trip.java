package Domain;

import Logger.LoggerManager;
import org.jooq.Field;
import org.jooq.Record;
import org.jooq.Table;
import org.jooq.impl.DSL;

import java.sql.Time;
import java.sql.Timestamp;
import java.time.LocalDateTime;

public class Trip extends BaseEntity {
  private LoggerManager logger = new LoggerManager(Trip.class);
  private Timestamp departure;
  private Destination destination;
  private int destination_id;

  public int getDestination_id() {
    return destination_id;
  }

  public void setDestination_id(int destination_id) {
    this.destination_id = destination_id;
  }

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

  public Trip(Record record) {
    super(record.getValue(ID));
    this.departure = record.getValue(DEPARTURE);
    this.destination_id = record.getValue(DESTINATION_ID);
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
}
