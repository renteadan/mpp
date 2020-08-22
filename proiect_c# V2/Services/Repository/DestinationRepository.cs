using csharp.Domain;
using Npgsql;
using System.Collections.Generic;
public class DestinationRepository : BaseRepository, IRepository<Destination>, IDestinationRepository
{
	public DestinationRepository()
	{
	}

	public List<Destination> FindAll()
	{
		using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM destination;"))
		{
			using (NpgsqlDataReader reader = ExecuteSelect(command))
			{
				List<Destination> destinations = new List<Destination>();
				while (reader.Read())
				{
					Destination destination = new Destination(reader.GetInt32(0), reader.GetString(1));
					destinations.Add(destination);
				}
				return destinations;
			}
		}
	}

	public Destination Insert(Destination destination)
	{
		NpgsqlCommand command = new NpgsqlCommand("INSERT INTO destination(name) VALUES(@name) RETURNING id;");
		command.Parameters.AddWithValue("@name", destination.Name);
		int result = ExecuteScalar(command);
		destination.Id = result;
		return destination;
	}

	public void Update(Destination destination)
	{
		NpgsqlCommand command = new NpgsqlCommand("UPDATE destination SET name = @name WHERE id = @id;");
		command.Parameters.AddWithValue("@name", destination.Name);
		command.Parameters.AddWithValue("@id", destination.Id);
		ExecuteNonQuery(command);
	}

	public void Delete(Destination destination)
	{
		NpgsqlCommand command = new NpgsqlCommand("DELETE FROM destination WHERE id = @id;");
		command.Parameters.AddWithValue("@id", destination.Id);
		ExecuteNonQuery(command);
	}

	public Destination Find(int id)
	{
		using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM destination WHERE id = @id;"))
		{
			command.Parameters.AddWithValue("@id", id);
			using (NpgsqlDataReader reader = ExecuteSelect(command))
			{
				if (!reader.Read())
					throw new SQLErrorNoEntityFound($"No destination found with this id = {id}");
				return new Destination(reader.GetInt32(0), reader.GetString(1));
			}
		}
	}

	public List<Destination> FindLastN(int n)
	{
		using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM destination ORDER BY id DESC LIMIT @n;"))
		{
			command.Parameters.AddWithValue("@n", n);
			using (NpgsqlDataReader reader = ExecuteSelect(command))
			{
				List<Destination> destinations = new List<Destination>();
				while (reader.Read())
				{
					Destination destination = new Destination(reader.GetInt32(0), reader.GetString(1));
					destinations.Add(destination);
				}
				return destinations;
			}
		}
	}
}