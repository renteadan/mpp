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

  @FXML
  private TableView<Trip> tripsTable;
  private DestinationService destinationService = factory.getBean(DestinationService.class);
  private TripService tripService = factory.getBean(TripService.class);
  private Trip selectedTrip;
  private AdminController parent;
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
                    if (item.isBefore(datePicker.getValue())) {
                      setDisable(true);
                      setStyle("-fx-background-color: #ff0709;");
                    }
                  }
                };
              }
            };
    datePicker.setDayCellFactory(dayCellFactory);
    initTable();
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
    Destination destination = destinationSelect.getValue();
    loadTableData(tripService.getTripsByDestination(destination));
  }
}
