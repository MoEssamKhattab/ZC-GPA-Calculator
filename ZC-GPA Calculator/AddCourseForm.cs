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
    public partial class AddCourseForm : Form
    {
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public CourseSubtype CourseSubtype { get; set; }
        public string CourseGrade { get; set; }
        public byte CourseCredits { get; set; }

        public AddCourseForm()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void AddCourseForm_Load(object sender, EventArgs e)
        {
            subtypeComboBox.DataSource = Enum.GetNames(typeof(CourseSubtype));
            subtypeComboBox.SelectedIndex= 0;

            string[] letterGrades = new string[] { "A", "A-", "B+", "B", "B-", "C+", "C", "C-", "F", "P" };
            gradeComboBox.DataSource = letterGrades;
            gradeComboBox.SelectedIndex = 0;
        }

        private void addCourseBtn_Click(object sender, EventArgs e)
        {
            CourseCode= courseCodeTxt.Text;
            CourseTitle= courseTitleTxt.Text;
            CourseSubtype = (CourseSubtype)Enum.Parse(typeof(CourseSubtype),subtypeComboBox.Text);
            CourseGrade = gradeComboBox.Text;
            CourseCredits = Convert.ToByte(creditsNumericUpDown.Value);
            this.DialogResult = DialogResult.OK;
        }
    }
}
