namespace mpp_csharp.src.Forms
{
	partial class TripSelect
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.destinationBox = new System.Windows.Forms.ComboBox();
			this.tripView = new System.Windows.Forms.DataGridView();
			this.timePick = new System.Windows.Forms.DateTimePicker();
			((System.ComponentModel.ISupportInitialize)(this.tripView)).BeginInit();
			this.SuspendLayout();
			// 
			// destinationBox
			// 
			this.destinationBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.destinationBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.destinationBox.FormattingEnabled = true;
			this.destinationBox.Location = new System.Drawing.Point(51, 44);
			this.destinationBox.Name = "destinationBox";
			this.destinationBox.Size = new System.Drawing.Size(121, 21);
			this.destinationBox.TabIndex = 0;
			this.destinationBox.SelectedIndexChanged += new System.EventHandler(this.DestinationBox_SelectedIndexChanged);
			// 
			// tripView
			// 
			this.tripView.AllowUserToAddRows = false;
			this.tripView.AllowUserToDeleteRows = false;
			this.tripView.AllowUserToResizeRows = false;
			this.tripView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.tripView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
			this.tripView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.tripView.Location = new System.Drawing.Point(51, 152);
			this.tripView.Name = "tripView";
			this.tripView.ReadOnly = true;
			this.tripView.RowHeadersVisible = false;
			this.tripView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
			this.tripView.Size = new System.Drawing.Size(276, 150);
			this.tripView.TabIndex = 1;
			this.tripView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TripView_CellClick);
			this.tripView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TripView_CellContentClick);
			// 
			// timePick
			// 
			this.timePick.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.timePick.Location = new System.Drawing.Point(51, 99);
			this.timePick.Name = "timePick";
			this.timePick.Size = new System.Drawing.Size(200, 20);
			this.timePick.TabIndex = 2;
			this.timePick.ValueChanged += new System.EventHandler(this.TimePick_ValueChanged);
			// 
			// TripSelect
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.timePick);
			this.Controls.Add(this.tripView);
			this.Controls.Add(this.destinationBox);
			this.Name = "TripSelect";
			this.Text = "TripSelect";
			this.Load += new System.EventHandler(this.TripSelect_Load);
			((System.ComponentModel.ISupportInitialize)(this.tripView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox destinationBox;
		private System.Windows.Forms.DataGridView tripView;
		private System.Windows.Forms.DateTimePicker timePick;
	}
}