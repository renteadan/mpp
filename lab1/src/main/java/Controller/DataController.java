package Controller;

import Domain.Reservation;
import Domain.Trip;
import Service.ReservationService;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;


public class DataController {
  public TableView<Reservation> reservationsTable;
  private static ApplicationContext factory = new ClassPathXmlApplicationContext("BeanFactory.xml");
  private ReservationService reservationService = factory.getBean(ReservationService.class);
  private Trip trip;
  private Reservation selectedReservation;

  public void setTrip(Trip trip) {
    this.trip = trip;
    loadTableData();
  }
  public DataController() {}

  public void initialize() {
    initTable();
  }

  @SuppressWarnings("unchecked")
  private void initTable() {
    TableColumn<Reservation, String> nameColumn = new TableColumn<>("Nume client");
    TableColumn<Reservation, Integer> seatsColumn = new TableColumn<>("Numar locuri");
    nameColumn.setCellValueFactory(new PropertyValueFactory<>("clientName"));
    seatsColumn.setCellValueFactory(new PropertyValueFactory<>("seatsNr"));
    reservationsTable.getColumns().addAll(nameColumn, seatsColumn);
    reservationsTable.getSelectionModel().selectedItemProperty().addListener((observableValue, oldE, newE) -> {
      selectedReservation = newE;
    });
  }

  private void loadTableData() {
    reservationsTable.getItems().clear();
    reservationsTable.getItems().addAll(reservationService.getReservationsByTrip(trip));
  }
}
