using csharp.Domain;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Services.Repository.ygfxpsvi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Services.Repository
{
	class DestinationORMRepository : IDestinationRepository
	{
		public DestinationORMRepository()
		{
			string connString = ConfigurationManager.ConnectionStrings["rogue.db.elephantsql.comygfxpsvi"].ConnectionString;
			XpoDefault.DataLayer = XpoDefault.GetDataLayer(connString, AutoCreateOption.SchemaAlreadyExists);

		}

		public void Delete(Destination entity)
		{
			DestinationEntity entity1 = new DestinationEntity(Session.DefaultSession)
			{
				Id = entity.Id
			};
			entity1.Delete();
		}

		public Destination Find(int id)
		{
			DestinationEntity entity = new DestinationEntity(Session.DefaultSession)
			{
				Id = id
			};
			entity.Reload();
			return new Destination(entity.Id, entity.Name);
		}

		public List<Destination> FindAll()
		{
			Console.WriteLine("ORM Repo");
			XPCollection<DestinationEntity> destinationEntities = new XPCollection<DestinationEntity>(Session.DefaultSession);
			List<Destination> destinations = new List<Destination>();
			foreach(DestinationEntity en in destinationEntities)
			{
				Destination dest = new Destination()
				{
					Id = en.Id,
					Name = en.Name
				};
				destinations.Add(dest);
			}
			return destinations;
		}

		public List<Destination> FindLastN(int n)
		{
			XPCollection<DestinationEntity> destinationEntities = new XPCollection<DestinationEntity>(Session.DefaultSession)
			{
				TopReturnedObjects = n
			};
			List<Destination> destinations = new List<Destination>();
			foreach (DestinationEntity en in destinationEntities)
			{
				Destination dest = new Destination()
				{
					Id = en.Id,
					Name = en.Name
				};
				destinations.Add(dest);
			}
			return destinations;
		}

		public Destination Insert(Destination entity)
		{
			DestinationEntity dest = new DestinationEntity(Session.DefaultSession)
			{
				Name = entity.Name
			};
			dest.Save();
			dest.Reload();
			entity.Id = dest.Id;
			return entity;
		}

		public void Update(Destination entity)
		{
			DestinationEntity dest = new DestinationEntity(Session.DefaultSession)
			{
				Id = entity.Id,
				Name = entity.Name
			};
			dest.Save();
		}
	}
}
