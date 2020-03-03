import Domain.DestinationEntity;
import Gateway.BaseGateway;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.boot.MetadataSources;
import org.hibernate.boot.registry.StandardServiceRegistry;
import org.hibernate.boot.registry.StandardServiceRegistryBuilder;

import java.sql.*;

public class Main {
  public static void main(String[] args) {
    jdbc();
  }

  public int sum(int a, int b) {
    return a+b;
  }

  public static void sqlTest() throws SQLException {
    BaseGateway gt = new BaseGateway();
    Connection conn = gt.getConn();
    Statement st;
    st = conn.createStatement();
    ResultSet rs = st.executeQuery("SELECT * from destination");
    Statement st2 = conn.createStatement();
    st2.execute("INSERT INTO destination(destination) values ('Bucuresti')");
    while(rs.next()) {
      System.out.println(rs.getString(2));
    }
    st.close();
    gt.closeConnection();
  }

  public static void jdbc() {
    sqlTest();
  }
}
