namespace ZC_GPA_Calculator
{
    partial class AddSemesterForm
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
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddSemesterForm));
			addSemesterBtn = new Guna.UI2.WinForms.Guna2Button();
			guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
			guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
			yearDatePicker = new Guna.UI2.WinForms.Guna2DateTimePicker();
			semesterComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
			SuspendLayout();
			// 
			// addSemesterBtn
			// 
			addSemesterBtn.Animated = true;
			addSemesterBtn.BackColor = Color.Transparent;
			addSemesterBtn.BorderRadius = 8;
			addSemesterBtn.CustomizableEdges = customizableEdges1;
			addSemesterBtn.DisabledState.BorderColor = Color.DarkGray;
			addSemesterBtn.DisabledState.CustomBorderColor = Color.DarkGray;
			addSemesterBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
			addSemesterBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
			addSemesterBtn.FillColor = Color.FromArgb(8, 8, 8);
			addSemesterBtn.Font = new Font("Arial Rounded MT Bold", 10F, FontStyle.Regular, GraphicsUnit.Point);
			addSemesterBtn.ForeColor = Color.White;
			addSemesterBtn.Location = new Point(194, 205);
			addSemesterBtn.Name = "addSemesterBtn";
			addSemesterBtn.ShadowDecoration.CustomizableEdges = customizableEdges2;
			addSemesterBtn.Size = new Size(180, 45);
			addSemesterBtn.TabIndex = 2;
			addSemesterBtn.Text = "Add Semester";
			addSemesterBtn.Click += addSemesterBtn_Click;
			// 
			// guna2HtmlLabel1
			// 
			guna2HtmlLabel1.BackColor = Color.Transparent;
			guna2HtmlLabel1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
			guna2HtmlLabel1.Location = new Point(92, 58);
			guna2HtmlLabel1.Name = "guna2HtmlLabel1";
			guna2HtmlLabel1.Size = new Size(90, 30);
			guna2HtmlLabel1.TabIndex = 4;
			guna2HtmlLabel1.Text = "Semester";
			// 
			// guna2HtmlLabel2
			// 
			guna2HtmlLabel2.BackColor = Color.Transparent;
			guna2HtmlLabel2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
			guna2HtmlLabel2.Location = new Point(92, 122);
			guna2HtmlLabel2.Name = "guna2HtmlLabel2";
			guna2HtmlLabel2.Size = new Size(45, 30);
			guna2HtmlLabel2.TabIndex = 5;
			guna2HtmlLabel2.Text = "Year";
			// 
			// yearDatePicker
			// 
			yearDatePicker.Checked = true;
			yearDatePicker.CustomizableEdges = customizableEdges3;
			yearDatePicker.FillColor = Color.White;
			yearDatePicker.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
			yearDatePicker.Format = DateTimePickerFormat.Long;
			yearDatePicker.Location = new Point(227, 119);
			yearDatePicker.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
			yearDatePicker.MinDate = new DateTime(2024, 1, 1, 0, 0, 0, 0);
			yearDatePicker.Name = "yearDatePicker";
			yearDatePicker.ShadowDecoration.CustomizableEdges = customizableEdges4;
			yearDatePicker.Size = new Size(250, 36);
			yearDatePicker.TabIndex = 1;
			yearDatePicker.Value = new DateTime(2024, 1, 1, 0, 0, 0, 0);
			// 
			// semesterComboBox
			// 
			semesterComboBox.BackColor = Color.Transparent;
			semesterComboBox.CustomizableEdges = customizableEdges5;
			semesterComboBox.DrawMode = DrawMode.OwnerDrawFixed;
			semesterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			semesterComboBox.FocusedColor = Color.FromArgb(94, 148, 255);
			semesterComboBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
			semesterComboBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
			semesterComboBox.ForeColor = Color.FromArgb(68, 88, 112);
			semesterComboBox.ItemHeight = 30;
			semesterComboBox.Location = new Point(227, 57);
			semesterComboBox.Name = "semesterComboBox";
			semesterComboBox.ShadowDecoration.CustomizableEdges = customizableEdges6;
			semesterComboBox.Size = new Size(250, 36);
			semesterComboBox.TabIndex = 0;
			// 
			// AddSemesterForm
			// 
			AutoScaleDimensions = new SizeF(11F, 28F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(568, 307);
			Controls.Add(semesterComboBox);
			Controls.Add(yearDatePicker);
			Controls.Add(guna2HtmlLabel2);
			Controls.Add(guna2HtmlLabel1);
			Controls.Add(addSemesterBtn);
			Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(4);
			Name = "AddSemesterForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Add New Semester";
			Load += AddSemesterForm_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Guna.UI2.WinForms.Guna2Button addSemesterBtn;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2DateTimePicker yearDatePicker;
        private Guna.UI2.WinForms.Guna2ComboBox semesterComboBox;
    }
}