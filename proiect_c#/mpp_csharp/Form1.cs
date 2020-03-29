using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace mpp_csharp
{
  public partial class Form1 : Form
  {
    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      Logger logger = new Logger("CApp");
      logger.Error(new Exception("CEva"));
    }
  }
}
