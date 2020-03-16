package Gateway;

import Domain.Destination;
import Errors.SQLErrorNoEntityFound;
import org.jooq.*;
import org.jooq.impl.DSL;

import java.util.Vector;


public class DestinationGateway extends BaseGateway implements GatewayInterface<Destination> {

  private Table<?> TABLE = Destination.TABLE;
  private DSLContext ctx = DSL.using(SQLDialect.POSTGRES);
  public DestinationGateway() {
    super();
  }

  public Destination insert(Destination destination) {
    InsertQuery<?> insertQuery = ctx.insertQuery(TABLE);
    insertQuery.addValue(Destination.NAME, destination.getName());
    insertQuery.setReturning(Destination.ID);
    Result<?> result = super.insertJooq(insertQuery);
    destination.setId(result.getValue(0, Destination.ID));
    return destination;
  }

  public Destination update(Destination destination) {
    UpdateQuery<?> updateQuery = ctx.updateQuery(TABLE);
    updateQuery.addValue(Destination.NAME, destination.getName());
    updateQuery.setReturning(Destination.NAME);
    updateQuery.addConditions(DSL.condition("id = ?", destination.getId()));
    Result<?> result = super.updateJooq(updateQuery);
    destination.setName(result.getValue(0, Destination.NAME));
    return destination;
  }

  public Destination find(int id) throws SQLErrorNoEntityFound {
    SelectQuery<?> selectQuery = ctx.selectQuery(TABLE);
    selectQuery.addConditions(DSL.condition("id = ?", id));
    Result<?> result = super.findJooq(selectQuery);
    if(result.isEmpty())
      throw new SQLErrorNoEntityFound("No destination was found!");
    return new Destination(result.getValue(0, Destination.ID), result.getValue(0, Destination.NAME));
  }

  public void delete(Destination destination) {
    DeleteQuery<?> deleteQuery = ctx.deleteQuery(TABLE);
    deleteQuery.addConditions(DSL.condition("id = ?", destination.getId()));
    super.deleteJooq(deleteQuery);
  }

  public Vector<Destination> findAll() {
    SelectQuery<?> selectQuery = ctx.selectQuery(TABLE);
    Result<?> result = super.findJooq(selectQuery);
    Vector<Destination> destinations = new Vector<>();
    for(Record row: result) {
      destinations.add(new Destination(row));
    }
    return destinations;
  }

  public Vector<Destination> findLastN(int n) {
    SelectQuery<?> selectQuery = ctx.selectQuery(TABLE);
    selectQuery.addOrderBy(Destination.ID.desc());
    selectQuery.addLimit(n);
    Result<?> result = super.findJooq(selectQuery);
    Vector<Destination> destinations = new Vector<>();
    for(Record row: result) {
      destinations.add(new Destination(row));
    }
    return destinations;
  }
}
