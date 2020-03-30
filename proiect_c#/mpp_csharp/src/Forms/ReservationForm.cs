using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mpp_csharp.src.Forms
{
	public partial class ReservationForm : Form
	{

		private readonly ReservationService service = new ReservationService();
		private readonly Trip trip;
		public ReservationForm()
		{
			InitializeComponent();
		}

		public ReservationForm(Trip trip)
		{
			InitializeComponent();
			this.trip = trip;
		}

		private void LoadData()
		{
			dataView.DataSource = service.getReservationsByTrip(trip);
			dataView.Columns.Remove(dataView.Columns["Id"]);
			dataView.Columns.Remove(dataView.Columns["Trip"]);
			seatsBar.Maximum = service.CountRemainingSeatsOnTrip(trip);
			seatsBar.Minimum = 1;
			seatsBar.Value = 1;
			seatsLabel.Text = "1";
		}

		private void ReservationForm_Load(object sender, EventArgs e)
		{
			LoadData();
		}

		private void SeatsBar_ValueChanged(object sender, EventArgs e)
		{
			seatsLabel.Text = seatsBar.Value.ToString();
		}

		private void MakeReservation()
		{
			int seats = seatsBar.Value;
			string name = nameBox.Text;
			service.Insert(new Reservation(name, seats, trip));
		}

		private void ReserveButton_Click(object sender, EventArgs e)
		{
			MakeReservation();
			LoadData();
		}
	}
}
