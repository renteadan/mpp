package Gateway;

import Domain.Destination;
import Domain.Trip;
import Errors.SQLErrorNoEntityFound;
import Logger.LoggerManager;
import org.jooq.*;
import org.jooq.impl.DSL;
import java.util.Vector;

public class TripGateway extends BaseGateway implements GatewayInterface<Trip> {
  private Table<?> TABLE = Trip.TABLE;
  private DSLContext ctx = DSL.using(SQLDialect.POSTGRES);
  private DestinationGateway destinationGateway = new DestinationGateway();
  private LoggerManager logger = new LoggerManager(TripGateway.class);

  public TripGateway() {
    super();
  }
  @Override
  public Trip find(int id) throws SQLErrorNoEntityFound {
    SelectQuery<?> selectQuery = ctx.selectQuery(TABLE);
    selectQuery.addConditions(DSL.condition("? = ?", Trip.ID, id));
    Result<?> result = super.findJooq(selectQuery);
    if(result.isEmpty())
      throw new SQLErrorNoEntityFound("No trip found with this id!");
    return createTrip(result.get(0));
  }

  private Trip createTrip(Record result) throws SQLErrorNoEntityFound {
    Destination destination = destinationGateway.find(result.getValue(Trip.DESTINATION_ID));
    return new Trip(result.getValue(Trip.ID), result.getValue(Trip.DEPARTURE), destination);
  }

  @Override
  public void delete(Trip entity) {
    DeleteQuery<?> deleteQuery = ctx.deleteQuery(TABLE);
    deleteQuery.addConditions(DSL.condition("id = ?", entity.getId()));
    super.deleteJooq(deleteQuery);
  }

  @Override
  public Trip update(Trip entity) {
    UpdateQuery<?> updateQuery = ctx.updateQuery(TABLE);
    updateQuery.addValue(Trip.DEPARTURE, entity.getDeparture());
    updateQuery.addValue(Trip.DESTINATION_ID, entity.getDestination().getId());
    updateQuery.setReturning(Trip.DEPARTURE, Trip.DESTINATION_ID);
    updateQuery.addConditions(DSL.condition("id = ?", entity.getId()));
    Result<?> result = super.updateJooq(updateQuery);
    entity.setDeparture(result.getValue(0, Trip.DEPARTURE));
    try {
      Destination destination = destinationGateway.find(result.getValue(0, Trip.DESTINATION_ID));
      entity.setDestination(destination);
      return entity;
    } catch (SQLErrorNoEntityFound e) {
      logger.error(e);
    }
    return entity;
  }

  @Override
  public Trip insert(Trip entity) {
    InsertQuery<?> insertQuery = ctx.insertQuery(TABLE);
    insertQuery.addValue(Trip.DEPARTURE, entity.getDeparture());
    insertQuery.addValue(Trip.DESTINATION_ID, entity.getDestination().getId());
    insertQuery.setReturning(Trip.ID);
    Result<?> result = super.insertJooq(insertQuery);
    entity.setId(result.getValue(0, Trip.ID));
    return entity;
  }

  @Override
  public Vector<Trip> findAll() {
    SelectQuery<?> selectQuery = ctx.selectQuery(TABLE);
    Result<?> result = super.findJooq(selectQuery);
    Vector<Trip> trips = new Vector<>();
    for(Record row: result) {
      try {
        trips.add(createTrip(row));
      } catch (SQLErrorNoEntityFound ignored) {
      }
    }
    return trips;
  }

  @Override
  public Vector<Trip> findLastN(int n) {
    SelectQuery<?> selectQuery = ctx.selectQuery(TABLE);
    selectQuery.addOrderBy(Trip.ID.desc());
    selectQuery.addLimit(n);
    Result<?> result = super.findJooq(selectQuery);
    Vector<Trip> trips = new Vector<>();
    for(Record row: result) {
      try {
        trips.add(createTrip(row));
      } catch (SQLErrorNoEntityFound ignored) {
      }
    }
    return trips;
  }
}
