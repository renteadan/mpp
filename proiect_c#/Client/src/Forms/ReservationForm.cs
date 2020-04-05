using chsarp.Client;
using csharp.Domain;
using csharp.Networking;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace csharp.Client
{
	public partial class ReservationForm : Form, IForm
	{

		private readonly ClientListener listener;
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

		public ReservationForm(Trip trip, ClientListener Listener)
		{
			InitializeComponent();
			listener = Listener;
			this.trip = trip;
			listener.AddForm(this);
			listener.ReloadData += ReloadData;
		}

		public void ReloadData(object sender, ReloadDataEventArgs e)
		{
			BeginInvoke(new Action(() => {
				LoadData();
			}));
		}

		private void LoadData()
		{
			QueryReservations query = new QueryReservations(trip);
			listener.SendQuery(query);
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
			Reservation reservation = new Reservation(name, seats, trip);
			ReservationAdd add = new ReservationAdd(reservation);
			listener.SendAdd(add);
		}

		private void ReserveButton_Click(object sender, EventArgs e)
		{
			MakeReservation();
		}

		public void HandleResponse(IResponse response)
		{
			if(response is ResponseReservations responseReservations)
			{
				List<Reservation> trips = responseReservations.reservations;
				dataView.DataSource = trips;
				dataView.Columns.Remove(dataView.Columns["Id"]);
				dataView.Columns.Remove(dataView.Columns["Trip"]);
				seatsBar.Maximum = responseReservations.RemainingSeats;
				seatsBar.Minimum = 0;
				seatsBar.Value = 0;
				seatsLabel.Text = "0";
			} else if(response is ResponseReloadData)
			{
				LoadData();
			}
		}
	}
}
