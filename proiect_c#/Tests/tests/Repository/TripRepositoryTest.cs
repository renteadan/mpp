using csharp.Domain;
using csharp.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Test.Repository
{
	internal class TripRepositoryTest
	{
		private readonly Logger logger = new Logger("Test");
		private readonly DestinationRepository destionationRepository = new DestinationRepository();
		private readonly TripRepository repository = new TripRepository();
		private int beforeRecords;
		private readonly List<Destination> allDestinations = new List<Destination>();

		private void DeleteAll()
		{
			foreach (Destination d in allDestinations)
			{
				destionationRepository.Delete(d);
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
			Destination dest = new Destination("testTripRepo");
			dest = destionationRepository.Insert(dest);
			allDestinations.Add(dest);
			Trip trip = new Trip(DateTime.Now, dest);
			trip = repository.Insert(trip);
			try
			{
				Trip trip2 = repository.Find(trip.Id);
				Assert.AreEqual(trip, trip2);
			}
			catch (SQLErrorNoEntityFound)
			{
				Assert.Fail();
			}
		}

		[Test]
		public void Delete()
		{
			Destination dest = new Destination("testTripGateway");
			dest = destionationRepository.Insert(dest);
			allDestinations.Add(dest);
			Trip trip = new Trip(DateTime.Now, dest);
			trip = repository.Insert(trip);
			repository.Delete(trip);
			Trip finalTrip = trip;
			Assert.Throws<SQLErrorNoEntityFound>(() =>
			{
				repository.Find(finalTrip.Id);
			});
		}

		[Test]
		public void Update()
		{
			Destination dest = new Destination("testTripGateway");
			dest = destionationRepository.Insert(dest);
			allDestinations.Add(dest);
			Trip trip = new Trip(DateTime.Now, dest);
			trip = repository.Insert(trip);
			trip.Departure = DateTime.Parse("2019-01-01 00:05:00");
			repository.Update(trip);
			try
			{
				trip = repository.Find(trip.Id);
				Assert.AreEqual(trip.Departure, DateTime.Parse("2019-01-01 00:05:00"));
			}
			catch (SQLErrorNoEntityFound)
			{
				Assert.Fail();
			}
		}

		[Test]
		public void Insert()
		{
			Destination dest = new Destination("testTripGateway");
			dest = destionationRepository.Insert(dest);
			allDestinations.Add(dest);
			Trip trip = new Trip(DateTime.Now.AddHours(12), dest);
			trip = repository.Insert(trip);
			try
			{
				Trip trip2 = repository.Find(trip.Id);
				Assert.AreEqual(trip, trip2);
			}
			catch (SQLErrorNoEntityFound)
			{
				Assert.Fail();
			}
		}
	}
}