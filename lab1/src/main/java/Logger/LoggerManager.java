package Logger;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

public class LoggerManager {
  private Logger logger;

  public void error(Exception e) {
    logger.error(e.getClass().getCanonicalName(), e);
  }

  public void info(String info) {
    logger.info(info);
  }

  public LoggerManager(Class<?> instanceClass) {
    logger = LogManager.getLogger(instanceClass);
  }
}
