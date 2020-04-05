using Npgsql;
using System.Configuration;
using System.Threading.Tasks;

public class BaseRepository
{
	private readonly string connString = ConfigurationManager.ConnectionStrings["postgresql"].ConnectionString;

	protected BaseRepository()
	{
	}

	protected void ExecuteNonQuery(NpgsqlCommand command)
	{
		using (NpgsqlConnection conn = new NpgsqlConnection(connString))
		{
			conn.Open();
			command.Connection = conn;
			command.Prepare();
			command.ExecuteNonQuery();
		}
	}

	protected NpgsqlDataReader ExecuteSelect(NpgsqlCommand command)
	{
		NpgsqlConnection conn = new NpgsqlConnection(connString);
		conn.Open();
		command.Connection = conn;
		command.Prepare();
		return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
	}

	protected  int ExecuteScalar(NpgsqlCommand command)
	{
		using (NpgsqlConnection conn = new NpgsqlConnection(connString))
		{
			conn.Open();
			command.Connection = conn;
			command.Prepare();
			return (int)command.ExecuteScalar();
		}
	}
}