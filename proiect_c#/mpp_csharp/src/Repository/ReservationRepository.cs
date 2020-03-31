using Npgsql;
using System.Collections.Generic;

public class ReservationRepository : BaseRepository, IRepository<Reservation>, IReservationRepository
{
	public void Delete(Reservation reservation)
	{
		NpgsqlCommand command = new NpgsqlCommand("DELETE FROM reservation WHERE id = @id;");
		command.Parameters.AddWithValue("@id", reservation.Id);
		ExecuteNonQuery(command);
	}

	public Reservation Find(int id)
	{
		NpgsqlCommand command = new NpgsqlCommand(
			"SELECT * FROM reservation " +
			"LEFT JOIN trip ON trip.id = reservation.trip_id " +
			"LEFT JOIN destination ON trip.destination_id = destination.id " +
			"WHERE reservation.id = @id;");
		command.Parameters.AddWithValue("@id", id);
		using (NpgsqlDataReader reader = ExecuteSelect(command))
		{
			if (!reader.Read())
				throw new SQLErrorNoEntityFound($"No reservation found with this id = {id}");
			Destination destination = new Destination(reader.GetInt32(7), reader.GetString(8));
			Trip trip = new Trip(reader.GetInt32(4), reader.GetDateTime(5), destination);
			return new Reservation(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), trip);
		}
	}

	public List<Reservation> FindAll()
	{
		NpgsqlCommand command = new NpgsqlCommand(
			"SELECT * FROM reservation " +
			"LEFT JOIN trip ON trip.id = reservation.trip_id " +
			"LEFT JOIN destination ON trip.destination_id = destination.id;"
		);
		using (NpgsqlDataReader reader = ExecuteSelect(command))
		{
			List<Reservation> reservations = new List<Reservation>();
			while (reader.Read())
			{
				Destination destination = new Destination(reader.GetInt32(7), reader.GetString(8));
				Trip trip = new Trip(reader.GetInt32(4), reader.GetDateTime(5), destination);
				Reservation reservation = new Reservation(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), trip);
				reservations.Add(reservation);
			}
			return reservations;
		}
	}

	public List<Reservation> FindLastN(int n)
	{
		NpgsqlCommand command = new NpgsqlCommand(
			"SELECT * FROM reservation " +
			"LEFT JOIN trip ON trip.id = reservation.trip_id " +
			"LEFT JOIN destination ON trip.destination_id = destination.id " +
			"ORDER BY reservation.id DESC " +
			"LIMIT @n;"
		);
		command.Parameters.AddWithValue("@n", n);
		using (NpgsqlDataReader reader = ExecuteSelect(command))
		{
			List<Reservation> reservations = new List<Reservation>();
			while (reader.Read())
			{
				Destination destination = new Destination(reader.GetInt32(7), reader.GetString(8));
				Trip trip = new Trip(reader.GetInt32(4), reader.GetDateTime(5), destination);
				Reservation reservation = new Reservation(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), trip);
				reservations.Add(reservation);
			}
			return reservations;
		}
	}

	public Reservation Insert(Reservation reservation)
	{
		NpgsqlCommand command = new NpgsqlCommand("INSERT INTO reservation(client_name, seats_nr, trip_id) VALUES(@client_name, @seats_nr, @trip_id) RETURNING id;");
		command.Parameters.AddWithValue("@client_name", reservation.ClientName);
		command.Parameters.AddWithValue("@seats_nr", reservation.SeatsNr);
		command.Parameters.AddWithValue("@trip_id", reservation.Trip.Id);
		int result = ExecuteScalar(command);
		reservation.Id = result;
		return reservation;
	}

	public void Update(Reservation reservation)
	{
		NpgsqlCommand command = new NpgsqlCommand("UPDATE reservation SET client_name = @client_name, seats_nr = @seats_nr, trip_id = @trip_id WHERE id = @id;");
		command.Parameters.AddWithValue("@client_name", reservation.ClientName);
		command.Parameters.AddWithValue("@seats_nr", reservation.SeatsNr);
		command.Parameters.AddWithValue("@trip_id", reservation.Trip.Id);
		ExecuteNonQuery(command);
	}

	public List<Reservation> GetReservationsByTrip(Trip trip)
	{
		NpgsqlCommand command = new NpgsqlCommand(
			"SELECT * FROM reservation " +
			"LEFT JOIN trip ON trip.id = reservation.trip_id " +
			"LEFT JOIN destination ON trip.destination_id = destination.id " +
			"WHERE trip.id = @trip_id;"
		);
		command.Parameters.AddWithValue("@trip_id", trip.Id);
		using (NpgsqlDataReader reader = ExecuteSelect(command))
		{
			List<Reservation> reservations = new List<Reservation>();
			while (reader.Read())
			{
				Destination destination = new Destination(reader.GetInt32(7), reader.GetString(8));
				Trip new_trip = new Trip(reader.GetInt32(4), reader.GetDateTime(5), destination);
				Reservation reservation = new Reservation(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), new_trip);
				reservations.Add(reservation);
			}
			return reservations;
		}
	}
}
