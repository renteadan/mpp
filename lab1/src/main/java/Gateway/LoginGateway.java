package Gateway;
import org.jooq.*;
import org.jooq.impl.DSL;

public class LoginGateway extends BaseGateway {
  private DSLContext ctx = DSL.using(SQLDialect.POSTGRES);
  private Table<?> TABLE = DSL.table("account");
  public Boolean login(String username, String password) {
    SelectQuery<?> selectQuery = ctx.selectQuery(TABLE);
    selectQuery.addConditions(DSL.condition("username = ?", username));
    selectQuery.addConditions(DSL.condition("password = ?", password));
    Result<?> result = super.findJooq(selectQuery);
    return result.isNotEmpty();
  }
}
