using System;
public class ValidationError: Exception {
  public ValidationError(string message): base(message) {
  }
}
