
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
    Destination destination = new Destination(18, "satu mare");

    Destination destination2 = dest.update(destination);
    System.out.println(destination2.toString());
  }
  public static void jdbc() {
    sqlTest();
  }
}
