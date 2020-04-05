using csharp.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chsarp.Client
{
	public interface IForm
	{
		void HandleResponse(IResponse response);
	}
}
