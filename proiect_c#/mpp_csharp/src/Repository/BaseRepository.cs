using Npgsql;
using System.Configuration;
using System.Threading.Tasks;

public class BaseRepository
{
	private readonly string connString = ConfigurationManager.ConnectionStrings["postgresql"].ConnectionString;
	private static NpgsqlConnection conn;

	protected BaseRepository()
	{
		SetConn();
	}

	private void SetConn()
	{
		if (conn != null)
			return;
		conn = new NpgsqlConnection(connString);
		conn.Open();
	}

	protected void ExecuteNonQuery(NpgsqlCommand command)
	{
		command.Connection = conn;
		command.Prepare();
		command.ExecuteNonQuery();
	}

	protected  NpgsqlDataReader ExecuteSelect(NpgsqlCommand command)
	{
		command.Connection = conn;
		command.Prepare();
		return command.ExecuteReader();
		 
	}

	protected  int ExecuteScalar(NpgsqlCommand command)
	{
		command.Connection = conn;
		command.Prepare();
		return (int) command.ExecuteScalar();
	}
}