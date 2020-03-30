using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace mpp_csharp.src.Forms
{
	public partial class TripSelect : Form
	{
		private readonly DestinationService destinationService = new DestinationService();
		private readonly TripService tripService = new TripService();
		public TripSelect()
		{
			InitializeComponent();
		}

		private void TripSelect_Load(object sender, EventArgs e)
		{
			destinationBox.DataSource = destinationService.FindAll();
			timePick.Value = DateTime.Now;
		}

		private void LoadTable()
		{
			Destination destination = (Destination) destinationBox.SelectedItem;
			DateTime date = timePick.Value;
			List<Trip> trips = tripService.GetTripsByDestinationAndDate(destination, date);
			tripView.DataSource = trips;
			tripView.Columns.Remove(tripView.Columns["Id"]);
		}

		private void DestinationBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadTable();
		}

		private void TripView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
		}

		private void TimePick_ValueChanged(object sender, EventArgs e)
		{
			LoadTable();
		}

		private void TripView_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			Trip trip = (Trip)tripView.Rows[e.RowIndex].DataBoundItem;
			new ReservationForm(trip).ShowDialog();
		}
	}
}
