using System;
using System.Windows.Forms;

namespace mpp_csharp.src.Forms
{
	public partial class ReservationForm : Form
	{

		private readonly ReservationService service = new ReservationService(new ReservationRepository(), new ReservationValidator());
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
			dataView.DataSource = service.GetReservationsByTrip(trip);
			dataView.Columns.Remove(dataView.Columns["Id"]);
			dataView.Columns.Remove(dataView.Columns["Trip"]);
			seatsBar.Maximum = service.CountRemainingSeatsOnTrip(trip);
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
			service.Insert(new Reservation(name, seats, trip));
		}

		private void ReserveButton_Click(object sender, EventArgs e)
		{
			MakeReservation();
			LoadData();
		}
	}
}
