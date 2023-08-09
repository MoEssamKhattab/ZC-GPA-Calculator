using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using Org.BouncyCastle.Crypto;
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
        public SemesterCard()
        {
            InitializeComponent();
        }

        private void SemesterCard_Load(object sender, EventArgs e)
        {
            string[] letterGrades = new string[] { "A", "A-", "B+", "B", "B-", "C+", "C", "C-", "F", "P" };
            this.grade.DataSource = letterGrades;

            courseList.Rows.Clear();
            courseList.Refresh();
            string[] row = new string[] { "CIE 205", "Data Structures and Algorithm Analysis", "LECTURE" };
            courseList.Rows.Add(row);
            courseList.Rows.Add(row);
            courseList.Rows.Add(row);
            courseList.Rows.Add(row);
            courseList.Rows.Add(row);
            courseList.Rows.Add(row);
            courseList.Rows.Add(row);


            string[] tremRow = new string[] {"Term" };
            string[] overallRow = new string[] {"Overall" };

            calculationsTable.Rows.Add(tremRow);
            calculationsTable.Rows.Add(overallRow);

            setCardHeight(7,4);
        }

        //==================

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

        public void fill(semester semester)
        {
            this.semesterTitle.Text = $"{semester.Title.ToString()}, {semester.Year}";
            this.courseList.Rows.Clear();
            this.courseList.Refresh();

            foreach (var course in semester.Courses)
            {
                var rowIndex = courseList.Rows.Add();
                courseList.Rows[rowIndex].Cells["Course"].Value = course.Code;
                courseList.Rows[rowIndex].Cells["Title"].Value = course.Title;
                courseList.Rows[rowIndex].Cells["Subtype"].Value = course.SubType.ToString();
                courseList.Rows[rowIndex].Cells["Grade"].Value = course.Grade;
                courseList.Rows[rowIndex].Cells["Credits"].Value = course.Credits.ToString();
                courseList.Rows[rowIndex].Cells["QualityPoints"].Value = String.Format("{0:0.00}", course.calculateQualityPoints().ToString());
            }
        }
    }
}
