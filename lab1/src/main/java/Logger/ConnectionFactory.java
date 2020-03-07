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
      dataSource.setUrl(PropertyLoader.getProperty("sql_url"));
      dataSource.setUsername(PropertyLoader.getProperty("username"));
      dataSource.setPassword(PropertyLoader.getProperty("password"));
      dataSource.setDefaultSchema(PropertyLoader.getProperty("logs_schema"));
      dataSource.setDriverClassName(PropertyLoader.getProperty("postgresql_driver"));
    }
    return dataSource.getConnection();
  }
}
