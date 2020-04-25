using csharp.Domain;

namespace csharp.Services.Service
{
	public class DestinationService : BaseService<Destination, IDestinationRepository>
	{
		public DestinationService(DestinationRepository repository, DestinationValidator validator) : base(repository, validator)
		{
		}
	}
}