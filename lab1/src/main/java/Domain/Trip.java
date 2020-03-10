package Domain;

import Logger.LoggerManager;
import org.jooq.Field;
import org.jooq.ForeignKey;
import org.jooq.Record;
import org.jooq.Table;
import org.jooq.impl.DSL;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.time.LocalDateTime;
import java.util.Map;

public class Trip extends BaseEntity {
  private LoggerManager logger = new LoggerManager(Trip.class);
  private LocalDateTime departure;
  private Destination destination;
  private static String tableName = "trip";
  public static Field<LocalDateTime> DEPARTURE = DSL.field("departure", LocalDateTime.class);
  public static Field<Integer> DESTINATION_ID = DSL.field("destination_id", Integer.class);
  public static Table<?> TABLE = DSL.table(tableName);
  public Trip(int id, LocalDateTime departure, Destination destination) {
    super(id);
    this.departure = departure;
    this.destination = destination;
  }

  public Trip(LocalDateTime departure, Destination destination) {
    super();
    this.departure = departure;
    this.destination = destination;
  }

  public Trip(Map<?,?> resultSet) throws SQLException {
    super(1);
    this.departure = resultSet.getTimestamp(1).toLocalDateTime();
    this.destination = new Destination(resultSet.getInt(3), resultSet.getString(4));
  }

  public LocalDateTime getDeparture() {
    return departure;
  }

  public void setDeparture(LocalDateTime departure) {
    this.departure = departure;
  }

  public Destination getDestination() {
    return destination;
  }

  public void setDestination(Destination destination) {
    this.destination = destination;
  }
}
