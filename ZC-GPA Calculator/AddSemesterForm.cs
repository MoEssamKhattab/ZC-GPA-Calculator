using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZC_GPA_Calculator
{
    public partial class AddSemesterForm : Form
    {
        public Semester SemesterTitle {get; set;}
        public int SemesterYear { get; set;}
        public AddSemesterForm()
        {
            InitializeComponent();
            this.MaximizeBox= false;
            this.MinimizeBox= false;

            this.yearDatePicker.Format = DateTimePickerFormat.Custom;
            this.yearDatePicker.CustomFormat = "yyyy";
            this.yearDatePicker.ShowUpDown = true;
        }
        private void AddSemesterForm_Load(object sender, EventArgs e)
        {
            
            this.semesterComboBox.DataSource = Enum.GetNames(typeof(Semester));
            this.semesterComboBox.SelectedIndex= 0;
        }

        private void addSemesterBtn_Click(object sender, EventArgs e)
        {
            SemesterTitle = (Semester)Enum.Parse(typeof(Semester) ,this.semesterComboBox.Text);
            SemesterYear = this.yearDatePicker.Value.Year;
            this.DialogResult = DialogResult.OK;
        }
    }
}
