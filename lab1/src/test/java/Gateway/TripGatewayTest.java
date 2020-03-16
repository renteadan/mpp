package Gateway;

import Domain.Destination;
import Domain.Trip;
import Errors.SQLErrorNoEntityFound;
import Logger.LoggerManager;
import org.junit.jupiter.api.AfterAll;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;
import java.sql.Timestamp;
import java.time.LocalDateTime;
import java.util.Vector;

import static org.junit.jupiter.api.Assertions.*;

class TripGatewayTest {

  private static LoggerManager loggerManager = new LoggerManager(TripGatewayTest.class);
  private static DestinationGateway destinationGateway = new DestinationGateway();
  private static TripGateway gateway = new TripGateway();
  private static int beforeRecords;
  private static Vector<Destination> allDestinations = new Vector<>();

  private static void deleteAll() {
    for(Destination d: allDestinations) {
      destinationGateway.delete(d);
    }
  }

  @BeforeAll
  static void setup() {
    beforeRecords = gateway.findAll().size();
  }

  @AfterAll
  static void cleanUp() {
    deleteAll();
    int currentRecords = gateway.findAll().size();
    if (currentRecords != beforeRecords) {
      loggerManager.error(new Exception("Not all test records were deleted!"));
      fail();
    }
    else {
      loggerManager.info(String.format("%s tests passed", TripGatewayTest.class.getCanonicalName()));
    }
  }

  @Test
  void find() {
    Destination dest = new Destination("testTripGateway");
    dest = destinationGateway.insert(dest);
    allDestinations.add(dest);
    Trip trip = new Trip(Timestamp.valueOf(LocalDateTime.now()), dest);
    trip = gateway.insert(trip);
    try {
      Trip trip2 = gateway.find(trip.getId());
      assertEquals(trip, trip2);
    } catch (SQLErrorNoEntityFound e) {
      loggerManager.error(e);
      fail();
    }
  }

  @Test
  void delete() {
    Destination dest = new Destination("testTripGateway");
    dest = destinationGateway.insert(dest);
    allDestinations.add(dest);
    Trip trip = new Trip(Timestamp.valueOf(LocalDateTime.now()), dest);
    trip = gateway.insert(trip);
    gateway.delete(trip);
    Trip finalTrip = trip;
    assertThrows(SQLErrorNoEntityFound.class, () -> {
      gateway.find(finalTrip.getId());
    });
  }

  @Test
  void update() {
    Destination dest = new Destination("testTripGateway");
    dest = destinationGateway.insert(dest);
    allDestinations.add(dest);
    Trip trip = new Trip(Timestamp.valueOf(LocalDateTime.now()), dest);
    trip = gateway.insert(trip);
    trip.setDeparture(Timestamp.valueOf("2019-01-01 00:05:00"));
    trip = gateway.update(trip);
    try {
      trip=gateway.find(trip.getId());
      assertEquals(trip.getDeparture(), Timestamp.valueOf("2019-01-01 00:05:00"));
    } catch (SQLErrorNoEntityFound e) {
      fail();
    }
  }

  @Test
  void insert() {
    Destination dest = new Destination("testTripGateway");
    dest = destinationGateway.insert(dest);
    allDestinations.add(dest);
    Trip trip = new Trip(Timestamp.valueOf(LocalDateTime.now()), dest);
    trip = gateway.insert(trip);
    try {
      Trip trip2 = gateway.find(trip.getId());
      assertEquals(trip, trip2);
    } catch (SQLErrorNoEntityFound e) {
      loggerManager.error(e);
      fail();
    }
  }
}