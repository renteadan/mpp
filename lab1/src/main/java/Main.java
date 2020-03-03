
import Gateway.BaseGateway;


import java.sql.*;

public class Main {
  public static void main(String[] args) throws SQLException {
    jdbc();
  }

  public int sum(int a, int b) {
    return a+b;
  }

  public static void sqlTest()  {
    BaseGateway gt = new BaseGateway();
    ResultSet rs;
    try {
      rs = gt.executeQuery("SELECT * from destination");
//      gt.execute("INSERT INTO destination(name) values ('Bucuresti')");
      while(rs.next()) {
        System.out.println(rs.getString(2));
      }
    } catch (SQLException e) {
      e.printStackTrace();
    }
  }

  public static void jdbc() {
    sqlTest();
  }
}
