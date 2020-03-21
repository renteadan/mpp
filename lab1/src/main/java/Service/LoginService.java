package Service;

import Gateway.LoginGateway;

public class LoginService {
  private LoginGateway gateway = new LoginGateway();

  public LoginService() {
  }

  public Boolean login(String username, String password) {
    return gateway.login(username, password);
  }
}
