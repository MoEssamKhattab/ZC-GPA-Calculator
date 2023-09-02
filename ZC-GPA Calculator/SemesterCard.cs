﻿using System.ComponentModel;

namespace ZC_GPA_Calculator
{
    public partial class SemesterCard : UserControl
    {
        bool allowEditing = false;
        bool allowAdding = false;
        BindingList<string> letterGrades;
        public string SemesterTitle { get => semesterTitle.Text; set => semesterTitle.Text = value; }
        public bool AllowAdding { get => allowAdding; set => allowAdding = value; }

        public SemesterCard()
        {
            InitializeComponent();
            letterGrades = new BindingList<string> { "A", "A-", "B+", "B", "B-", "C+", "C", "C-", "F", "P", "I", "IP", "W", "WP", "WF","TR" };
            this.grade.DataSource = letterGrades;

        }
        private void SemesterCard_Load(object sender, EventArgs e)
        {
            int leftMargin = (this.Parent.Width - this.Width) / 2;
            this.Margin = new Padding(leftMargin, 10, 0, 10);

            allowEditing = true;

            if (AllowAdding)
                addCourseBtn.Visible = true;
            
            updateCardHeight();  
        }
        public void updateCardHeight()
        {
            int cardHeaderHeight = cardHeaderPanel.Height;
            int separator = 45;

            // Course table
            int headerHeight = courseTable.ThemeStyle.HeaderStyle.Height;
            int rowHeight = courseTable.ThemeStyle.RowsStyle.Height;
            int courseTableHeight = (courseTable.RowCount * rowHeight) + headerHeight;
            courseTable.Height = courseTableHeight;

            // Add course button
            int addCourseBtnHeight = addCourseBtn.Height;

            // Calculations Table
            int calculationsTableHeight = calculationsTable.ThemeStyle.HeaderStyle.Height + 2* calculationsTable.ThemeStyle.RowsStyle.Height;
            
            // Total card
            int cardHeight = Convert.ToInt16(courseTable.Location.Y) + courseTableHeight + separator + calculationsTableHeight;

            if (allowAdding)
                cardHeight += addCourseBtnHeight;

            this.Height = cardHeight;
        }
        public void fill(BindingList<semester> semesters, int index)
        {
            this.semesterTitle.Text = semesters[index].Name;
            this.courseTable.Rows.Clear();
            this.courseTable.Refresh();

            foreach (var course in semesters[index].Courses.ToList())
            {
                // Fill course table
                var rowIndex = courseTable.Rows.Add();
                courseTable.Rows[rowIndex].Cells["Course"].Value = course.Code;
                courseTable.Rows[rowIndex].Cells["Title"].Value = course.Title;
                courseTable.Rows[rowIndex].Cells["Subtype"].Value = course.SubType.ToString();

                //if (!letterGrades.Contains(course.Grade))
                //{
                //    ((DataGridViewComboBoxCell)courseTable.Rows[rowIndex].Cells["Grade"]).Items.Add(letterGrades);
                //}                
                courseTable.Rows[rowIndex].Cells["Grade"].Value = course.Grade;

                courseTable.Rows[rowIndex].Cells["Credits"].Value = course.Credits.ToString();
                // Need to be further improved (the quality points format)
                courseTable.Rows[rowIndex].Cells["QualityPoints"].Value = String.Format("{0:0.00}", course.calculateQualityPoints().ToString());
            }
            updateGPACalculationsTable(semesters, index);
        }
        public void updateCourseTableQualityPoints(semester semester, int courseIndex)
        {
            double newQualityPoints = semester.Courses[courseIndex].QualityPoints;
            courseTable.Rows[courseIndex].Cells["QualityPoints"].Value = newQualityPoints;
            courseTable.Refresh();
        }
        public void updateGPACalculationsTable(BindingList<semester> semesters, int index)
        {
            this.calculationsTable.Rows.Clear();
            this.calculationsTable.Refresh();

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
        private void courseTable_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (courseTable.IsCurrentCellDirty && courseTable.CurrentCell.OwningColumn == courseTable.Columns["Grade"])     // raise the enent only when the grade is changed
            {
                // This fires the cell value changed handler below
                courseTable.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        
        public event EventHandler CgpaUpdate;       // To update the cGPA label in case of any change
        public event EventHandler GradeChanged;
        private void courseTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (courseTable.Rows.Count != 0 && allowEditing && e.ColumnIndex == courseTable.Columns["Grade"].Index) //allowEditing flag is used to avoid performing the following in case of first uploading the transcript
            {
                int rowIndex = e.RowIndex;
                DataGridViewComboBoxCell gradeComboBox = (DataGridViewComboBoxCell)courseTable.Rows[rowIndex].Cells["Grade"];

                if (gradeComboBox.Value != null)
                {
                    GradeChanged?.Invoke(this, e);
                    courseTable.Invalidate();

                    CgpaUpdate?.Invoke(null, EventArgs.Empty);
                }
            }
        }
        public event EventHandler<Course> CourseAdded;
        private void addCourseBtn_Click(object sender, EventArgs e)
        {
            Course newCourse;
            using (AddCourseForm addCourseForm = new AddCourseForm())
            {
                string CourseCode;
                string CourseTitle;
                CourseSubtype CourseSubtype;
                string CourseGrade;
                int CourseCredits;

                DialogResult dialogResult = addCourseForm.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    CourseCode = addCourseForm.CourseCode;
                    CourseTitle = addCourseForm.CourseTitle;
                    CourseSubtype = addCourseForm.CourseSubtype;
                    CourseGrade = addCourseForm.CourseGrade;
                    CourseCredits = addCourseForm.CourseCredits;

                    newCourse = new Course(CourseCode, CourseTitle, CourseSubtype, CourseGrade, CourseCredits);

                    CourseAdded?.Invoke(this, newCourse);
                    courseTable.Invalidate();

                    CgpaUpdate?.Invoke(null,EventArgs.Empty);
                }
            }
        }
        private int currentRowIndex = 0;
        public event EventHandler<int> CourseDeleted;
        private void courseTable_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            // TODO: to make sure that it's not the header of the table that clicked on

            if (allowAdding && e.Button == MouseButtons.Right && e.RowIndex != -1)  // Allow delete only in case of added semesters 
            {                                                                       // Excluding the header
                courseTable.CurrentCell = courseTable.Rows[e.RowIndex].Cells[0];
                currentRowIndex= e.RowIndex;
                courseTableContextMenuStrip.Show(Cursor.Position);
            }
        }
        private void deleteCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            courseTable.Rows.RemoveAt(currentRowIndex);
            CourseDeleted?.Invoke(this, currentRowIndex);
            updateCardHeight();
        }
        private void courseTable_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode & e.KeyCode)
            {
                case Keys.Delete:
                    e.Handled = true;
                    e.SuppressKeyPress = true;      // Disable the default Delete key event
                    break;
            }
        }
    }
}
