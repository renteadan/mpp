package Domain;

import org.jooq.Field;
import org.jooq.Record;
import org.jooq.Table;
import org.jooq.impl.DSL;

import java.util.Objects;

public class Destination extends BaseEntity {
  private String name;
  public static Field<String> NAME = DSL.field("name", String.class);
  private static String tableName = "destination";
  public static Table<?>TABLE = DSL.table(tableName);
  public Destination(int id, String name) {
    super(id);
    this.name = name;
  }

  public Destination(Record record) {
    super(record.getValue(ID));
    this.name = record.getValue(NAME);
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

  @Override
  public boolean equals(Object o) {
    if (this == o) return true;
    if (!(o instanceof Destination)) return false;
    Destination that = (Destination) o;
    return getName().equals(that.getName()) && getId() == that.getId();
  }

  @Override
  public int hashCode() {
    return Objects.hash(getName(), getId());
  }
}
