package Gateway;

import Domain.Destination;
import net.bytebuddy.utility.RandomString;
import org.junit.jupiter.api.Test;

import java.util.Vector;

import static org.junit.jupiter.api.Assertions.*;

class DestinationGatewayTest {

  private DestinationGateway gateway = new DestinationGateway();
  @Test
  void find() {
    Destination dest = new Destination("Test1");
    dest = gateway.insert(dest);
    Destination dest2 = gateway.find(dest.getId());
    assertEquals(dest.getName(), dest2.getName());
    gateway.deleteById(dest.getId());
    assertNull(gateway.find(dest.getId()));
  }

  @Test
  void deleteById() {
    Destination dest = new Destination("Test1");
    dest = gateway.insert(dest);
    gateway.deleteById(dest.getId());
    Destination destination = gateway.find(dest.getId());
    assertNull(destination);
  }

  @Test
  void insert() {
    Vector<Destination> insertedDestinations = new Vector<>();
    Destination destination;
    for(int i=0; i<10 ;i++) {
      destination = new Destination(RandomString.make(10));
      destination = gateway.insert(destination);
      insertedDestinations.add(destination);
    }
    for (Destination d:insertedDestinations) {
      gateway.deleteById(d.getId());
      assertNull(gateway.find(d.getId()));
    }
  }

  @Test
  void update() {
    Destination dest = new Destination("Test1");
    dest = gateway.insert(dest);
    assertEquals(gateway.find(dest.getId()).getName(), "Test1");
    dest.setName("Test2");
    gateway.update(dest);
    assertEquals(gateway.find(dest.getId()).getName(), "Test2");
    gateway.deleteById(dest.getId());
    assertNull(gateway.find(dest.getId()));
  }
}