package Controller;

import Domain.Destination;
import Domain.Trip;
import Service.DestinationService;
import Service.TripService;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.util.Callback;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import java.sql.Timestamp;
import java.time.LocalDate;
import java.util.Vector;

public class SearchController {
  public ChoiceBox<Destination> destinationSelect;
  public DatePicker datePicker;
  private static ApplicationContext factory = new ClassPathXmlApplicationContext("BeanFactory.xml");
  public Button logoutButton;

  @FXML
  private TableView<Trip> tripsTable;
  private DestinationService destinationService = factory.getBean(DestinationService.class);
  private TripService tripService = factory.getBean(TripService.class);
  private Trip selectedTrip;
  private Destination selectedDestination;
  private AdminController parent;
  private LocalDate selectedDate;
  public SearchController() {
  }

  public void setParent(AdminController adminController) {
    parent = adminController;
  }

  public void initialize() {
    initUI();
  }

  private void initUI() {
    destinationSelect.getItems().addAll(destinationService.findAll());
    datePicker.setValue(LocalDate.now());
    final Callback<DatePicker, DateCell> dayCellFactory =
            new Callback<DatePicker, DateCell>() {
              @Override
              public DateCell call(final DatePicker datePicker) {
                return new DateCell() {
                  @Override
                  public void updateItem(LocalDate item, boolean empty) {
                    super.updateItem(item, empty);
                    if (item.isBefore(datePicker.getValue().minusDays(14))) {
                      setDisable(true);
                      setStyle("-fx-background-color: #ff0709;");
                    }
                  }
                };
              }
            };
    datePicker.setDayCellFactory(dayCellFactory);
    initTable();
    selectedDate = LocalDate.now();
  }

  @SuppressWarnings("unchecked")
  private void initTable() {
    TableColumn<Trip, Destination> destinationColumn = new TableColumn<>("Destinatie");
    TableColumn<Trip, Timestamp> departureColumn = new TableColumn<>("Ora plecarii");
    destinationColumn.setCellValueFactory(new PropertyValueFactory<>("destination"));
    departureColumn.setCellValueFactory(new PropertyValueFactory<>("departure"));
    tripsTable.getColumns().addAll(destinationColumn, departureColumn);
    tripsTable.getSelectionModel().selectedItemProperty().addListener((observableValue, oldE, newE) -> {
      if(newE == null)
        return;
      selectedTrip = newE;
      parent.selectTrip(selectedTrip);
    });
  }

  private void loadTableData(Vector<Trip> trips) {
    tripsTable.getItems().clear();
    tripsTable.getItems().addAll(trips);
  }

  public void selectDestination(ActionEvent actionEvent) {
    selectedTrip = null;
    parent.disableData();
    selectedDestination = destinationSelect.getValue();
    if(selectedDate != null)
      loadTableData(tripService.getTripsByDestinationAndDate(selectedDestination, selectedDate));
  }

  public void selectDate(ActionEvent actionEvent) {
    parent.disableData();
    selectedDate = datePicker.getValue();
    selectedDate = selectedDate.atStartOfDay().toLocalDate();
    if(selectedDestination != null)
      loadTableData(tripService.getTripsByDestinationAndDate(selectedDestination, selectedDate));
  }

  public void logout(ActionEvent actionEvent) {
  }
}
