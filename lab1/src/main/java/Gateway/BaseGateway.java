package Gateway;

import java.sql.*;

public class BaseGateway {

  private Connection conn;

  private void setConn() {
    if (conn != null) {
      return;
    }
    try {
      conn =
          DriverManager.getConnection("jdbc:postgresql://rogue.db.elephantsql.com:5432/ygfxpsvi", "ygfxpsvi", "WCuZM01Z1gO8SKmGUqRcJviIcmP59pwy");
      conn.setSchema("mpp");
    } catch (SQLException ex) {
      // handle any errors
      System.out.println("SQLException: " + ex.getMessage());
      System.out.println("SQLState: " + ex.getSQLState());
      System.out.println("VendorError: " + ex.getErrorCode());
    }
  }

  public void closeConnection() {
    if (conn != null)  {
      try {
        conn.close();
        conn = null;
      } catch (SQLException e) {
        e.printStackTrace();
      }
    }
  }

  public ResultSet executeQuery(String query) throws SQLException {
    setConn();
    Statement st = conn.createStatement();
    ResultSet rs = st.executeQuery(query);
    closeConnection();
    return rs;
  }

  public void execute(String query) throws SQLException {
    setConn();
    Statement st = conn.createStatement();
    st.execute(query);
    closeConnection();
  }
}
