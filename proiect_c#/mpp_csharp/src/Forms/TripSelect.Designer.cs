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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.tripView)).BeginInit();
			this.SuspendLayout();
			// 
			// destinationBox
			// 
			this.destinationBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.destinationBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.destinationBox.FormattingEnabled = true;
			this.destinationBox.Location = new System.Drawing.Point(102, 44);
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
			this.tripView.Location = new System.Drawing.Point(39, 152);
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
			this.timePick.Location = new System.Drawing.Point(102, 97);
			this.timePick.Name = "timePick";
			this.timePick.Size = new System.Drawing.Size(200, 20);
			this.timePick.TabIndex = 2;
			this.timePick.ValueChanged += new System.EventHandler(this.TimePick_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(36, 47);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Destination";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(36, 103);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Departure";
			// 
			// TripSelect
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.timePick);
			this.Controls.Add(this.tripView);
			this.Controls.Add(this.destinationBox);
			this.Name = "TripSelect";
			this.Text = "TripSelect";
			this.Load += new System.EventHandler(this.TripSelect_Load);
			((System.ComponentModel.ISupportInitialize)(this.tripView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox destinationBox;
		private System.Windows.Forms.DataGridView tripView;
		private System.Windows.Forms.DateTimePicker timePick;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}