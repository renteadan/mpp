package Gateway;

import Domain.Destination;
import Domain.Reservation;
import Domain.Trip;
import Errors.SQLErrorNoEntityFound;
import Logger.LoggerManager;
import org.junit.jupiter.api.AfterAll;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;

import java.sql.Timestamp;
import java.time.LocalDateTime;
import java.util.Collections;
import java.util.Random;
import java.util.Vector;

import static org.junit.jupiter.api.Assertions.*;

class ReservationGatewayTest {

  private static LoggerManager loggerManager = new LoggerManager(DestinationGatewayTest.class);
  private static ReservationGateway gateway = new ReservationGateway();
  private static DestinationGateway destinationGateway = new DestinationGateway();
  private static TripGateway tripGateway = new TripGateway();
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
    try {
      Destination dest = new Destination("TestResGateway");
      dest = destinationGateway.insert(dest);
      allDestinations.add(dest);
      Trip trip = new Trip(Timestamp.valueOf(LocalDateTime.now()), dest);
      trip = tripGateway.insert(trip);
      Reservation reservation = new Reservation("Reservation Gateway Test", 7, trip);
      reservation = gateway.insert(reservation);
      Reservation reservation1 = gateway.find(reservation.getId());
      assertEquals(reservation, reservation1);
    } catch (SQLErrorNoEntityFound ignored) {
      fail();
    }
  }

  @Test
  void delete() {
    Destination dest = new Destination("TestResGateway");
    dest = destinationGateway.insert(dest);
    allDestinations.add(dest);
    Trip trip = new Trip(Timestamp.valueOf(LocalDateTime.now()), dest);
    trip = tripGateway.insert(trip);
    Reservation reservation = new Reservation("Reservation Gateway Test", 7, trip);
    reservation = gateway.insert(reservation);
    gateway.delete(reservation);
    Reservation finalReservation = reservation;
    assertThrows(SQLErrorNoEntityFound.class, () -> {
      gateway.find(finalReservation.getId());
    });
  }

  @Test
  void update() {
    Destination dest = new Destination("TestResGateway");
    dest = destinationGateway.insert(dest);
    allDestinations.add(dest);
    Trip trip = new Trip(Timestamp.valueOf(LocalDateTime.now()), dest);
    trip = tripGateway.insert(trip);
    Reservation reservation = new Reservation("Reservation Gateway Test", 7, trip);
    reservation = gateway.insert(reservation);
    reservation.setSeatsNr(10);
    reservation = gateway.update(reservation);
    assertEquals(reservation.getSeatsNr(), 10);
  }

  @Test
  void insert() {
    Destination dest = new Destination("TestResGateway");
    dest = destinationGateway.insert(dest);
    allDestinations.add(dest);
    Trip trip = new Trip(Timestamp.valueOf(LocalDateTime.now()), dest);
    trip = tripGateway.insert(trip);
    Vector<Reservation> reservations = new Vector<>();
    for(int i=0; i<10; i++) {
      Reservation reservation = new Reservation("Reservation Gateway Test",12, trip);
      reservation = gateway.insert(reservation);
      reservations.add(reservation);
    }
    Collections.reverse(reservations);
    assertArrayEquals(gateway.findLastN(10).toArray(), reservations.toArray());
  }
}