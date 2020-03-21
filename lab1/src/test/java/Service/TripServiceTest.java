package Service;

import Domain.Destination;
import Domain.Trip;
import Errors.ValidationError;
import Logger.LoggerManager;
import net.bytebuddy.utility.RandomString;
import org.junit.jupiter.api.AfterAll;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;

import java.sql.Timestamp;
import java.time.LocalDateTime;
import java.util.Collections;
import java.util.Vector;

import static org.junit.jupiter.api.Assertions.*;

class TripServiceTest {

  private static TripService service = new TripService();
  private static DestinationService destinationService = new DestinationService();
  private static int initialCount;
  private static LoggerManager logger = new LoggerManager(TripServiceTest.class);
  private static Vector<Destination> allRecords = new Vector<>();
  @BeforeAll
  static void setUp() {
    initialCount = service.count();
  }

  private static void deleteAll() {
    for(Destination d: allRecords) {
      destinationService.delete(d);
    }
  }

  @AfterAll
  static void cleanUp() {
    deleteAll();
    int currentCount = service.count();
    if (currentCount != initialCount) {
      logger.error(new Exception("Not all test records were deleted!"));
      fail();
    } else {
      logger.info(String.format("%s tests passed", TripServiceTest.class.getCanonicalName()));
    }
  }

  @Test
  void getTripsByDestination() {
    Destination destination = new Destination(RandomString.make(10));
    try {
      destination = destinationService.insert(destination);
      allRecords.add(destination);
      Vector<Trip> trips = new Vector<>();
      for(int i=0;i<10;i++) {
        Trip trip = new Trip(Timestamp.valueOf(LocalDateTime.now().plusDays(1)), destination);
        trip = service.insert(trip);
        trips.add(trip);
      }
      Vector<Trip> trips1 = service.getTripsByDestination(destination);
      Collections.reverse(trips);
      assertArrayEquals(trips.toArray(), trips1.toArray());
    } catch (ValidationError validationError) {
      fail();
    }
  }
}