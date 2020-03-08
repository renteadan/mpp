package Gateway;

import Domain.Destination;
import Logger.LoggerManager;
import org.jooq.*;
import org.jooq.impl.DSL;

import java.sql.ResultSet;
import java.sql.SQLException;

public class DestinationGateway extends BaseGateway {

  private LoggerManager logger = new LoggerManager(DestinationGateway.class);
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

  public Destination find(int id) {
    ResultSet result = super.findById(id);
    if (result == null)
      return null;
    try {
      if(result.next()) return new Destination(result.getInt("id"), result.getString("name"));
      else return null;
    } catch (SQLException e) {
      logger.error(e);
      return null;
    }
  }
}
