using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
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
    public partial class SemesterCard : UserControl
    {
        //ComboBox gradeComboBox;
        //DataGridViewComboBoxEditingControl gradeRow;

        bool allowEditing = false;
        int semsterIndexInSemestersList = 0;        //to use in case of any grade update
        public SemesterCard()
        {
            InitializeComponent();
        }

        private void SemesterCard_Load(object sender, EventArgs e)
        {
            string[] letterGrades = new string[] { "A", "A-", "B+", "B", "B-", "C+", "C", "C-", "F", "P" };
            this.grade.DataSource = letterGrades;

            setCardHeight(7, 4);
            allowEditing = true;
        }


        public void setCardHeight(int courseCount, int maxVisibleCourseListElements)
        {
            int headerHeight = courseList.ThemeStyle.HeaderStyle.Height;
            int rowHeight = courseList.ThemeStyle.RowsStyle.Height;

            int courseListHeight;
            if (courseCount >= maxVisibleCourseListElements)
                courseListHeight = (maxVisibleCourseListElements * rowHeight) + headerHeight;
            else
                courseListHeight = (courseCount * rowHeight) + headerHeight;

            int calculationsTableHeight = calculationsTable.ThemeStyle.HeaderStyle.Height + 2* calculationsTable.ThemeStyle.RowsStyle.Height;
            int separator = 50;
            int cardHeight = Convert.ToInt16(courseList.Location.Y) + courseListHeight + separator + calculationsTableHeight;
                
            courseList.Height = courseListHeight;     
            this.Height = cardHeight;
        }

        public void fill(List<semester> semesters, int index)
        {
            this.semesterTitle.Text = $"{semesters[index].Title.ToString()}, {semesters[index].Year}";
            this.courseList.Rows.Clear();
            this.courseList.Refresh();

            this.semsterIndexInSemestersList = index;       //to use in case of any grade update

            foreach (var course in semesters[index].Courses)
            {
                // Fill course table
                var rowIndex = courseList.Rows.Add();
                courseList.Rows[rowIndex].Cells["Course"].Value = course.Code;
                courseList.Rows[rowIndex].Cells["Title"].Value = course.Title;
                courseList.Rows[rowIndex].Cells["Subtype"].Value = course.SubType.ToString();
                courseList.Rows[rowIndex].Cells["Grade"].Value = course.Grade;
                courseList.Rows[rowIndex].Cells["Credits"].Value = course.Credits.ToString();
                // Need to be further improved (the quality points format)
                courseList.Rows[rowIndex].Cells["QualityPoints"].Value = String.Format("{0:0.00}", course.calculateQualityPoints().ToString());
            }
            updateGPACalculationsTable(semesters, index);
        }

        public void updateGPACalculationsTable(List<semester> semesters, int index)
        {
            string[] tremRow = new string[] { "Term" };
            string[] overallRow = new string[] { "Overall" };

            calculationsTable.Rows.Add(tremRow);
            calculationsTable.Rows.Add(overallRow);
            // GPA Calculations
            calculationsTable.Rows[0].Cells["GPACredits"].Value = semesters[index].calculateGPACredits().ToString();
            calculationsTable.Rows[1].Cells["GPACredits"].Value = semesters[index].calculateOverallGPACredits(semesters, index).ToString();
            calculationsTable.Rows[0].Cells["GPA"].Value = semesters[index].calculateGPA().ToString();
            calculationsTable.Rows[1].Cells["GPA"].Value = semesters[index].calculateOverallGPA(semesters, index).ToString();
        }

        // This event handler manually raises the CellValueChanged event 
        // by calling the CommitEdit method. 
        private void courseList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (courseList.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                courseList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void courseList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (courseList.Rows.Count != 0 && allowEditing) //allowEditing flag is used to avoid performing the following in case of first uplading the transcript
            {
                int rowIndex = e.RowIndex;
                DataGridViewComboBoxCell gradeComboBox = (DataGridViewComboBoxCell)courseList.Rows[rowIndex].Cells["Grade"];

                if (gradeComboBox.Value != null)
                {
                    
                    MessageBox.Show($"{rowIndex}     {gradeComboBox.Value}       {this.semesterTitle.Text}");
                    courseList.Invalidate();
                }
            }
        }

        // Handelling the change of any grade value
        //private void courseList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)      
        //{
        //    // get ComboBox Object
        //    gradeComboBox = e.Control as ComboBox;
        //    gradeRow = e.Control as DataGridViewComboBoxEditingControl;

        //    if (gradeComboBox != null ) 
        //    {
        //        //Avoid attachement to multiple Event Handlers
        //        gradeComboBox.SelectedIndexChanged -= new EventHandler(gradeComboBox_LastColumnComboSelectionChanged);
        //        gradeComboBox.SelectedIndexChanged += gradeComboBox_LastColumnComboSelectionChanged;
        //    }
        //}

        //private void gradeComboBox_LastColumnComboSelectionChanged(object? sender, EventArgs e)
        //{
        //    string newGrade = (sender as ComboBox).SelectedItem.ToString();
        //    MessageBox.Show(newGrade);
        //}


    }
}
