using csharp.Domain;
using System;
using System.Collections.Generic;

public interface ITripRepository : IRepository<Trip>
{
	List<Trip> GetTripsByDestination(Destination destination);
	List<Trip> GetTripsByDestinationAndDate(Destination destination, DateTime date);
}