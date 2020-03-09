package Logger;

import Tools.PropertyLoader;
import org.apache.commons.dbcp2.BasicDataSource;

import java.sql.Connection;
import java.sql.SQLException;

public class ConnectionFactory {
  private static BasicDataSource dataSource;

  private ConnectionFactory() {}

  public static Connection getConnection() throws SQLException {
    if (dataSource == null) {
      dataSource = new BasicDataSource();
      dataSource.setUrl(PropertyLoader.SQL_URL);
      dataSource.setUsername(PropertyLoader.USERNAME);
      dataSource.setPassword(PropertyLoader.PASSWORD);
      dataSource.setDefaultSchema(PropertyLoader.LOGS_SCHEMA);
      dataSource.setDriverClassName(PropertyLoader.POSTGRESQL_DRIVER);
    }
    return dataSource.getConnection();
  }
}
