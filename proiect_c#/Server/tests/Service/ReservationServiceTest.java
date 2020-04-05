package Service;

import Domain.Destination;
import Domain.Reservation;
import Domain.Trip;
import Errors.ValidationError;
import Logger.LoggerManager;
import org.junit.jupiter.api.AfterAll;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import java.sql.Timestamp;
import java.time.LocalDateTime;
import java.util.Vector;

import static org.junit.jupiter.api.Assertions.*;

class ReservationServiceTest {

  private static ApplicationContext factory = new ClassPathXmlApplicationContext("BeanFactory.xml");
  private static LoggerManager loggerManager = new LoggerManager(ReservationServiceTest.class);
  private static ReservationService service = factory.getBean(ReservationService.class);
  private static DestinationService destinationService = factory.getBean(DestinationService.class);
  private static TripService tripService = factory.getBean(TripService.class);
  private static int beforeRecords;
  private static Vector<Destination> allDestinations = new Vector<>();

  private static void deleteAll() {
    for(Destination d: allDestinations) {
      destinationService.delete(d);
    }
  }

  @BeforeAll
  static void setup() {
    beforeRecords = service.findAll().size();
  }

  @AfterAll
  static void cleanUp() {
    deleteAll();
    int currentRecords = service.findAll().size();
    if (currentRecords != beforeRecords) {
      loggerManager.error(new Exception("Not all test records were deleted!"));
      fail();
    }
    else {
      loggerManager.info(String.format("%s tests passed", ReservationServiceTest.class.getCanonicalName()));
    }
  }

  @Test
  void getReservationsByTrip() {
    try {
      Destination dest = new Destination("TestResService");
      dest = destinationService.insert(dest);
      allDestinations.add(dest);
      Trip trip = new Trip(Timestamp.valueOf(LocalDateTime.now().plusDays(1)), dest);
      trip = tripService.insert(trip);
      Reservation reservation = new Reservation("Reservation Service Test", 7, trip);
      service.insert(reservation);
      reservation = new Reservation("Client 2", 9, trip);
      service.insert(reservation);
      Vector<Reservation> reservations = service.getReservationsByTrip(trip);
      int seats = 0;
      for(Reservation res:reservations) {
        seats+=res.getSeatsNr();
      }
      assertEquals(seats, 16);
    } catch (ValidationError ignored) {
      fail();
    }
  }
}