
public class DestinationService: BaseService<Destination, DestinationRepository> {
  public DestinationService(): base(new DestinationRepository(), new DestinationValidator()) {
  }
}
