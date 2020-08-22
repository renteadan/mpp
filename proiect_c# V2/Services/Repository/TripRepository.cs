
using csharp.Domain;
using Npgsql;
using System;
using System.Collections.Generic;
public class TripRepository : BaseRepository, IRepository<Trip>, ITripRepository
{

	public TripRepository()
	{
	}

	public Trip Find(int id)
	{
		using (NpgsqlCommand command = new NpgsqlCommand(
			"SELECT * FROM trip " +
			"LEFT JOIN destination ON trip.destination_id = destination.id " +
			"WHERE trip.id = @id;"))
		{
			command.Parameters.AddWithValue("@id", id);
			using (NpgsqlDataReader reader = ExecuteSelect(command))
			{
				if (!reader.Read())
					throw new SQLErrorNoEntityFound($"No trip found with this id = {id}");
				Destination destination = new Destination(reader.GetInt32(3), reader.GetString(4));
				return new Trip(reader.GetInt32(0), reader.GetDateTime(1), destination);
			}
		}
	}

	public void Delete(Trip trip)
	{
		NpgsqlCommand command = new NpgsqlCommand("DELETE FROM trip WHERE id = @id;");
		command.Parameters.AddWithValue("@id", trip.Id);
		ExecuteNonQuery(command);
	}

	public List<Trip> GetTripsByDestination(Destination destination)
	{
		using (NpgsqlCommand command = new NpgsqlCommand(
			"SELECT * FROM trip " +
			"LEFT JOIN destination ON trip.destination_id = destination.id " +
			"WHERE trip.destination_id = @id;"))
		{
			command.Parameters.AddWithValue("@id", destination.Id);
			using (NpgsqlDataReader reader = ExecuteSelect(command))
			{
				List<Trip> trips = new List<Trip>();
				while (reader.Read())
				{
					Destination new_dest = new Destination(reader.GetInt32(3), reader.GetString(4));
					Trip trip = new Trip(reader.GetInt32(0), reader.GetDateTime(1), new_dest);
					trips.Add(trip);
				}
				return trips;
			}
		}
	}

	public List<Trip> GetTripsByDestinationAndDate(Destination destination, DateTime date)
	{
		using (NpgsqlCommand command = new NpgsqlCommand(
			"SELECT * FROM trip " +
			"LEFT JOIN destination ON trip.destination_id = destination.id " +
			"WHERE trip.destination_id = @id AND " +
			"DATE_PART('day', trip.departure - @date::timestamp) = 0;"))
		{
			command.Parameters.AddWithValue("@id", destination.Id);
			command.Parameters.AddWithValue("@date", date);
			using (NpgsqlDataReader reader = ExecuteSelect(command))
			{
				List<Trip> trips = new List<Trip>();
				while (reader.Read())
				{
					Destination new_dest = new Destination(reader.GetInt32(3), reader.GetString(4));
					Trip trip = new Trip(reader.GetInt32(0), reader.GetDateTime(1), new_dest);
					trips.Add(trip);
				}
				return trips;
			}
		}
	}

	public void Update(Trip trip)
	{
		NpgsqlCommand command = new NpgsqlCommand("UPDATE trip SET departure = @departure, destination_id = @dest_id WHERE id = @id;");
		command.Parameters.AddWithValue("@departure", trip.Departure);
		command.Parameters.AddWithValue("@dest_id", trip.Destination.Id);
		command.Parameters.AddWithValue("@id", trip.Id);
		ExecuteNonQuery(command);
	}

	public Trip Insert(Trip trip)
	{
		NpgsqlCommand command = new NpgsqlCommand("INSERT INTO trip(departure, destination_id) VALUES(@departure, @dest_id) RETURNING id;");
		command.Parameters.AddWithValue("@departure", trip.Departure);
		command.Parameters.AddWithValue("@dest_id", trip.Destination.Id);
		int result = ExecuteScalar(command);
		trip.Id = result;
		return trip;
	}

	public List<Trip> FindAll()
	{
		using (NpgsqlCommand command = new NpgsqlCommand(
			"SELECT * FROM trip " +
			"LEFT JOIN destination ON trip.destination_id = destination.id;"
		))
		{
			using (NpgsqlDataReader reader = ExecuteSelect(command))
			{
				List<Trip> trips = new List<Trip>();
				while (reader.Read())
				{
					Destination destination = new Destination(reader.GetInt32(3), reader.GetString(4));
					Trip trip = new Trip(reader.GetInt32(0), reader.GetDateTime(1), destination);
					trips.Add(trip);
				}
				return trips;
			}
		}
	}

	public List<Trip> FindLastN(int n)
	{
		using (NpgsqlCommand command = new NpgsqlCommand(
			"SELECT * FROM trip " +
			"LEFT JOIN destination ON trip.destination_id = destination.id " +
			"ORDER BY id DESC " +
			"LIMIT @n;"
		))
		{
			command.Parameters.AddWithValue("@n", n);
			using (NpgsqlDataReader reader = ExecuteSelect(command))
			{
				List<Trip> trips = new List<Trip>();
				while (reader.Read())
				{
					Destination destination = new Destination(reader.GetInt32(3), reader.GetString(4));
					Trip trip = new Trip(reader.GetInt32(0), reader.GetDateTime(1), destination);
					trips.Add(trip);
				}
				return trips;
			}
		}
	}
}
