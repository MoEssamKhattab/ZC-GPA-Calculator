﻿using System.ComponentModel;
using static ZC_GPA_Calculator.GradeCalculator;

namespace ZC_GPA_Calculator;

public partial class SemesterCard : UserControl
{
    bool allowEditing = false;
    bool allowAdding = false;
    static List<string> letterGrades = new List<string> { "A", "A-", "B+", "B", "B-", "C+", "C", "C-", "F" };
    static int maxVisibleCourses = 6;
    public string SemesterTitle { get => semesterTitle.Text; set => semesterTitle.Text = value; }
    public bool AllowAdding { get => allowAdding; set => allowAdding = value; }
    public SemesterCard()
    {
        InitializeComponent();
        InitializeGradeComboboxItems(letterGrades);

        Utilities.SetDoubleBuffered(semesterCardTableLayoutPanel);   // to reduse graphics flicker when resizing
        Utilities.SetDoubleBuffered(CourseTable);
        Utilities.SetDoubleBuffered(calculationsTable);
    }
    private void SemesterCard_Load(object sender, EventArgs e)
    {
        allowEditing = true;

        if (AllowAdding) addCourseBtn.Visible = true;

        UpdateCardHeight(maxVisibleCourses);
    }
    public void InitializeGradeComboboxItems(List<string> grades)
    {
        foreach (string grade in grades) 
        {
            this.grade.Items.Add(grade);
        }
    }
    /// <summary>
    /// This method is used to add any special grade that found in the transcript and not found in the basic letterGrades list (eg. W, WP, WF, I, TR, ...) 
    /// </summary>
    /// <param name="rowIndex">The row of the course that has a new grade to be added</param>
    private void AddNewGradeItem(int rowIndex, string newGrade)
    {
        DataGridViewComboBoxCell gradeComboBox = (DataGridViewComboBoxCell)courseTable.Rows[rowIndex].Cells["Grade"];
        gradeComboBox.Items.Add(newGrade);
    }

    public void UpdateCardHeight(int maxVisibleCourses)
    {
        // Course table
        int headerHeight = courseTable.ThemeStyle.HeaderStyle.Height;
        int rowHeight = courseTable.ThemeStyle.RowsStyle.Height;
        int courseTableHeight = (Math.Min(maxVisibleCourses, courseTable.RowCount) * rowHeight) + headerHeight;
        courseTable.Height = courseTableHeight;

        // Calculations Table
        int calculationsTableHeight = calculationsTable.ThemeStyle.HeaderStyle.Height + 2 * calculationsTable.ThemeStyle.RowsStyle.Height;

        // Total Margins
        int totalMargin = courseTable.Margin.Top + courseTable.Margin.Bottom
            + semesterCardSeparator.Margin.Top + semesterCardSeparator.Margin.Bottom
            + addCourseBtn.Margin.Top + addCourseBtn.Margin.Bottom
            + calculationsTable.Margin.Top + calculationsTable.Margin.Bottom
            + semesterCardTableLayoutPanel.Margin.Bottom;

        // Total card
        int cardHeight = Convert.ToInt16(semesterCardTableLayoutPanel.Location.Y) + courseTableHeight + calculationsTableHeight + semesterCardSeparator.Height + totalMargin;

        // Add Course Button
        if (allowAdding)
            cardHeight += addCourseBtn.Height;

        this.Height = cardHeight;
    }
    public void Fill(BindingList<Semester> semesters, int index)
    {
        this.semesterTitle.Text = semesters[index].Name;
        //this.courseTable.Rows.Clear();
        //this.courseTable.Refresh();
        
        List<Course> courseList = semesters[index].Courses.ToList();
        for (int i =0; i < courseList.Count; i++)
        {
            // Fill course table
            var rowIndex = courseTable.Rows.Add();
            courseTable.Rows[rowIndex].Cells["Course"].Value = courseList[i].Code;
            courseTable.Rows[rowIndex].Cells["Title"].Value = courseList[i].Title;
            courseTable.Rows[rowIndex].Cells["Subtype"].Value = courseList[i].Subtype.ToString();

            if (!letterGrades.Contains(courseList[i].Grade))
            {
                AddNewGradeItem(i,courseList[i].Grade);
            }
            courseTable.Rows[rowIndex].Cells["Grade"].Value = courseList[i].Grade;

            courseTable.Rows[rowIndex].Cells["Credits"].Value = courseList[i].Credits.ToString();
            courseTable.Rows[rowIndex].Cells["QualityPoints"].Value = courseList[i].CalculateQualityPoints(semesters[index].Year, semesters[index].Title.ToString()).ToString("0.00");
        }
        UpdateGPACalculationsTable(semesters, index);
        //updateCardHeight(maxVisibleCourses);          //  already called in the load event handler
    }
    public void AddNewCourse(BindingList<Semester> semesters, int semesterIndex)
    {
        Course course = semesters[semesterIndex].Courses[semesters[semesterIndex].Courses.Count - 1];

        var rowIndex = courseTable.Rows.Add();
        courseTable.Rows[rowIndex].Cells["Course"].Value = course.Code;
        courseTable.Rows[rowIndex].Cells["Title"].Value = course.Title;
        courseTable.Rows[rowIndex].Cells["Subtype"].Value = course.Subtype.ToString();
        courseTable.Rows[rowIndex].Cells["Grade"].Value = course.Grade;
        courseTable.Rows[rowIndex].Cells["Credits"].Value = course.Credits.ToString();
        courseTable.Rows[rowIndex].Cells["QualityPoints"].Value = course.CalculateQualityPoints(semesters[semesterIndex].Year, semesters[semesterIndex].Title.ToString()).ToString("0.00");

        UpdateGPACalculationsTable(semesters, semesterIndex);
        UpdateCardHeight(maxVisibleCourses);
    }
    public void UpdateCourseTableQualityPoints(Semester semester, int courseIndex)
    {
        double newQualityPoints = semester.Courses[courseIndex].QualityPoints;
        courseTable.Rows[courseIndex].Cells["QualityPoints"].Value = newQualityPoints.ToString("0.00");
        courseTable.Refresh();
    }
    public void UpdateGPACalculationsTable(BindingList<Semester> semesters, int index)
    {
        this.calculationsTable.Rows.Clear();
        this.calculationsTable.Refresh();

        string[] tremRow = new string[] { "Term" };
        string[] overallRow = new string[] { "Overall" };

        calculationsTable.Rows.Add(tremRow);
        calculationsTable.Rows.Add(overallRow);
        // GPA Calculations
        calculationsTable.Rows[0].Cells["GPACredits"].Value = semesters[index].CalculateGPACredits().ToString();
        calculationsTable.Rows[1].Cells["GPACredits"].Value = CalculateOverallGPACredits(semesters, index).ToString();
        calculationsTable.Rows[0].Cells["AttemptedCredits"].Value = semesters[index].CalculateAttemptedCredits().ToString();
        calculationsTable.Rows[1].Cells["AttemptedCredits"].Value = CalculateOverallAttemptedCredits(semesters, index).ToString();
        calculationsTable.Rows[0].Cells["EarnedCredits"].Value = semesters[index].CalculateEarnedCredits().ToString();
        calculationsTable.Rows[1].Cells["EarnedCredits"].Value = CalculateOverallEarnedCredits(semesters, index).ToString();
        calculationsTable.Rows[0].Cells["TotalCredits"].Value = semesters[index].CalculateTotalCredits().ToString();
        calculationsTable.Rows[1].Cells["TotalCredits"].Value = CalculateOverallTotalCredits(semesters, index).ToString();
        calculationsTable.Rows[0].Cells["TransferCredits"].Value = semesters[index].CalculateTransferCredits().ToString();
        calculationsTable.Rows[1].Cells["TransferCredits"].Value = CalculateOverallTransferCredits(semesters, index).ToString();
        calculationsTable.Rows[0].Cells["Quality_Points"].Value = semesters[index].CalculateQualityPoints().ToString("0.00");
        calculationsTable.Rows[1].Cells["Quality_Points"].Value = CalculateOverallQualityPoints(semesters, index).ToString("0.00");
        calculationsTable.Rows[0].Cells["GPA"].Value = semesters[index].CalculateGPA().ToString("0.0000");
        calculationsTable.Rows[1].Cells["GPA"].Value = CalculateOverallGPA(semesters, index).ToString("0.0000");
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
        using (AddCourseForm addCourseForm = new())
        {
            string CourseCode;
            string CourseTitle;
            CourseSubtype CourseSubtype;
            string CourseGrade;
            byte CourseCredits;

            DialogResult dialogResult = addCourseForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                CourseCode = addCourseForm.CourseCode;
                CourseTitle = addCourseForm.CourseTitle;
                CourseSubtype = addCourseForm.CourseSubtype;
                CourseGrade = addCourseForm.CourseGrade;
                CourseCredits = addCourseForm.CourseCredits;
					
                //string code, string title, CourseSubtype subtype, string grade, byte credits, int year /*= 2020*/, string semester /*= "Fall"*/, sbyte repeatedIn = -1, bool isRepeat = false

					newCourse = new Course(CourseCode, CourseTitle, CourseSubtype, CourseGrade, CourseCredits, int.MaxValue, null /*Always New Schema*/);

                CourseAdded?.Invoke(this, newCourse);
                courseTable.Invalidate();       // Invalidates the entire surface of the courses datagridview to be redrawn

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
            currentRowIndex = e.RowIndex;
            courseTableContextMenuStrip.Show(Cursor.Position);
        }
    }
    private void deleteCourseToolStripMenuItem_Click(object sender, EventArgs e)
    {
        courseTable.Rows.RemoveAt(currentRowIndex);
        UpdateCardHeight(maxVisibleCourses);
        CourseDeleted?.Invoke(this, currentRowIndex);
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