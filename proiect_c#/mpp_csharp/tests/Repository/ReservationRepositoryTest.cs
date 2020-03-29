using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Test.Repository
{
	internal class ReservationRepositoryTest
	{
		private readonly Logger logger = new Logger("Test");
		private readonly ReservationRepository repository = new ReservationRepository();
		private readonly DestinationRepository destinationRepository = new DestinationRepository();
		private readonly TripRepository tripRepository = new TripRepository();
		private int beforeRecords;
		private readonly List<Destination> allDestinations = new List<Destination>();

		private void DeleteAll()
		{
			foreach (Destination d in allDestinations)
			{
				destinationRepository.Delete(d);
			}
		}

		[OneTimeSetUp]
		public void Setup()
		{
			beforeRecords = repository.FindAll().Count;
		}

		[OneTimeTearDown]
		public void CleanUp()
		{
			DeleteAll();
			int currentRecords = repository.FindAll().Count;
			if (currentRecords != beforeRecords)
			{
				logger.Error(new Exception("Not all test records were Deleted!"));
				Assert.Fail();
			}
			else
			{
				logger.Info($"{GetType().Name} tests passed");
			}
		}

		[Test]
		public void Find()
		{
			try
			{
				Destination dest = new Destination("TestResGateway");
				dest = destinationRepository.Insert(dest);
				allDestinations.Add(dest);
				Trip trip = new Trip(DateTime.Now.AddHours(3), dest);
				trip = tripRepository.Insert(trip);
				Reservation reservation = new Reservation("Reservation Gateway Test", 7, trip);
				reservation = repository.Insert(reservation);
				Reservation reservation1 = repository.Find(reservation.Id);
				Assert.AreEqual(reservation, reservation1);
			}
			catch (SQLErrorNoEntityFound)
			{
				Assert.Fail();
			}
		}

		[Test]
		public void Delete()
		{
			Destination dest = new Destination("TestResGateway");
			dest = destinationRepository.Insert(dest);
			allDestinations.Add(dest);
			Trip trip = new Trip(DateTime.Now.AddHours(5), dest);
			trip = tripRepository.Insert(trip);
			Reservation reservation = new Reservation("Reservation Gateway Test", 7, trip);
			reservation = repository.Insert(reservation);
			repository.Delete(reservation);
			Reservation finalReservation = reservation;
			Assert.Throws<SQLErrorNoEntityFound>(() =>
			{
				repository.Find(finalReservation.Id);
			});
		}

		[Test]
		public void Update()
		{
			Destination dest = new Destination("TestResGateway");
			dest = destinationRepository.Insert(dest);
			allDestinations.Add(dest);
			Trip trip = new Trip(DateTime.Now.AddHours(10), dest);
			trip = tripRepository.Insert(trip);
			Reservation reservation = new Reservation("Reservation Gateway Test", 7, trip);
			reservation = repository.Insert(reservation);
			reservation.SeatsNr = 10;
			repository.Update(reservation);
			Assert.AreEqual(reservation.SeatsNr, 10);
		}

		[Test]
		public void Insert()
		{
			Destination dest = new Destination("TestResGateway");
			dest = destinationRepository.Insert(dest);
			allDestinations.Add(dest);
			Trip trip = new Trip(DateTime.Now.AddDays(1), dest);
			trip = tripRepository.Insert(trip);
			List<Reservation> reservations = new List<Reservation>();
			for (int i = 0; i < 10; i++)
			{
				Reservation reservation = new Reservation("Reservation Gateway Test", 12, trip);
				reservation = repository.Insert(reservation);
				reservations.Add(reservation);
			}
			reservations.Reverse();
			Assert.AreEqual(repository.FindLastN(10), reservations);
		}
	}
}