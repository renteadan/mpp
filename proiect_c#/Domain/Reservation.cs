using System;

namespace csharp.Domain
{
	[Serializable]
	public class Reservation : BaseEntity
	{
		public string ClientName { get; set; }
		public int SeatsNr { get; set; }
		public Trip Trip { get; set; }

		public Reservation(int id, string clientName, int seatsNr, Trip trip) : base(id)
		{
			ClientName = clientName;
			SeatsNr = seatsNr;
			Trip = trip;
		}

		public Reservation(string clientName, int seatsNr, Trip trip)
		{
			ClientName = clientName;
			SeatsNr = seatsNr;
			Trip = trip;
		}

		public Reservation()
		{
		}

		public override bool Equals(object obj)
		{
			return obj is Reservation reservation &&
						 Id == reservation.Id;
		}

		public override int GetHashCode()
		{
			return -98705819 + Id.GetHashCode();
		}
	} 
}