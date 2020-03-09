package Gateway;
import java.sql.*;
import Logger.LoggerManager;
import Tools.PropertyLoader;
import org.jooq.*;
import org.jooq.impl.DSL;

class BaseGateway {

  private LoggerManager logger = new LoggerManager(BaseGateway.class);
  private DSLContext context;
  private static Connection conn;
  protected BaseGateway() {
    setConn();
    context = DSL.using(conn, SQLDialect.POSTGRES);
  }
  private void setConn() {
    try {
      if (conn == null || conn.isClosed()) {
        conn = DriverManager.getConnection(PropertyLoader.SQL_URL, PropertyLoader.USERNAME, PropertyLoader.PASSWORD);
        conn.setSchema(PropertyLoader.DATA_SCHEMA);
      }
    } catch (SQLException e) {
      logger.error(e);
    }
  }


  protected void deleteJooq(DeleteQuery<?> sql) {
    context.execute(sql);
  }

  protected Result<?> insertJooq(InsertQuery<?> sql) {
    context.execute(sql);
    return sql.getResult();
  }

  protected Result<?> updateJooq(UpdateQuery<?> sql) {
    context.execute(sql);
    return sql.getResult();
  }

  protected Result<?> findJooq(SelectQuery<?> sql) {
    context.execute(sql);
    return sql.getResult();
  }
}
