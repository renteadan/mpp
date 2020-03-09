package Domain;

import org.jooq.Field;
import org.jooq.impl.DSL;

public class BaseEntity {
  private int id;
  public static Field<Integer> ID = DSL.field("id", Integer.class);

  public BaseEntity(int id) {
    this.id = id;
  }

  public BaseEntity() {
  }

  public int getId() {
    return id;
  }

  public void setId(int id) {
    this.id = id;
  }
}
