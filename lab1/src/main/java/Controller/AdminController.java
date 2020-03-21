package Controller;
import Domain.Trip;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.control.Tab;
import javafx.scene.control.TabPane;

import java.io.IOException;
import java.net.URL;

public class AdminController {
  @FXML
  public TabPane tabPane;

  private DataController dataController;

  Tab dataTab;
  public AdminController() {}

  public void initialize() {
    try {
      loadSearchTab();
      loadDataTable();
    } catch (IOException e) {
      e.printStackTrace();
    }
  }

  private void loadSearchTab() throws IOException {
    URL searchUrl = getClass().getResource("/FXML/Search.fxml");
    FXMLLoader searchLoader = new FXMLLoader(searchUrl);
    Parent search = searchLoader.load();
    Tab searchTab = new Tab("Cautare", search);
    tabPane.getTabs().add(searchTab);
    SearchController searchController = searchLoader.getController();
    searchController.setParent(this);
  }

  private void loadDataTable() throws IOException {
    URL dataUrl = getClass().getResource("/FXML/Data.fxml");
    FXMLLoader dataLoader = new FXMLLoader(dataUrl);
    Parent data = dataLoader.load();
    dataTab = new Tab("Calatorii", data);
    tabPane.getTabs().add(dataTab);
    dataController = dataLoader.getController();
    dataTab.setDisable(true);
  }

  public void selectTrip(Trip trip) {
    dataController.setTrip(trip);
    dataTab.setDisable(false);
  }

  public void disableData() {
    dataTab.setDisable(true);
  }
}
