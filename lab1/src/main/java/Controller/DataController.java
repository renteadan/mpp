package Controller;

import Domain.Reservation;
import Domain.Trip;
import Errors.ValidationError;
import Service.ReservationService;
import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.event.ActionEvent;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;


public class DataController {
  public TableView<Reservation> reservationsTable;
  private static ApplicationContext factory = new ClassPathXmlApplicationContext("BeanFactory.xml");
  public TextField nameField;
  public Slider sliderSeats;
  public Label seatsLabel;
  public Button createButton;
  private ReservationService reservationService = factory.getBean(ReservationService.class);
  private Trip trip;
  private Reservation selectedReservation;
  private int maxSeats = 0;

  public void setTrip(Trip trip) {
    this.trip = trip;
    resetData();
  }

  private void resetData() {
    loadTableData();
    initialValues();
  }

  private void showError(Exception e) {
    Alert alert = new Alert(Alert.AlertType.ERROR, e.getMessage());
    alert.showAndWait();
  }

  private void initialValues() {
    maxSeats = reservationService.countRemainingSeatsOnTrip(trip);
    sliderSeats.setMax(maxSeats);
    sliderSeats.setValue(1);
    seatsLabel.setText("1");
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
    sliderSeats.setMin(1);
    sliderSeats.setBlockIncrement(1);
    sliderSeats.valueProperty().addListener(new ChangeListener<Number>() {
      @Override
      public void changed(ObservableValue<? extends Number> observable, Number oldValue, Number newValue) {
        seatsLabel.setText(String.valueOf(newValue.intValue()));
      }
    });
    seatsLabel.setText("1");
  }

  private void loadTableData() {
    reservationsTable.getItems().clear();
    reservationsTable.getItems().addAll(reservationService.getReservationsByTrip(trip));
  }


  public void create(ActionEvent actionEvent) {
    String name = nameField.getText();
    Integer seats = (int) sliderSeats.getValue();
    Reservation reservation = new Reservation(name ,seats, trip);
    try {
      reservationService.insert(reservation);
      resetData();
    } catch (ValidationError validationError) {
      showError(validationError);
    }
  }
}
