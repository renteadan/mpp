namespace csharp.Services.Service
{
	public class LoginService
	{
		private readonly LoginRepository repository = new LoginRepository();

		public bool Login(string username, string password)
		{
			return repository.Login(username, password);
		}
	}
}