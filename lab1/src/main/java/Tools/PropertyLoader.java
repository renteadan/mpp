package Tools;

import java.io.FileInputStream;
import java.util.Properties;
import Logger.LoggerManager;
public class PropertyLoader {
  private static Properties props;
  private static LoggerManager logger = new LoggerManager(PropertyLoader.class);

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

  public static String getProperty(String property) {
    loadProperties();
    return props.getProperty(property);
  }
}
