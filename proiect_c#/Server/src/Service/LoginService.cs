

class LoginService
{
	private LoginRepository repository = new LoginRepository();
	public bool Login(string username, string password)
	{
		return repository.Login(username, password);
	}
}

