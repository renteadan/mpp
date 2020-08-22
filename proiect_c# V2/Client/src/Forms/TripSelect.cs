using chsarp.Client;
using csharp.Domain;
using csharp.Networking;
using System;
using System.Windows.Forms;

namespace csharp.Client
{
	public partial class TripSelect : Form, IObserver
	{
		private readonly IClientWorker listener;
		private Destination destination;
		private DateTime date;
		public TripSelect()
		{
			InitializeComponent();
		}

		public TripSelect(IClientWorker Listener)
		{
			InitializeComponent();
			listener = Listener;
			listener.ReloadData += ReloadData;
		}

		public void ReloadData(object sender, ReloadDataEventArgs e)
		{
			BeginInvoke(new Action(() => LoadTable()));
		}

		private void TripSelect_Load(object sender, EventArgs e)
		{
			timePick.Value = DateTime.Now;
			destinationBox.DataSource = listener.GetDestinations();
		}

		private void LoadTable()
		{
			destination = (Destination)destinationBox.SelectedItem;
			date = timePick.Value;
			if (destination == null || date == null)
				return;
			tripView.DataSource = listener.GetTrips(destination, date);
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
			new ReservationForm(trip, listener).Show();
			LoadTable();
		}

		public void UpdateObs()
		{
			LoadTable();
		}

		private void TripSelect_FormClosing(object sender, FormClosingEventArgs e)
		{
			listener.ReloadData -= ReloadData;
		}
	}
}
