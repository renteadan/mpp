package Gateway;

import Domain.Destination;
import Errors.SQLErrorNoEntityFound;
import Logger.LoggerManager;
import Service.BaseService;
import net.bytebuddy.utility.RandomString;
import org.junit.jupiter.api.AfterAll;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;
import java.util.Vector;

import static org.junit.jupiter.api.Assertions.*;

class DestinationGatewayTest {

  private static LoggerManager loggerManager = new LoggerManager(DestinationGatewayTest.class);
  private static DestinationGateway gateway = new DestinationGateway();
  private static int beforeRecords;

  @BeforeAll
  static void setup() {
    beforeRecords = gateway.findAll().size();
  }

  @AfterAll
  static void cleanUp() {
    int currentRecords = gateway.findAll().size();
    if (currentRecords != beforeRecords) {
      loggerManager.error(new Exception("Not all test records were deleted!"));
      fail();
    }
    else {
      loggerManager.info(String.format("%s tests passed", DestinationGatewayTest.class.getCanonicalName()));
    }
  }
  @Test
  void find() {
    Destination dest = new Destination("Test1");
    dest = gateway.insert(dest);
    Destination dest2;
    try {
      dest2 = gateway.find(dest.getId());
      assertEquals(dest.getName(), dest2.getName());
    } catch (SQLErrorNoEntityFound ignored) {
      fail();
    }
    gateway.delete(dest);
    Destination finalDest = dest;
    assertThrows(SQLErrorNoEntityFound.class, () -> {
      gateway.find(finalDest.getId());
    });
  }

  @Test
  void delete() {
    Destination dest = new Destination("Test1");
    dest = gateway.insert(dest);
    gateway.delete(dest);
    Destination finalDest = dest;
    assertThrows(SQLErrorNoEntityFound.class, () -> {
      gateway.find(finalDest.getId());
    });
  }

  @Test
  void insert() {
    gateway.findAll();
    Vector<Destination> insertedDestinations = new Vector<>();
    Destination destination;
    for(int i=0; i<10 ;i++) {
      destination = new Destination(RandomString.make(10));
      destination = gateway.insert(destination);
      insertedDestinations.add(destination);
    }
    for (Destination d:insertedDestinations) {
      gateway.delete(d);
      assertThrows(SQLErrorNoEntityFound.class, () -> {
        gateway.find(d.getId());
      });
    }
  }

  @Test
  void update() {
    Destination dest = new Destination("Test1");
    dest = gateway.insert(dest);
    Destination dest2;
    try {
      assertEquals(gateway.find(dest.getId()).getName(), "Test1");
    } catch (SQLErrorNoEntityFound ignored) {
      fail();
    }
    dest.setName("Test2");
    dest2 = gateway.update(dest);
    try {
      assertEquals(gateway.find(dest.getId()).getName(), "Test2");
    } catch (SQLErrorNoEntityFound ignored) {
      fail();
    }
    assertEquals(dest2.getName(), dest.getName());
    gateway.delete(dest);
    Destination finalDest = dest;
    assertThrows(SQLErrorNoEntityFound.class, () -> {
      gateway.find(finalDest.getId());
    });
  }
}