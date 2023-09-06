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
    public partial class HandleRepeatsForm : Form
    {
        public string OldCourseCode { get; set; }
        public BindingList<string> AllCourseCodes { get; set; }

        public HandleRepeatsForm(string newCourseCode, BindingList<string> allCourseCodes)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            Label1.Text = $"It seems that the course, {newCourseCode}, is repeated with a different course code than the old one.";
            
            AllCourseCodes = allCourseCodes;
            AllCourseCodes.Remove(newCourseCode);
        }

        private void submitCourseCodeBtn_Click(object sender, EventArgs e)
        {
            OldCourseCode = courseCodesComboBox.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void HandleRepeatsForm_Load(object sender, EventArgs e)
        {
            this.courseCodesComboBox.DataSource = AllCourseCodes;
            courseCodesComboBox.SelectedIndex = 0;
        }
    }
}
