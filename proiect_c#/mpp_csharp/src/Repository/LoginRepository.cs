using Npgsql;

class LoginRepository: BaseRepository
{
	public bool Login(string username, string password)
	{
		NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM account WHERE username = @username AND password = @password;");
		command.Parameters.AddWithValue("@username", username);
		command.Parameters.AddWithValue("@password", password);
		using(NpgsqlDataReader reader = ExecuteSelect(command))
		{
			return reader.HasRows;
		}
	}
}
