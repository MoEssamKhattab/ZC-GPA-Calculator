﻿using System;
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
        public BindingList<string> allCourseCodes;

        public HandleRepeatsForm(string newCourseCode, BindingList<semester> semestersList)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            Label1.Text = $"It seems that the course, {newCourseCode}, is repeated with a different course code than the old one.";
            
            allCourseCodes = new BindingList<string>();
            fillAllCourseCodesList(semestersList, allCourseCodes);
        }
        private void HandleRepeatsForm_Load(object sender, EventArgs e)
        {
            this.courseCodesComboBox.DataSource = allCourseCodes;
            courseCodesComboBox.SelectedIndex = 0;
        }
        private void submitCourseCodeBtn_Click(object sender, EventArgs e)
        {
            OldCourseCode = courseCodesComboBox.Text;
            this.DialogResult = DialogResult.OK;
        }

        public void fillAllCourseCodesList(BindingList<semester> semestersList, BindingList<string> allCourseCodesList)
        {
            foreach (var semester in semestersList)
            {
                foreach (var course in semester.Courses)
                {
                    if (!allCourseCodesList.Contains(course.Code) && course.GpaCredits != 0)
                    {
                        allCourseCodesList.Add(course.Code);
                    }
                }
            }
        }
    }
}
