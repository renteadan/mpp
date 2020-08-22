using System;

namespace csharp.Domain
{
	[Serializable]
	public class Trip : BaseEntity
	{
		public DateTime Departure { get; set; }
		public Destination Destination { get; set; }

		public int LeftSeats { get; set; }

		public Trip(int id, DateTime departure, Destination destination) : base(id)
		{
			Departure = departure;
			Destination = destination;
		}

		public Trip(DateTime departure, Destination destination)
		{
			Departure = departure;
			Destination = destination;
		}

		public Trip()
		{
		}

		public static bool operator ==(Trip a, Trip b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}

			if ((a is null) || (b is null))
			{
				return false;
			}

			return a.Id == b.Id;
		}

		public static bool operator !=(Trip a, Trip b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			return obj is Trip trip &&
						 Id == trip.Id;
		}

		public override int GetHashCode()
		{
			return -1734809 + Id.GetHashCode();
		}
	}
}