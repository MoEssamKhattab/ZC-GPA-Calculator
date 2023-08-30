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
        string studentName;
        string studentMajor;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            semesterCardList = new List<SemesterCard>();
        }
        private void browseFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Choose Your Transcript";
            //openFileDialog.InitialDirectory = @"";
            //openFileDialog.Filter = "Htm|*.htm";      //Select .pdf files only

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;     //files path                
                try
                {
                    semesterList = Controller.readHtmlTranscript(filePath, out studentName, out studentMajor);
                }
                catch (Exception ex)
                { Console.WriteLine(ex.Message); }

                if (semesterList != null && semesterList.Count != 0)
                {
                    initializeSemestersCards();
                    studentNameLabel.Text = studentName;
                    studentMajorLabel.Text = studentMajor;
                    studentPicture.Text = studentName[0].ToString();
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
                SemesterCard semesterCard = new SemesterCard();
                initializeSemesterCardEvents(semesterCard);

                semesterCard.fill(semesterList, i);
                
                semestersPanel.Controls.Add(semesterCard);

                semesterCardList.Add(semesterCard);
            }
        }
        private void addSemesterCard()
        {
            // TODO: to combine the above and below functions here
        }

        private void initializeSemesterCardEvents(SemesterCard semesterCard)
        {
            semesterCard.GradeChanged += (object sender, EventArgs e) =>
            {
                //int rowIndex = (e as DataGridViewCellEventArgs).RowIndex;

                DataGridViewCellEventArgs cell = (DataGridViewCellEventArgs)e;
                int rowIndex = cell.RowIndex;

                SemesterCard thisCard = (SemesterCard)sender;
                DataGridViewComboBoxCell gradeComboBox = (DataGridViewComboBoxCell)thisCard.CourseTable.Rows[rowIndex].Cells["Grade"];
                
                int semesterIndex = Controller.getSemesterIndex(thisCard, semesterCardList);
                Controller.updateSemestersList(ref this.semesterList, semesterIndex, rowIndex, gradeComboBox.Value.ToString());
                updateSemestersGPATables(this.semesterList, this.semesterCardList);
                semesterCard.updateCourseTableQualityPoints(semesterList[semesterIndex], rowIndex);
            };

            semesterCard.CourseAdded += (object sender, Course e) =>
            {
                SemesterCard thisCard = sender as SemesterCard;
                int semesterIndex = Controller.getSemesterIndex(thisCard, semesterCardList);
                semesterList[semesterIndex].Courses.Add(e);
                thisCard.fill(semesterList, semesterIndex);
                thisCard.updateCardHeight();
                semestersPanel.VerticalScroll.Value = semestersPanel.VerticalScroll.Maximum;
            };
        }
        private void addNewSemester(Semester semesterTitle, int year, List<semester> semesterList, List<SemesterCard> semesterCardList)
        {
            semester semester = new semester();
            semester.Title = semesterTitle;
            semester.Year = year;
            semesterList.Add(semester);

            SemesterCard semesterCard = new SemesterCard();
            semesterCard.AllowAdding= true;

            initializeSemesterCardEvents(semesterCard);

            semesterCard.SemesterTitle = $"{semesterTitle.ToString()}, {year}";
            semestersPanel.Controls.Add(semesterCard);
            semesterCardList.Add(semesterCard);
        }

        private void updateSemestersGPATables(List<semester> semesters, List<SemesterCard> semesterCards)
        {
            for (int i=0; i< semesterCards.Count; i++)
            {
                semesterCards[i].updateGPACalculationsTable(semesters, i);
            }
        }
        private void clearAllData()
        {
            //TODO: to clear all data
            semesterList.Clear();
            semestersPanel.Controls.Clear();
            semesterCardList.Clear();           
        }
        private void addNewSemesterBtn_Click(object sender, EventArgs e)
        {
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

        private void uploadAnotherDocBtn_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = tab1;
            clearAllData();
            this.browseFileBtn.PerformClick();
        }
    }
}