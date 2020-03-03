package Gateway;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class BaseGateway {

  private Connection conn;

  public Connection getConn() {
    if (conn != null) {
      return conn;
    }
    try {
      conn =
          DriverManager.getConnection("jdbc:mysql://localhost/mpp?" +
              "user=root&password=admin");
    } catch (SQLException ex) {
      // handle any errors
      System.out.println("SQLException: " + ex.getMessage());
      System.out.println("SQLState: " + ex.getSQLState());
      System.out.println("VendorError: " + ex.getErrorCode());
    }
    return conn;
  }

  public void closeConnection() {
    if (conn != null)  {
      try {
        conn.close();
      } catch (SQLException e) {
        e.printStackTrace();
      }
    }
  }
}
