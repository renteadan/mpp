package Tools;

import java.io.FileInputStream;
import java.util.Properties;
import Logger.LoggerManager;
public class PropertyLoader {
  private static Properties props;
  private static LoggerManager logger = new LoggerManager(PropertyLoader.class);
  public static String PASSWORD = getProperty("password");
  public static String USERNAME = getProperty("username");
  public static String SQL_URL = getProperty("sql_url");
  public static String DATA_SCHEMA = getProperty("data_schema");
  public static String LOGS_SCHEMA = getProperty("logs_schema");
  public static String POSTGRESQL_DRIVER = getProperty("postgresql_driver");

  private static void loadProperties() {
    if (props != null)
      return;
    props = new Properties();
    try {
      FileInputStream inputStream = new FileInputStream("src/main/resources/config.properties");
      props.load(inputStream);
    } catch (Exception e) {
      logger.error(e);
    }
  }

  private static String getProperty(String property) {
    loadProperties();
    return props.getProperty(property);
  }
}
