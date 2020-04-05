using chsarp.Client;
using System;
using System.CodeDom.Compiler;
using System.Threading;
using System.Windows.Forms;

namespace csharp.Client
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      ClientListener listener = new ClientListener();
      listener.Start();
      LoginForm form = new LoginForm(listener);
      Application.Run(form);
    }
  }
}
