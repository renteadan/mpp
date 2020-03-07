package Domain;

public class Destination extends BaseEntity {
  private String name;

  public Destination(int id, String name) {
    super(id);
    this.name = name;
  }

  public Destination(String name) {
    this.name = name;
  }

  public String getName() {
    return name;
  }

  public void setName(String name) {
    this.name = name;
  }
}
