using csharp.Domain;
using csharp.Networking;
using csharp.Utils;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using grpcService.Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.ClientCore
{
	internal class ProtoClientWorker : IClientWorker, IObserver
	{
		private readonly ServiceImpl.ServiceImplClient client;
		private Subscription subscription;

		public ProtoClientWorker()
		{
			GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:45000");
			client = new ServiceImpl.ServiceImplClient(channel);
		}

		private async Task Subscribe()
		{
			subscription = new Subscription() { Id = Guid.NewGuid().ToString() };
			using var call = client.Subscribe(subscription);
			var responseReader = Task.Run(async () =>
			{
				while (await call.ResponseStream.MoveNext())
				{
					Console.WriteLine("got notified");
					ReloadForms();
				}
			});
			await responseReader;
		}

		public void Unsubcribe()
		{
			client.Unsubscribe(subscription);
		}

		public event EventHandler<ReloadDataEventArgs> ReloadData;

		public void AddReservation(Reservation res)
		{
			client.MakeReservation(Converter.ToDTO(res));
		}

		public List<Destination> GetDestinations()
		{
			var result = client.GetDestinations(new DestinationsRequest());
			var query = from res in result.Destinations
									select Converter.FromDTO(res);
			return query.ToList();
		}

		public int GetRemainingSeats(Trip trip)
		{
			return client.GetRemainingSeats(Converter.ToDTO(trip)).Seats;
		}

		public List<Reservation> GetReservations(Trip trip)
		{
			ReservationsRequest request = new ReservationsRequest()
			{
				Trip = Converter.ToDTO(trip)
			};
			var result = client.GetReservations(request);
			var query = from res in result.Reservations
									select Converter.FromDTO(res);
			return query.ToList();
		}

		public List<Trip> GetTrips(Destination dest, DateTime time)
		{
			time = time.ToUniversalTime();
			TripsRequest request = new TripsRequest()
			{
				Departure = Timestamp.FromDateTime(time),
				Destination = Converter.ToDTO(dest)
			};
			var result = client.GetTrips(request);
			var query = from res in result.Trips
									select Converter.FromDTO(res);
			return query.ToList();
		}

		public bool Login(string user, string pass)
		{
			LoginRequest request = new LoginRequest()
			{
				Username = user,
				Password = pass
			};
			var result = client.Login(request).Ok;
			if(result)
			{
				Task.Run(async () =>
				{
					await Subscribe();
				}).GetAwaiter();
			}
			return result;
		}



		public void ReloadForms()
		{
			EventHandler<ReloadDataEventArgs> handler = ReloadData;
			handler?.Invoke(this, new ReloadDataEventArgs());
		}

		public void UpdateObs()
		{
			ReloadForms();
		}
	}
}