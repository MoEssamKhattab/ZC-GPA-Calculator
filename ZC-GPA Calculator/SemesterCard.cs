using System;
using System.ComponentModel;

namespace ZC_GPA_Calculator
{
    public partial class SemesterCard : UserControl
    {
        bool allowEditing = false;
        bool allowAdding = false;
        BindingList<string> letterGrades;
        const int maxVisibleCourses = 6;
        public string SemesterTitle { get => semesterTitle.Text; set => semesterTitle.Text = value; }
        public bool AllowAdding { get => allowAdding; set => allowAdding = value; }

        public SemesterCard()
        {
            InitializeComponent();
            letterGrades = new BindingList<string> { "A", "A-", "B+", "B", "B-", "C+", "C", "C-", "F" };
            this.grade.DataSource = letterGrades;
        }
        private void SemesterCard_Load(object sender, EventArgs e)
        {
            int leftMargin = (this.Parent.Width - this.Width) / 2;
            this.Margin = new Padding(leftMargin, 10, 0, 10);

            allowEditing = true;

            if (AllowAdding)
                addCourseBtn.Visible = true;
            
            updateCardHeight(maxVisibleCourses);  
        }
        public void updateCardHeight(int maxVisibleCourses)
        {
            int cardHeaderHeight = cardHeaderPanel.Height;
            int separator = 45;

            // Course table
            int headerHeight = courseTable.ThemeStyle.HeaderStyle.Height;
            int rowHeight = courseTable.ThemeStyle.RowsStyle.Height;
            int courseTableHeight = (Math.Min(maxVisibleCourses ,courseTable.RowCount) * rowHeight) + headerHeight;
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
            //this.courseTable.Rows.Clear();
            //this.courseTable.Refresh();

            foreach (var course in semesters[index].Courses.ToList())
            {
                // Fill course table
                var rowIndex = courseTable.Rows.Add();
                courseTable.Rows[rowIndex].Cells["Course"].Value = course.Code;
                courseTable.Rows[rowIndex].Cells["Title"].Value = course.Title;
                courseTable.Rows[rowIndex].Cells["Subtype"].Value = course.Subtype.ToString();

                if (!letterGrades.Contains(course.Grade))
                {
                    letterGrades.Add(course.Grade);
                }
                courseTable.Rows[rowIndex].Cells["Grade"].Value = course.Grade;

                courseTable.Rows[rowIndex].Cells["Credits"].Value = course.Credits.ToString();
                courseTable.Rows[rowIndex].Cells["QualityPoints"].Value = course.calculateQualityPoints().ToString("0.00");
            }
            updateGPACalculationsTable(semesters, index);
            updateCardHeight(maxVisibleCourses);
        }
        public void addNewCourse(BindingList<semester> semesters, int semesterIndex)
        {
            Course course = semesters[semesterIndex].Courses[semesters[semesterIndex].Courses.Count - 1];

            var rowIndex = courseTable.Rows.Add();
            courseTable.Rows[rowIndex].Cells["Course"].Value = course.Code;
            courseTable.Rows[rowIndex].Cells["Title"].Value = course.Title;
            courseTable.Rows[rowIndex].Cells["Subtype"].Value = course.Subtype.ToString();
            courseTable.Rows[rowIndex].Cells["Grade"].Value = course.Grade;
            courseTable.Rows[rowIndex].Cells["Credits"].Value = course.Credits.ToString();
            courseTable.Rows[rowIndex].Cells["QualityPoints"].Value = course.calculateQualityPoints().ToString("0.00");

            updateGPACalculationsTable(semesters, semesterIndex);
            updateCardHeight(maxVisibleCourses);
        }
        public void updateCourseTableQualityPoints(semester semester, int courseIndex)
        {
            double newQualityPoints = semester.Courses[courseIndex].QualityPoints;
            courseTable.Rows[courseIndex].Cells["QualityPoints"].Value = newQualityPoints.ToString("0.00");
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
            calculationsTable.Rows[1].Cells["GPACredits"].Value = semester.calculateOverallGPACredits(semesters, index).ToString();
            calculationsTable.Rows[0].Cells["AttemptedCredits"].Value = semesters[index].calculateAttemptedCredits().ToString();
            calculationsTable.Rows[1].Cells["AttemptedCredits"].Value = semester.calculateOverallAttemptedCredits(semesters, index).ToString();
            calculationsTable.Rows[0].Cells["EarnedCredits"].Value = semesters[index].calculateEarnedCredits().ToString();
            calculationsTable.Rows[1].Cells["EarnedCredits"].Value = semester.calculateOverallEarnedCredits(semesters, index).ToString();
            calculationsTable.Rows[0].Cells["TotalCredits"].Value = semesters[index].calculateTotalCredits().ToString();
            calculationsTable.Rows[1].Cells["TotalCredits"].Value = semester.calculateOverallTotalCredits(semesters, index).ToString();
            calculationsTable.Rows[0].Cells["TransferCredits"].Value = semesters[index].calculateTransferCredits().ToString();
            calculationsTable.Rows[1].Cells["TransferCredits"].Value = semester.calculateOverallTransferCredits(semesters, index).ToString();
            calculationsTable.Rows[0].Cells["Quality_Points"].Value = semesters[index].calculateQualityPoints().ToString("0.00");
            calculationsTable.Rows[1].Cells["Quality_Points"].Value = semester.calculateOverallQualityPoints(semesters, index).ToString("0.00");
            calculationsTable.Rows[0].Cells["GPA"].Value = semesters[index].calculateGPA().ToString("0.0000");
            calculationsTable.Rows[1].Cells["GPA"].Value = semester.calculateOverallGPA(semesters, index).ToString("0.0000");
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

                    //CgpaUpdate?.Invoke(null,EventArgs.Empty);     //No need as it already gets invoked with the grade change in course table
                }
            }
        }
        private int currentRowIndex = 0;
        public event EventHandler<int> CourseDeleted;
        private void courseTable_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
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
            updateCardHeight(maxVisibleCourses);
        }
        private void courseTable_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode & e.KeyCode)
            {
                case Keys.Delete:
                    e.Handled = true;
                    e.SuppressKeyPress = true;      // Disable the default event of the delete key 
                    break;
            }
        }
    }
}
