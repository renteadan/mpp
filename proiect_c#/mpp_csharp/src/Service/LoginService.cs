using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class LoginService
{
	private LoginRepository repository = new LoginRepository();
	public bool Login(string username, string password)
	{
		return repository.Login(username, password);
	}
}

