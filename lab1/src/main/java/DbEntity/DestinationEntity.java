package DbEntity;

import javax.persistence.*;

@Entity
@Table(name = "destination_entity", schema = "mpp")
public class DestinationEntity {
  private int id;
  private String name;

  @Column(name = "id", unique = true)
  @Id
  @GeneratedValue(strategy = GenerationType.IDENTITY)
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
