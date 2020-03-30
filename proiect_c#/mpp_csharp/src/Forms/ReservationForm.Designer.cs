namespace mpp_csharp.src.Forms
{
	partial class ReservationForm
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
			this.dataView = new System.Windows.Forms.DataGridView();
			this.nameBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.seatsBar = new System.Windows.Forms.TrackBar();
			this.label2 = new System.Windows.Forms.Label();
			this.seatsLabel = new System.Windows.Forms.Label();
			this.reserveButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.seatsBar)).BeginInit();
			this.SuspendLayout();
			// 
			// dataView
			// 
			this.dataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataView.Location = new System.Drawing.Point(12, 12);
			this.dataView.Name = "dataView";
			this.dataView.RowHeadersVisible = false;
			this.dataView.Size = new System.Drawing.Size(193, 426);
			this.dataView.TabIndex = 0;
			// 
			// nameBox
			// 
			this.nameBox.Location = new System.Drawing.Point(327, 40);
			this.nameBox.Name = "nameBox";
			this.nameBox.Size = new System.Drawing.Size(100, 20);
			this.nameBox.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(259, 43);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "Name";
			// 
			// seatsBar
			// 
			this.seatsBar.Location = new System.Drawing.Point(327, 109);
			this.seatsBar.Name = "seatsBar";
			this.seatsBar.Size = new System.Drawing.Size(104, 45);
			this.seatsBar.TabIndex = 3;
			this.seatsBar.ValueChanged += new System.EventHandler(this.SeatsBar_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(262, 109);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 20);
			this.label2.TabIndex = 4;
			this.label2.Text = "Seats";
			// 
			// seatsLabel
			// 
			this.seatsLabel.AutoSize = true;
			this.seatsLabel.Location = new System.Drawing.Point(438, 109);
			this.seatsLabel.Name = "seatsLabel";
			this.seatsLabel.Size = new System.Drawing.Size(0, 13);
			this.seatsLabel.TabIndex = 5;
			// 
			// reserveButton
			// 
			this.reserveButton.Location = new System.Drawing.Point(266, 175);
			this.reserveButton.Name = "reserveButton";
			this.reserveButton.Size = new System.Drawing.Size(75, 23);
			this.reserveButton.TabIndex = 6;
			this.reserveButton.Text = "Reserve";
			this.reserveButton.UseVisualStyleBackColor = true;
			this.reserveButton.Click += new System.EventHandler(this.ReserveButton_Click);
			// 
			// ReservationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.reserveButton);
			this.Controls.Add(this.seatsLabel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.seatsBar);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.nameBox);
			this.Controls.Add(this.dataView);
			this.Name = "ReservationForm";
			this.Text = "ReservationForm";
			this.Load += new System.EventHandler(this.ReservationForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.seatsBar)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataView;
		private System.Windows.Forms.TextBox nameBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TrackBar seatsBar;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label seatsLabel;
		private System.Windows.Forms.Button reserveButton;
	}
}