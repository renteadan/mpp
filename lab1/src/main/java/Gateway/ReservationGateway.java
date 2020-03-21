package Gateway;
import Domain.Destination;
import Domain.Reservation;
import Domain.Trip;
import Errors.SQLErrorNoEntityFound;
import Logger.LoggerManager;
import org.jooq.*;
import org.jooq.impl.DSL;

import java.util.Vector;

public class ReservationGateway extends BaseGateway implements GatewayInterface<Reservation> {

  private Table<?> TABLE = Reservation.TABLE;
  private DSLContext ctx = DSL.using(SQLDialect.POSTGRES);
  private TripGateway tripGateway = new TripGateway();
  private LoggerManager logger = new LoggerManager(ReservationGateway.class);

  @Override
  public Reservation find(int id) throws SQLErrorNoEntityFound {
    SelectQuery<?> selectQuery = ctx.selectQuery(TABLE);
    selectQuery.addConditions(DSL.condition("? = ?",Reservation.ID, id));
    Result<?> result = super.findJooq(selectQuery);
    if(result.isEmpty())
      throw new SQLErrorNoEntityFound("No reservation found with this id!");
    return createReservation(result.get(0));
  }

  private Reservation createReservation(Record result) throws SQLErrorNoEntityFound {
    Trip trip = tripGateway.find(result.getValue(Reservation.TRIP_ID));
    return new Reservation(result.getValue(Reservation.ID), result.getValue(Reservation.CLIENT_NAME), result.getValue(Reservation.SEATS_NR), trip);
  }

  private Vector<Reservation> createReservations(Result<?> result) {
    Vector<Reservation> reservations = new Vector<>();
    for(Record row: result) {
      try {
        reservations.add(createReservation(row));
      } catch (SQLErrorNoEntityFound ignored) {
      }
    }
    return reservations;
  }

  @Override
  public void delete(Reservation entity) {
      DeleteQuery<?> deleteQuery = ctx.deleteQuery(TABLE);
      deleteQuery.addConditions(DSL.condition("id = ?", entity.getId()));
      super.deleteJooq(deleteQuery);
  }

  @Override
  public Reservation update(Reservation entity) {
    UpdateQuery<?> updateQuery = ctx.updateQuery(TABLE);
    updateQuery.addValue(Reservation.CLIENT_NAME, entity.getClientName());
    updateQuery.addValue(Reservation.SEATS_NR, entity.getSeatsNr());
    updateQuery.addValue(Reservation.TRIP_ID, entity.getTrip().getId());
    updateQuery.setReturning(Reservation.CLIENT_NAME, Reservation.SEATS_NR, Reservation.TRIP_ID);
    updateQuery.addConditions(DSL.condition("id = ?", entity.getId()));
    Result<?> result = super.updateJooq(updateQuery);
    entity.setClientName(result.getValue(0, Reservation.CLIENT_NAME));
    entity.setSeatsNr(result.getValue(0, Reservation.SEATS_NR));
    try {
      Trip trip = tripGateway.find(result.getValue(0, Reservation.TRIP_ID));
      entity.setTrip(trip);
      return entity;
    } catch (SQLErrorNoEntityFound e) {
      logger.error(e);
    }
    return entity;
  }

  @Override
  public Reservation insert(Reservation entity) {
    InsertQuery<?> insertQuery = ctx.insertQuery(TABLE);
    insertQuery.addValue(Reservation.CLIENT_NAME, entity.getClientName());
    insertQuery.addValue(Reservation.SEATS_NR, entity.getSeatsNr());
    insertQuery.addValue(Reservation.TRIP_ID, entity.getTrip().getId());
    insertQuery.setReturning(Reservation.ID);
    Result<?> result = super.insertJooq(insertQuery);
    entity.setId(result.getValue(0, Reservation.ID));
    return entity;
  }

  @Override
  public Vector<Reservation> findAll() {
    SelectQuery<?> selectQuery = ctx.selectQuery(TABLE);
    Result<?> result = super.findJooq(selectQuery);
    return createReservations(result);
  }

  @Override
  public Vector<Reservation> findLastN(int n) {
    SelectQuery<?> selectQuery = ctx.selectQuery(TABLE);
    selectQuery.addOrderBy(Reservation.ID.desc());
    selectQuery.addLimit(n);
    Result<?> result = super.findJooq(selectQuery);
    return createReservations(result);
  }

  public Vector<Reservation> getReservationsByTrip(Trip trip) {
    SelectQuery<?> selectQuery = ctx.selectQuery(TABLE);
    selectQuery.addConditions(DSL.condition("? = ?", Reservation.TRIP_ID, trip.getId()));
    Result<?> result = super.findJooq(selectQuery);
    return createReservations(result);
  }
}
