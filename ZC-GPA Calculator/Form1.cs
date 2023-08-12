using Guna.UI2.AnimatorNS;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using iTextSharp.text;

namespace ZC_GPA_Calculator
{
    public partial class Form1 : Form
    {
        List<semester> semesterList;
        List<SemesterCard> semesterCardList;

        static string studentName;
        static string studentMajor;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            semesterCardList= new List<SemesterCard>();
        }
        private void browseFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Choose Your Transcript";
            //openFileDialog.InitialDirectory = @"E:\CIE\Y3\Fall\Probability\Projects\2\StatisText";
            openFileDialog.Filter = "PDF|*.pdf";      //Select .pdf files only

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;     //files path
                
                try
                {
                    semesterList = Controller.readTranscript(filePath, out studentName);
                }
                catch (Exception ex)
                { Console.WriteLine(ex.Message); }

                if (semesterList.Count != 0)
                {
                    initializeSemestersCards();
                    studentNameLabel.Text = studentName;
                    tabs.SelectTab(tab2);
                }
                else
                    MessageBox.Show("Enter a valid transcript document!");
            }
        }
        public void initializeSemestersCards()
        {
            //clearAllData(****)

            for (int i=0;  i<semesterList.Count; i++)
            {
                SemesterCard card = new SemesterCard();

                card.GradeChanged += (object sender, EventArgs e) =>
                {
                    //int rowIndex = (e as DataGridViewCellEventArgs).RowIndex;

                    DataGridViewCellEventArgs cell = (DataGridViewCellEventArgs)e;
                    int rowIndex = cell.RowIndex;

                    SemesterCard thisCard = (SemesterCard)sender;
                    DataGridViewComboBoxCell gradeComboBox = (DataGridViewComboBoxCell)thisCard.CourseList.Rows[rowIndex].Cells["Grade"];

                    Controller.updateSemestersList(ref this.semesterList, thisCard.SemsterIndexInSemestersList, rowIndex, gradeComboBox.Value.ToString());
                    updateSemestersGPATables(this.semesterList, this.semesterCardList);
                    card.updateQualityPointsOfCourseTable(semesterList[thisCard.SemsterIndexInSemestersList], rowIndex);
                };

                card.fill(semesterList, i);
                card.Parent= this.semestersPanel;
                int x = (semestersPanel.Width - card.Width) / 2;
                int y = 0;
                if (i!=0)
                    y = semesterCardList[i-1].Location.Y + semesterCardList[i - 1].Height + 25;
                
                card.Location = new System.Drawing.Point(x, y);

                semesterCardList.Add(card);
            }
        }
        private static void addSemesterCard()
        {
            // TODO: to combine the above and below functions here
        }
        private void addNewSemester(Semester semesterTitle, int year, List<semester> semesterList, List<SemesterCard> semesterCardList)
        {
            semester semester = new semester();
            semester.Title = semesterTitle;
            semester.Year = year;
            semesterList.Add(semester);

            SemesterCard semesterCard = new SemesterCard();
            
            // ====>> redundant code to be optimized
            semesterCard.GradeChanged += (object sender, EventArgs e) =>
            {
                //int rowIndex = (e as DataGridViewCellEventArgs).RowIndex;

                DataGridViewCellEventArgs cell = (DataGridViewCellEventArgs)e;
                int rowIndex = cell.RowIndex;

                SemesterCard thisCard = (SemesterCard)sender;
                DataGridViewComboBoxCell gradeComboBox = (DataGridViewComboBoxCell)thisCard.CourseList.Rows[rowIndex].Cells["Grade"];

                Controller.updateSemestersList(ref this.semesterList, thisCard.SemsterIndexInSemestersList, rowIndex, gradeComboBox.Value.ToString());
                updateSemestersGPATables(this.semesterList, this.semesterCardList);
                semesterCard.updateQualityPointsOfCourseTable(semesterList[thisCard.SemsterIndexInSemestersList], rowIndex);
            };

            semesterCard.SemesterTitle = $"{semesterTitle.ToString()}, {year}";
            semesterCard.Parent = this.semestersPanel;
            int x = (semestersPanel.Width - semesterCard.Width) / 2;
            int y = semesterCardList[semesterCardList.Count - 1].Location.Y + semesterCardList[semesterCardList.Count - 1].Height + 25;
            semesterCard.Location = new System.Drawing.Point(x, y);
            semesterCardList.Add(semesterCard);
        }
        private static void updateSemestersGPATables(List<semester> semesters, List<SemesterCard> semesterCards)
        {
            for (int i=0; i< semesterCards.Count; i++)
            {
                semesterCards[i].updateGPACalculationsTable(semesters, i);
            }
        }

        private static void clearAllData(List<semester> semesters, List<SemesterCard> semesterCards )
        {
            //TODO: to clear all data  
        }

        private void addNewSemesterBtn_Click(object sender, EventArgs e)
        {
            //AddSemesterForm addSemesterForm = new AddSemesterForm();
            //addSemesterForm.ShowDialog();
            using (AddSemesterForm addSemesterForm = new AddSemesterForm())
            {
                Semester semesterTitle;
                int year;

                DialogResult dialogResult = addSemesterForm.ShowDialog();               
                if (dialogResult == DialogResult.OK)
                {
                    semesterTitle = addSemesterForm.SemesterTitle;
                    year = addSemesterForm.SemesterYear;

                    addNewSemester(semesterTitle, year, this.semesterList, this.semesterCardList);

                    semestersPanel.VerticalScroll.Value = semestersPanel.VerticalScroll.Maximum;    //Scrol to the bottom to view the added semester card
                }
            }
        }
    }
}