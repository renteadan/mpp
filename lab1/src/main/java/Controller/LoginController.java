package Controller;

import Gateway.LoginGateway;
import Logger.LoggerManager;
import Service.LoginService;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Node;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.TextField;
import javafx.stage.Stage;
import java.io.IOException;
import java.net.URL;

public class LoginController {

  @FXML
  public TextField usernameField, passwordField;
  public Button loginButton;
  private LoginService service = new LoginService();
  private LoggerManager logger = new LoggerManager(LoginController.class);
  public LoginController() {
  }

  @FXML
  public void initialize() {
  }

  private void openAdmin(ActionEvent event) {
    try {
      URL url = getClass().getResource("/FXML/Admin.fxml");
      Parent root = FXMLLoader.load(url);
      Stage stage = new Stage();
      stage.setScene(new Scene(root));
      stage.show();
      Node source = (Node) event.getSource();
      source.getScene().getWindow().hide();
    } catch (Exception e) {
      logger.error(e);
    }
  }

  @FXML
  void login(ActionEvent event) {
    String username = usernameField.getText();
    String password = passwordField.getText();
    if(service.login(username, password)) {
      Alert alert = new Alert(Alert.AlertType.CONFIRMATION, "Login successful!");
      alert.showAndWait();
      openAdmin(event);
    } else {
      Alert alert = new Alert(Alert.AlertType.ERROR, "Login failed!");
      alert.showAndWait();
    }
  }
}
