package Gateway;

import Domain.Destination;

public class DestinationGateway extends BaseGateway {

  private static String tableName = "destination";
  private DataPacketConverter<Destination> toDataPacket = (destination, statement) -> {
    statement.setString(1, destination.getName());
  };
  public DestinationGateway() {
    super(tableName);
  }

  public Destination insert(Destination destination) {
    return super.insert(destination, toDataPacket);
  }
}
