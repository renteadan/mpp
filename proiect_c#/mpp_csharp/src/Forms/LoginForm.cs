using mpp_csharp.src.Forms;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace mpp_csharp
{
  public partial class LoginForm : Form
  {
    private readonly LoginService service = new LoginService();
    public LoginForm()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
    }

    private void Login()
    {
      string username = userBox.Text;
      string password = passBox.Text;
      if (service.Login(username, password))
      {
        MessageBox.Show("Login succes!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        TripSelect tripForm = new TripSelect();
        Hide();
        tripForm.ShowDialog();
        Close();
      }
      else
      {
        MessageBox.Show("Login failed\nWrong credentials!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
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
  }
}
