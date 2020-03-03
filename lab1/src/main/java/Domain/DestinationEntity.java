package Domain;

import javax.persistence.*;

@Entity
@Table(name = "destination_entity")
public class DestinationEntity {
  private int id;
  private String name;

  @Column(name = "id", unique = true)
  @Id
  @GeneratedValue
  public int getId() {
    return id;
  }

  public void setId(int id) {
    this.id = id;
  }

  @Column(name = "string")
  public String getName() {
    return name;
  }

  public void setName(String name) {
    this.name = name;
  }
}
