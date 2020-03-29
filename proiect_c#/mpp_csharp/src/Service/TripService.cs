using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TripService : BaseService<Trip, TripRepository>
{
	protected TripService() : base(new TripRepository(), new TripValidator())
	{
	}

	public List<Trip> GetTripsByDestination(Destination destination)
	{
		return repository.GetTripsByDestination(destination);
	}

	public List<Trip> GetTripsByDestinationAndDate(Destination destination, DateTime date)
	{
		return repository.GetTripsByDestinationAndDate(destination, date);
	}
}

