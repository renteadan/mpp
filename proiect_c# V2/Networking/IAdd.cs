

using csharp.Domain;
using System;

namespace csharp.Networking
{
	public interface IAdd
	{
	}

	[Serializable]
	public class ReservationAdd : IAdd
	{
		public Reservation reservation;

		public ReservationAdd(Reservation reservation)
		{
			this.reservation = reservation;
		}
	}
}
