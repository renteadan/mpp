package Gateway;

import java.sql.*;

import Domain.BaseEntity;
import Logger.LoggerManager;
import Tools.PropertyLoader;
import org.jooq.DSLContext;
import org.jooq.SQLDialect;
import org.jooq.impl.DSL;

class BaseGateway {

  private LoggerManager logger = new LoggerManager(BaseGateway.class);

  private Connection conn;
  private String tableName;
  protected BaseGateway(String tableName) {
    this.tableName = tableName;
    setConn();
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

  public ResultSet findById(int id) {
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

  public boolean executePreparedStatement(PreparedStatement statement) throws SQLException {
    return statement.execute();
  }

  public boolean deleteById(int id) {
    String sql = String.format("delete from %s where id = ?;", tableName);
    try {
      PreparedStatement statement = conn.prepareStatement(sql);
      statement.setInt(1, id);
      return executePreparedStatement(statement);
    } catch (Exception e) {
      logger.error(e);
      return false;
    }
  }

  public <E extends BaseEntity> E insert(E entity, DataPacketConverter<E> converter) {
    String sql = String.format("insert into %s values;", tableName);
    DSLContext ctx = DSL.using(conn, SQLDialect.POSTGRES);
    try {
      PreparedStatement preparedStatement = conn.prepareStatement(sql, PreparedStatement.RETURN_GENERATED_KEYS);
      converter.toDataPacket(entity, preparedStatement);
      int rowsAffected = preparedStatement.executeUpdate();
      if (rowsAffected == 0) {
        logger.error(new SQLException(String.format("Inserting %s failed", entity.getClass().getCanonicalName())));
        return null;
      }
      try (ResultSet generatedKeys = preparedStatement.getGeneratedKeys()) {
        if(generatedKeys.next()) {
          entity.setId(generatedKeys.getInt(1));
        } else {
          logger.error(new SQLException(String.format("Inserting %s failed", entity.getClass().getCanonicalName())));
          return null;
        }
      }
      return entity;
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
