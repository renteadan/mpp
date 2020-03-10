package Gateway;

import Domain.Destination;
import Domain.Trip;
import Logger.LoggerManager;
import org.jooq.*;
import org.jooq.impl.DSL;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Map;
import java.util.Vector;

public class TripGateway extends BaseGateway implements GatewayInterface<Trip> {
  private Table<?> TABLE = Trip.TABLE;
  private DSLContext ctx = DSL.using(SQLDialect.POSTGRES);
  private LoggerManager logger = new LoggerManager(TripGateway.class);

  public TripGateway() {
    super();
  }
  @Override
  public Trip find(int id) {
    SelectQuery<?> selectQuery = ctx.selectQuery(TABLE);
    Condition condition = DSL.condition("?.id = ?.?", Destination.TABLE, Trip.TABLE, Trip.DESTINATION_ID);
    selectQuery.addJoin(Destination.TABLE, condition);
    selectQuery.addConditions(DSL.condition("?.? = ?", Trip.TABLE, Trip.ID, id));
    Result<?> result = super.findJooq(selectQuery);
    Trip trip = null;
    Map<?,?> map = result.intoMap(Trip.TABLE);

    return trip;
  }

  @Override
  public void delete(Trip entity) {

  }

  @Override
  public Trip update(Trip entity) {
    return null;
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
    return null;
  }

  @Override
  public Vector<Trip> findLastN(int n) {
    return null;
  }
}
