using csharp.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test.Repository
{
	internal class DestinationRepositoryTest
	{
		private Logger logger;
		private readonly DestinationRepository repository = new DestinationRepository();
		private readonly Random random = new Random();
		private int beforeRecords;
		private string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}

		[OneTimeSetUp]
		public void Setup()
		{
			beforeRecords = repository.FindAll().Count;
			logger = new Logger("Test");
		}

		[OneTimeTearDown]
		public void CleanUp()
		{
			int currentRecords = repository.FindAll().Count;
			if (currentRecords != beforeRecords)
			{
				logger.Error(new Exception("Not all test records were deleted!"));
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
			Destination dest = new Destination("Test1");
			dest = repository.Insert(dest);
			Destination dest2;
			try
			{
				dest2 = repository.Find(dest.Id);
				Assert.AreEqual(dest.Name, dest2.Name);
			}
			catch (SQLErrorNoEntityFound)
			{
				Assert.Fail();
			}
			repository.Delete(dest);
			Destination finalDest = dest;
			Assert.Throws<SQLErrorNoEntityFound>(() =>
		 {
			 repository.Find(finalDest.Id);
		 });
		}

		[Test]
		public void Delete()
		{
			Destination dest = new Destination("Test1");
			dest = repository.Insert(dest);
			repository.Delete(dest);
			Destination finalDest = dest;
			Assert.Throws<SQLErrorNoEntityFound>(() =>
			{
				repository.Find(finalDest.Id);
			});
		}

		[Test]
		public void Insert()
		{
			List<Destination> InsertedDestinations = new List<Destination>();
			Destination destination;
			for (int i = 0; i < 10; i++)
			{
				destination = new Destination(RandomString(10));
				destination = repository.Insert(destination);
				InsertedDestinations.Add(destination);
			}
			foreach (Destination d in InsertedDestinations)
			{
				repository.Delete(d);
				Assert.Throws<SQLErrorNoEntityFound>(() =>
			 {
				 repository.Find(d.Id);
			 });
			}
		}

		[Test]
		public void Update()
		{
			Destination dest = new Destination("Test1");
			dest = repository.Insert(dest);
			Destination dest2;
			try
			{
				var d = repository.Find(dest.Id);
				Assert.AreEqual(d.Name, "Test1");
			}
			catch (SQLErrorNoEntityFound)
			{
				Assert.Fail();
			}
			dest.Name = "Test2";
			repository.Update(dest);
			try
			{
				var d = repository.Find(dest.Id);
				Assert.AreEqual(d.Name, "Test2");
			}
			catch (SQLErrorNoEntityFound)
			{
				Assert.Fail();
			}
			dest2 = repository.Find(dest.Id);
			Assert.AreEqual(dest2.Name, dest.Name);
			repository.Delete(dest);
			Destination finalDest = dest;
			Assert.Throws<SQLErrorNoEntityFound>(() =>
		 {
			 repository.Find(finalDest.Id);
		 });
		}
	}
}