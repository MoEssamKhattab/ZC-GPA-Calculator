using System.ComponentModel;

namespace ZC_GPA_Calculator
{
    public partial class Form1 : Form
    {
        BindingList<semester> semesterList;
        List<SemesterCard> semesterCardList;
        string studentName;
        string studentMajor;
        public Form1()
        {
            InitializeComponent();
            semestersComboBox.DisplayMember = "Name";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void browseFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Choose Your Transcript";
            //openFileDialog.InitialDirectory = @"";
            //openFileDialog.Filter = "Htm|*.htm";      //Select .Htm files only

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;     //files path                
                try
                {
                    semesterList = Controller.readHtmlTranscript(filePath, out studentName, out studentMajor);
                    semesterCard_CgpaUpdate(null, EventArgs.Empty);     // Called once after loading all the semester cards, rather than after loading each card or course

                    if (semesterList != null && semesterList.Count != 0)
                    {
                        initializeSemestersCards();
                        studentNameLabel.Text = studentName;
                        studentMajorLabel.Text = studentMajor;
                        studentPicture.Text = studentName[0].ToString();
                        tabs.SelectTab(tab2);
                    }
                    else
                        MessageBox.Show("Open a valid transcript file, please!");
                }
                catch
                {
                    MessageBox.Show("Open a valid HTML file, please!");
                }
            }
        }
        public void initializeSemestersCards()
        {
            semesterCardList = new List<SemesterCard>();
            for (int i=0;  i<semesterList.Count; i++)
            {
                SemesterCard semesterCard = new SemesterCard();
                initializeSemesterCardEvents(semesterCard);
                semestersComboBox.DataSource = semesterList;
                semesterCard.fill(semesterList, i);
                semestersPanel.Controls.Add(semesterCard);
                semesterCardList.Add(semesterCard);
            }
        }
        private void addNewSemester(Semester semesterTitle, int year, BindingList<semester> semesterList, List<SemesterCard> semesterCardList)
        {
            semester semester = new semester();
            semester.Title = semesterTitle;
            semester.Year = year;
            semesterList.Add(semester);

            SemesterCard semesterCard = new SemesterCard();
            semesterCard.AllowAdding = true;

            initializeSemesterCardEvents(semesterCard);

            semesterCard.SemesterTitle = $"{semesterTitle.ToString()}, {year}";
            semestersPanel.Controls.Add(semesterCard);
            semesterCardList.Add(semesterCard);
        }
        private void initializeSemesterCardEvents(SemesterCard semesterCard)
        {
            semesterCard.GradeChanged += (object sender, EventArgs e) =>
            {
                //int rowIndex = (e as DataGridViewCellEventArgs).RowIndex
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
                //thisCard.fill(semesterList, semesterIndex);     // TODO: to create a separate function that handles adding a new course in the semester card
                thisCard.addNewCourse(semesterList, semesterIndex);
                semestersPanel.VerticalScroll.Value = semestersPanel.VerticalScroll.Maximum;
            };
            semesterCard.CourseDeleted += (object sender, int courseIndex) =>
            {
                SemesterCard thisCard = sender as SemesterCard;
                int semesterIndex = Controller.getSemesterIndex(thisCard, semesterCardList);
                semesterList[semesterIndex].Courses.RemoveAt(courseIndex);
                updateSemestersGPATables(this.semesterList, this.semesterCardList);
                semesterCard_CgpaUpdate(null, EventArgs.Empty);
            };
            semesterCard.CgpaUpdate += new System.EventHandler(this.semesterCard_CgpaUpdate);
        }        
        private void semesterCard_CgpaUpdate(object sender, EventArgs e)
        {
            double cGPA = Math.Round(semesterList[semesterList.Count - 1].calculateOverallGPA(semesterList, semesterList.Count - 1), 2);
            cgpaLabel.Text = $"CGPA: {cGPA}";
        }     
        private void updateSemestersGPATables(BindingList<semester> semesters, List<SemesterCard> semesterCards)
        {
            for (int i=0; i< semesterCards.Count; i++)
            {
                semesterCards[i].updateGPACalculationsTable(semesters, i);
            }
        }
        private void clearAllData()
        {
            // Clear all data
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
        private void semestersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Point targetPosition = new Point(0, semesterCardList[semestersComboBox.SelectedIndex].Top - semesterCardList[semestersComboBox.SelectedIndex].Margin.Top - semestersPanel.AutoScrollPosition.Y);
            semestersPanel.AutoScrollPosition = targetPosition;
        }
    }
}