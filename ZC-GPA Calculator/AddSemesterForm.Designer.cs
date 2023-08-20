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
            this.addSemesterBtn = new Guna.UI2.WinForms.Guna2Button();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.yearDatePicker = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.semesterComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.SuspendLayout();
            // 
            // addSemesterBtn
            // 
            this.addSemesterBtn.Animated = true;
            this.addSemesterBtn.BackColor = System.Drawing.Color.Transparent;
            this.addSemesterBtn.BorderRadius = 8;
            this.addSemesterBtn.CustomizableEdges = customizableEdges1;
            this.addSemesterBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.addSemesterBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.addSemesterBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.addSemesterBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.addSemesterBtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(8)))), ((int)(((byte)(8)))));
            this.addSemesterBtn.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.addSemesterBtn.ForeColor = System.Drawing.Color.White;
            this.addSemesterBtn.Location = new System.Drawing.Point(194, 205);
            this.addSemesterBtn.Name = "addSemesterBtn";
            this.addSemesterBtn.ShadowDecoration.CustomizableEdges = customizableEdges2;
            this.addSemesterBtn.Size = new System.Drawing.Size(180, 45);
            this.addSemesterBtn.TabIndex = 2;
            this.addSemesterBtn.Text = "Add Semester";
            this.addSemesterBtn.Click += new System.EventHandler(this.addSemesterBtn_Click);
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(92, 58);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(90, 30);
            this.guna2HtmlLabel1.TabIndex = 4;
            this.guna2HtmlLabel1.Text = "Semester";
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(92, 122);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(45, 30);
            this.guna2HtmlLabel2.TabIndex = 5;
            this.guna2HtmlLabel2.Text = "Year";
            // 
            // yearDatePicker
            // 
            this.yearDatePicker.Checked = true;
            this.yearDatePicker.CustomizableEdges = customizableEdges3;
            this.yearDatePicker.FillColor = System.Drawing.Color.White;
            this.yearDatePicker.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.yearDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.yearDatePicker.Location = new System.Drawing.Point(227, 119);
            this.yearDatePicker.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.yearDatePicker.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.yearDatePicker.Name = "yearDatePicker";
            this.yearDatePicker.ShadowDecoration.CustomizableEdges = customizableEdges4;
            this.yearDatePicker.Size = new System.Drawing.Size(250, 36);
            this.yearDatePicker.TabIndex = 1;
            this.yearDatePicker.Value = new System.DateTime(2020, 8, 12, 23, 50, 0, 0);
            // 
            // semesterComboBox
            // 
            this.semesterComboBox.BackColor = System.Drawing.Color.Transparent;
            this.semesterComboBox.CustomizableEdges = customizableEdges5;
            this.semesterComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.semesterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.semesterComboBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.semesterComboBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.semesterComboBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.semesterComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.semesterComboBox.ItemHeight = 30;
            this.semesterComboBox.Location = new System.Drawing.Point(227, 57);
            this.semesterComboBox.Name = "semesterComboBox";
            this.semesterComboBox.ShadowDecoration.CustomizableEdges = customizableEdges6;
            this.semesterComboBox.Size = new System.Drawing.Size(250, 36);
            this.semesterComboBox.TabIndex = 0;
            // 
            // AddSemesterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 307);
            this.Controls.Add(this.semesterComboBox);
            this.Controls.Add(this.yearDatePicker);
            this.Controls.Add(this.guna2HtmlLabel2);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.addSemesterBtn);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AddSemesterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New Semester";
            this.Load += new System.EventHandler(this.AddSemesterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button addSemesterBtn;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2DateTimePicker yearDatePicker;
        private Guna.UI2.WinForms.Guna2ComboBox semesterComboBox;
    }
}