using chsarp.Client;
using csharp.Domain;
using csharp.Networking;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace csharp.Client
{
	public partial class TripSelect : Form, IForm
	{
		private readonly ClientListener listener;
		public TripSelect()
		{
			InitializeComponent();
		}

		public TripSelect(ClientListener Listener)
		{
			InitializeComponent();
			listener = Listener;
			listener.AddForm(this);
			listener.ReloadData += ReloadData;
		}

		public void ReloadData(object sender, ReloadDataEventArgs e)
		{
			BeginInvoke(new Action(() => {
				LoadTable();
			}));
		}

		private void TripSelect_Load(object sender, EventArgs e)
		{
			QueryDestinations query = new QueryDestinations();
			listener.SendQuery(query);
			timePick.Value = DateTime.Now;
		}

		private void LoadTable()
		{
			Destination destination = (Destination)destinationBox.SelectedItem;
			DateTime date = timePick.Value;
			QueryTrips query = new QueryTrips(destination, date);
			listener.SendQuery(query);
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

		public void HandleResponse(IResponse response)
		{
			if(response is ResponseTrips responseTrips)
			{
				List<Trip> trips = responseTrips.trips;
				tripView.DataSource = trips;
				tripView.Columns.Remove(tripView.Columns["Id"]);
			} else if(response is ResponseDestinations responseDestinations)
			{
				destinationBox.DataSource = responseDestinations.destinations;
			} else if(response is ResponseReloadData)
			{
				LoadTable();
			}
		}
	}
}
