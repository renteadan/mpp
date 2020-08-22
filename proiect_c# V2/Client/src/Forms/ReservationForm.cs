using chsarp.Client;
using csharp.Domain;
using csharp.Networking;
using System;
using System.Windows.Forms;

namespace csharp.Client
{
	public partial class ReservationForm : Form, IObserver
	{

		private readonly IClientWorker listener;
		private Trip trip;
		public ReservationForm()
		{
			InitializeComponent();
		}

		public ReservationForm(Trip trip, IClientWorker Listener)
		{
			InitializeComponent();
			listener = Listener;
			this.trip = trip;
			listener.ReloadData += ReloadData;
		}

		public void ReloadData(object sender, ReloadDataEventArgs e)
		{
			BeginInvoke(new Action(() => LoadData()));
		}
		private void LoadData()
		{
			dataView.DataSource = listener.GetReservations(trip);
			dataView.Columns.Remove(dataView.Columns["Id"]);
			dataView.Columns.Remove(dataView.Columns["Trip"]);
			seatsBar.Maximum = listener.GetRemainingSeats(trip);
			seatsBar.Minimum = 0;
			seatsBar.Value = 0;
			seatsLabel.Text = "0";

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
			listener.AddReservation(reservation);
		}

		private void ReserveButton_Click(object sender, EventArgs e)
		{
			MakeReservation();
		}

		public void UpdateObs()
		{
			LoadData();
		}

		private void ReservationForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			listener.ReloadData -= ReloadData;
		}
	}
}
