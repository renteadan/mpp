//using csharp.Domain;
//using csharp.Networking;
//using System;
//using System.Collections.Generic;
//using Grpc.Core;
//using System.Linq;
//using Google.Protobuf.WellKnownTypes;
//using grpcService.Proto;

//namespace chsarp.Client
//{
//	class ProtoClientWorker : IClientWorker
//	{
//		private ServiceImpl.ServiceImplClient client;

//		public ProtoClientWorker()
//		{
//			Channel channel = new Channel("localhost:45000", ChannelCredentials.Insecure);
//			client = new ServiceImpl.ServiceImplClient(channel);
//		}

//		public event EventHandler<ReloadDataEventArgs> ReloadData;

//		public void AddReservation(Reservation res)
//		{
//			client.MakeReservation(Converter.ToDTO(res));
//		}

//		public List<Destination> GetDestinations()
//		{
//			var result = client.GetDestinations(new DestinationsRequest());
//			var query = from res in result.Destinations
//									select Converter.FromDTO(res);
//			return query.ToList();
//		}

//		public int GetRemainingSeats(Trip trip)
//		{
//			return client.GetRemainingSeats(Converter.ToDTO(trip)).Seats;
//		}

//		public List<Reservation> GetReservations(Trip trip)
//		{
//			ReservationsRequest request = new ReservationsRequest()
//			{
//				Trip = Converter.ToDTO(trip)
//			};
//			var result = client.GetReservations(request);
//			var query = from res in result.Reservations
//									select Converter.FromDTO(res);
//			return query.ToList();
//		}

//		public List<Trip> GetTrips(Destination dest, DateTime time)
//		{
//			TripsRequest request = new TripsRequest()
//			{
//				Departure = Timestamp.FromDateTime(time),
//				Destination = Converter.ToDTO(dest)
//			};
//			var result = client.GetTrips(request);
//			var query = from res in result.Trips
//									select Converter.FromDTO(res);
//			return query.ToList();
//		}

//		public bool Login(string user, string pass)
//		{
//			LoginRequest request = new LoginRequest()
//			{
//				Username = user,
//				Password = pass
//			};
//			return client.Login(request).Ok;
//		}

//		public void ReloadForms()
//		{
//			EventHandler<ReloadDataEventArgs> handler = ReloadData;
//			handler?.Invoke(this, new ReloadDataEventArgs());
//		}
//	}
//}
