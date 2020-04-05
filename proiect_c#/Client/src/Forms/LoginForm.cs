using chsarp.Client;
using csharp.Networking;
using System;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;


namespace csharp.Client
{
  public partial class LoginForm : Form, IForm
  {
    private readonly ClientListener listener;
    public LoginForm()
    {
      InitializeComponent();
      listener.AddForm(this);
    }

    public LoginForm(ClientListener listener)
    {
      InitializeComponent();
      this.listener = listener;
      listener.AddForm(this);
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
      QueryLogin queryLogin = new QueryLogin(username, password);
      listener.SendQuery(queryLogin);
    }

    private void LoginButton_Click(object sender, EventArgs e)
    {
      Login();
    }

    private void PassBox_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Enter)
      {
        Login();
      }
    }

    private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      listener.Dispose();
    }
  }
}
