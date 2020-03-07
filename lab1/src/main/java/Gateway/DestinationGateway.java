package Gateway;

import Domain.Destination;
import org.jooq.*;
import org.jooq.impl.DSL;
public class DestinationGateway extends BaseGateway {

  private static String tableName = "destination";
  private Field<String> NAME = DSL.field("name", String.class);
  private Table<?> TABLE = DSL.table(tableName);
  private DSLContext ctx = DSL.using(SQLDialect.POSTGRES);
  public DestinationGateway() {
    super(tableName);
  }

  public Destination insert(Destination destination) {
    InsertQuery<?> insertQuery = ctx.insertQuery(TABLE);
    insertQuery.addValue(NAME, destination.getName());
    return super.insert(destination, insertQuery);
  }

  public Destination update(Destination destination) {
    UpdateQuery<?> updateQuery = ctx.updateQuery(TABLE);
    updateQuery.addValue(NAME, destination.getName());
    updateQuery.setReturning(NAME);
    Result<?> result = super.update(destination, updateQuery);
    if(result == null)
      return null;
    destination.setName(result.getValue(0, NAME));
    return destination;
  }
}
