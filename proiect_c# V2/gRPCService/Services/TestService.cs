using csharp.Domain;
using csharp.Networking;
using csharp.Services;
using csharp.Utils;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace grpcService.Proto
{
	public class TestService: ServiceImpl.ServiceImplBase
	{
		private readonly ServiceImplementation serviceImpl = new ServiceImplementation();
		private readonly Dictionary<string, IServerStreamWriter<ReloadData>> clients = new Dictionary<string, IServerStreamWriter<ReloadData>>();
		private readonly BufferBlock<ReloadData> buffer = new BufferBlock<ReloadData>();

		public TestService()
		{
		}

		public override async Task Subscribe(Subscription request, IServerStreamWriter<ReloadData> responseStream, ServerCallContext context)
		{
			clients[request.Id] = responseStream;
			while(clients.ContainsKey(request.Id))
			{
				Console.WriteLine("waiting for event");
				var @event = await buffer.ReceiveAsync();
				Console.WriteLine("new Event", @event);
				foreach(var client in clients.Values)
				{
					await client.WriteAsync(@event);
				}
			}
		}

		public override Task<Empty> Unsubscribe(Subscription request, ServerCallContext context)
		{
			clients.Remove(request.Id);
			return Task.FromResult(new Empty());
		}

		public override Task<DestionationsResponse> GetDestinations(DestinationsRequest request, ServerCallContext context)
		{
			var res = serviceImpl.GetDestinations();
			DestionationsResponse response = new DestionationsResponse();
			foreach(var dest in res)
			{
				response.Destinations.Add(Converter.ToDTO(dest));
			}
			return Task.FromResult(response);
		}

		public override Task<TripsResponse> GetTrips(TripsRequest request, ServerCallContext context)
		{
			var res = serviceImpl.GetTrips(Converter.FromDTO(request.Destination), request.Departure.ToDateTime());
			TripsResponse response = new TripsResponse();
			foreach(var trip in res)
			{
				response.Trips.Add(Converter.ToDTO(trip));
			}
			return Task.FromResult(response);
		}

		public override Task<ReservationsResponse> GetReservations(ReservationsRequest request, ServerCallContext context)
		{
			var result = serviceImpl.GetReservations(Converter.FromDTO(request.Trip));
			ReservationsResponse response = new ReservationsResponse();
			foreach(var res in result)
			{
				response.Reservations.Add(Converter.ToDTO(res));
			}
			return Task.FromResult(response);
		}

		public override Task<Empty> MakeReservation(ReservationDTO request, ServerCallContext context)
		{
			Reservation reservation = Converter.FromDTO(request);
			serviceImpl.AddReservation(reservation);
			buffer.SendAsync(new ReloadData());
			return Task.FromResult(new Empty());
		}

		public override Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
		{
			LoginResponse response = new LoginResponse()
			{
				Ok = serviceImpl.Login(request.Username, request.Password)
			};
			return Task.FromResult(response);
		}

		public override Task<SeatsResponse> GetRemainingSeats(TripDTO request, ServerCallContext context)
		{
			SeatsResponse response = new SeatsResponse()
			{
				Seats = serviceImpl.GetRemainingSeats(Converter.FromDTO(request))
			};
			return Task.FromResult(response);
		}
	}
}
