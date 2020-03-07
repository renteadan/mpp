
import Domain.Destination;
import Gateway.DestinationGateway;
import Logger.LoggerManager;


import java.sql.*;

public class Main {

  private static LoggerManager logger = new LoggerManager(Main.class);
  public static void main(String[] args) throws SQLException {
    jdbc();
  }

  public int sum(int a, int b) {
    return a+b;
  }

  public static void sqlTest() {
    DestinationGateway dest = new DestinationGateway();
    ResultSet rs = dest.findAll();
    Destination destination = new Destination("cluj");
    dest.insert(destination);
    try {
      while (rs.next()) {
        System.out.print(rs.getString("name") + " ");
        System.out.println(rs.getInt("id"));
        dest.deleteById(rs.getInt("id"));
      }
    } catch (Exception e) {
      logger.error(e);
    }
  }
  public static void jdbc() {
    sqlTest();
  }
}
