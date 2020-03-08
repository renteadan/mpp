package Gateway;

import java.sql.*;
import java.sql.Statement;

import Domain.BaseEntity;
import Logger.LoggerManager;
import Tools.PropertyLoader;
import org.jooq.*;
import org.jooq.impl.DSL;

class BaseGateway {

  private Field<Integer> ID = DSL.field("id", Integer.class);
  private LoggerManager logger = new LoggerManager(BaseGateway.class);
  private DSLContext context;
  private Connection conn;
  private String tableName;
  protected BaseGateway(String tableName) {
    this.tableName = tableName;
    setConn();
    context = DSL.using(conn, SQLDialect.POSTGRES);
  }
  private void setConn() {
    if (conn != null) {
      return;
    }
    try {
      conn =
          DriverManager.getConnection(PropertyLoader.getProperty("sql_url"), PropertyLoader.getProperty("username"), PropertyLoader.getProperty("password"));
      conn.setSchema(PropertyLoader.getProperty("data_schema"));
    } catch (SQLException e) {
      logger.error(e);
    }
  }

  public ResultSet executeQuery(String query) throws SQLException {
    Statement st = conn.createStatement();
    return st.executeQuery(query);
  }

  public ResultSet executeQueryPreparedStatement(PreparedStatement statement) throws SQLException {
    return statement.executeQuery();
  }

  protected ResultSet findById(int id) {
    String sql = String.format("select * from %s where id = ?;", tableName);
    try {
      PreparedStatement statement = conn.prepareStatement(sql);
      statement.setInt(1, id);
      return executeQueryPreparedStatement(statement);
    } catch (Exception e) {
      logger.error(e);
      return null;
    }
  }

  public void executePreparedStatement(PreparedStatement statement) throws SQLException {
    statement.execute();
  }

  public void deleteById(int id) {
    String sql = String.format("delete from %s where id = ?;", tableName);
    try {
      PreparedStatement statement = conn.prepareStatement(sql);
      statement.setInt(1, id);
      executePreparedStatement(statement);
    } catch (Exception e) {
      logger.error(e);
    }
  }

  protected  <E extends BaseEntity> E insert(E entity, InsertQuery<?> sql) {
    try {
      sql.setReturning(ID);
      context.execute(sql);
      Result<?> result = sql.getResult();
      entity.setId(result.getValue(0,ID));
      return entity;
    } catch (Exception e) {
      logger.error(e);
      return null;
    }
  }

  protected  <E extends BaseEntity> Result<?> update(E entity, UpdateQuery<?> sql) {
    try {
      sql.addConditions(DSL.condition("id = ?", entity.getId()));
      context.execute(sql);
      return sql.getResult();
    } catch (Exception e) {
      logger.error(e);
      return null;
    }
  }

  public ResultSet findAll() {
    String sql =String.format("select * from %s;", tableName);
    try {
      return executeQuery(sql);
    } catch (SQLException e) {
      logger.error(e);
      return null;
    }
  }
}
