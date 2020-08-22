
using csharp.Domain;
using Google.Protobuf.WellKnownTypes;
using grpcService.Proto;

namespace csharp.Utils
{
	public class Converter
	{

		public static Destination FromDTO(DestinationDTO dest)
		{
			return new Destination(dest.Id, dest.Name);
		}

		public static DestinationDTO ToDTO(Destination dest)
		{
			return new DestinationDTO
			{
				Id = dest.Id,
				Name = dest.Name
			};
		}

		public static Trip FromDTO(TripDTO trip)
		{
			return new Trip(trip.Id, trip.Departure.ToDateTime(), Converter.FromDTO(trip.Destination));
		}

		public static TripDTO ToDTO(Trip trip)
		{
			return new TripDTO
			{
				Id = trip.Id,
				Departure = Timestamp.FromDateTime(trip.Departure),
				Destination = Converter.ToDTO(trip.Destination)
			};
		}

		public static Reservation FromDTO(ReservationDTO reservation)
		{
			return new Reservation(reservation.Id, reservation.ClientName, reservation.NumberOfSeats, Converter.FromDTO(reservation.Trip));
		}

		public static ReservationDTO ToDTO(Reservation reservation)
		{
			return new ReservationDTO()
			{
				Id = reservation.Id,
				ClientName = reservation.ClientName,
				NumberOfSeats = reservation.SeatsNr,
				Trip = Converter.ToDTO(reservation.Trip)
			};
		}
	}
}
