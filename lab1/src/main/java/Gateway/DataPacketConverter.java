package Gateway;

import Domain.BaseEntity;

import java.sql.PreparedStatement;
import java.sql.SQLException;

public interface DataPacketConverter<E extends BaseEntity> {
  void toDataPacket(E obj, PreparedStatement statement) throws SQLException;
}
