using csharp.Domain;

namespace csharp.Services.Service
{
	public class DestinationService : BaseService<Destination, IDestinationRepository>
	{
		public DestinationService(IDestinationRepository repository, DestinationValidator validator) : base(repository, validator)
		{
		}
	}
}