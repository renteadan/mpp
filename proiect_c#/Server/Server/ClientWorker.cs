﻿using csharp.Domain;
using csharp.Networking;
using csharp.Services;
using csharp.Services.Service;
using System;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace csharp.Server
{
	internal class ClientWorker
	{
		private readonly Logger logger = new Logger("CApp");
		private readonly TcpClient client;
		private readonly BinaryFormatter formatter = new BinaryFormatter();
		private bool Running { get; set; }
		private readonly ReservationService reservationService = new ReservationService(new ReservationRepository(), new ReservationValidator());
		private readonly TripService tripService = new TripService(new TripRepository(), new TripValidator());
		private readonly LoginService loginService = new LoginService();
		private readonly RemotableObject state;

		public ClientWorker(TcpClient client, RemotableObject state)
		{
			this.client = client;
			this.state = state;
			Running = true;
		}

		public void Run()
		{
			while (Running)
			{
				try
				{
					object request = formatter.Deserialize(client.GetStream());
					Console.WriteLine($"New Request! {request.GetType()}");
					if (request is IAdd add)
					{
						HandleAdd(add);
					}
					else if (request is IQuery query)
					{
						HandleQuery(query);
					}
				}
				catch (Exception e)
				{
					logger.Error(e);
					Running = false;
					client.Close();
				}
			}
		}

		private void HandleAdd(IAdd addRequest)
		{
			if (addRequest is ReservationAdd message)
			{
				Reservation reservation = message.reservation;
				reservationService.Insert(reservation);
			}
		}

		private void HandleQuery(IQuery query)
		{
			if (query is QueryDestinations)
			{
				ResponseDestinations response = new ResponseDestinations(state.GetDestinations());
				SendMessage(response);
			}
			else if (query is QueryLogin login)
			{
				ResponseLogin response = new ResponseLogin(loginService.Login(login.Username, login.Password));
				SendMessage(response);
			}
			else if (query is QueryTrips queryTrips)
			{
				ResponseTrips response = new ResponseTrips(tripService.GetTripsByDestinationAndDate(queryTrips.destination, queryTrips.departure));
				SendMessage(response);
			}
			else if (query is QueryReservations queryReservations)
			{
				ResponseReservations response = new ResponseReservations(reservationService.GetReservationsByTrip(queryReservations.Trip), reservationService.CountRemainingSeatsOnTrip(queryReservations.Trip));
				SendMessage(response);
			}
		}

		public void SendMessage(IResponse response)
		{
			try
			{
				formatter.Serialize(client.GetStream(), response);
				client.GetStream().Flush();
			}
			catch (Exception e)
			{
				logger.Error(e);
			}
		}
	}
}