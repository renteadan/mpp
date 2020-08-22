using chsarp.Client;
using csharp.Networking;
using System;
using System.Windows.Forms;


namespace csharp.Client
{
	public partial class LoginForm : Form, IObserver
	{
		private readonly IClientWorker listener;
		public LoginForm()
		{
			InitializeComponent();
		}

		public LoginForm(IClientWorker listener)
		{
			InitializeComponent();
			this.listener = listener;
		}

		public void HandleResponse(IResponse response)
		{
			if (response is ResponseLogin responseLogin)
			{
				if (responseLogin.Success)
					LoginSuccess();
				else
					LoginFailed();
			}
		}

		private void LoginSuccess()
		{
			MessageBox.Show("Login succes!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			TripSelect tripForm = new TripSelect(listener);
			Hide();
			tripForm.ShowDialog();
			Close();
		}

		private void LoginFailed()
		{
			MessageBox.Show("Login failed\nWrong credentials!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private void Login()
		{
			string username = userBox.Text;
			string password = passBox.Text;
			if (listener.Login(username, password))
				LoginSuccess();
			else
				LoginFailed();
		}

		private void LoginButton_Click(object sender, EventArgs e)
		{
			Login();
		}

		private void PassBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				Login();
			}
		}

		private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
		{
		}

		public void UpdateObs()
		{
		}
	}
}
