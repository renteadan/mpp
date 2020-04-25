using csharp.Domain;
using System;

namespace csharp.Networking
{
	public interface IQuery
	{
	}

	[Serializable]
	public class QueryReservations : IQuery
	{
		public QueryReservations(Trip trip)
		{
			Trip = trip;
		}

		public Trip Trip { get; set; }
	}

	[Serializable]
	public class QueryDestinations : IQuery
	{

	}

	[Serializable]
	public class QueryTrips : IQuery
	{
		public Destination destination;
		public DateTime departure;

		public QueryTrips(Destination destination, DateTime departure)
		{
			this.destination = destination;
			this.departure = departure;
		}
	}

	[Serializable]
	public class QueryLogin : IQuery
	{
		public string Username, Password;

		public QueryLogin(string username, string password)
		{
			Username = username;
			Password = password;
		}
	}
}