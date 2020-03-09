package Service;

import Domain.Destination;
import Logger.LoggerManager;
import net.bytebuddy.utility.RandomString;
import org.junit.jupiter.api.AfterAll;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;

import java.util.Collections;
import java.util.Vector;

import static org.junit.jupiter.api.Assertions.*;

class DestinationServiceTest {

  private static DestinationService service = new DestinationService();
  private static int initialCount;
  private static LoggerManager logger = new LoggerManager(DestinationServiceTest.class);
  private static Vector<Destination> allRecords = new Vector<>();
  @BeforeAll
  static void setUp() {
    initialCount = service.count();
  }

  private static void deleteAll() {
    for(Destination d: allRecords) {
      service.delete(d);
    }
  }

  @AfterAll
  static void cleanUp() {
    deleteAll();
    int currentCount = service.count();
    if (currentCount != initialCount) {
      logger.error(new Exception("Not all test records were deleted!"));
      fail();
    }
  }

  @Test
  void find() {
    Destination destination = new Destination(RandomString.make(10));
    destination = service.insert(destination);
    allRecords.add(destination);
    Destination destination1 = service.find(destination.getId());
    assertEquals(destination, destination1);
  }

  @Test
  void delete() {
    Vector<Destination> destinations = new Vector<>();
    for(int i=0;i<20;i++) {
      Destination destination = new Destination(RandomString.make(20));
      destination = service.insert(destination);
      destinations.add(destination);
      allRecords.add(destination);
    }

    for(Destination d:destinations) {
      service.delete(d);
      allRecords.remove(d);
      d = service.find(d.getId());
      assertNull(d);
    }
  }

  @Test
  void update() {
    Vector<Destination> destinations = new Vector<>();
    Vector<Destination> destinations1 = new Vector<>();
    for(int i=0;i<20;i++) {
      Destination destination = new Destination(RandomString.make(20));
      destination = service.insert(destination);
      destinations.add(destination);
      allRecords.add(destination);
    }
    for(Destination d:destinations) {
      Destination destination = new Destination(d.getId(), RandomString.make(20));
      destination = service.update(destination);
      destinations1.add(destination);
    }
    destinations = service.findLastN(20);
    Collections.reverse(destinations);
    assertArrayEquals(destinations.toArray(), destinations1.toArray());
  }

  @Test
  void insert() {
    int count = service.count();
    for(int i=0;i<20;i++) {
      Destination destination = new Destination(RandomString.make(20));
      destination = service.insert(destination);
      allRecords.add(destination);
    }
    assertEquals(count, service.count() - 20);
  }
}