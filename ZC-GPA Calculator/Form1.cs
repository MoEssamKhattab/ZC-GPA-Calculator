using System.ComponentModel;
using System.Diagnostics;

namespace ZC_GPA_Calculator
{
    public partial class MainForm : Form
    {
        BindingList<semester> semesterList;
        List<SemesterCard> semesterCardList;

        string studentName;
        string studentMajor;
        public MainForm()
        {
            InitializeComponent();
            semestersComboBox.DisplayMember = "Name";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(1024, 768);
            this.MaximumSize = new Size(1920, 1080);
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
        }
        private void browseFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Choose Your Transcript";
            //openFileDialog.InitialDirectory = @"";
            //openFileDialog.Filter = "Htm|*.htm";      //Select .Htm files only

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                readInputFile(filePath);
            }
        }
        public void readInputFile(string filePath)
        {
            try
            {
                semesterList = Utilities.readHtmlTranscript(filePath, out studentName, out studentMajor);
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
                {
                    MessageBox.Show("Open a valid transcript file, please!");
                }
            }
            catch (NullReferenceException nullRefEx)
            {
                MessageBox.Show("Open a valid HTML file, please!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

                semesterCard.Margin = new Padding(3, 0, 3, 20);
                semesterCard.Dock= DockStyle.Fill;
                
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

            semesterCard.SemesterTitle = $"{semesterTitle} {year}";

            semesterCard.Margin = new Padding(3, 0, 3, 20);
            semesterCard.Dock = DockStyle.Fill;
            
            semestersPanel.Controls.Add(semesterCard);
            semesterCardList.Add(semesterCard);
        }
        private void initializeSemesterCardEvents(SemesterCard semesterCard)
        {
            semesterCard.GradeChanged += (object sender, EventArgs e) =>
            {
                //int rowIndex = (e as DataGridViewCellEventArgs).RowIndex
                DataGridViewCellEventArgs cell = (DataGridViewCellEventArgs)e;
                int rowIndex = cell.RowIndex;       // Course index

                SemesterCard thisCard = (SemesterCard)sender;
                int semesterIndex = Utilities.getSemesterIndex(thisCard, semesterCardList);

                if (semesterList[semesterIndex].Courses[rowIndex].RepeatedIn != -1)
                {
                    MessageBox.Show("This is an already repeated course, try to change the grade of the recently repeated version");
                }
                else if (semesterList[semesterIndex].Courses[rowIndex].GpaCredits == 0 && !semesterCardList[semesterIndex].AllowAdding)
                {
                    MessageBox.Show("This course has 0 GPA Credits; changing its grade will not affect your GPA at all!");
                }
                else
                {                
                    DataGridViewComboBoxCell gradeComboBox = (DataGridViewComboBoxCell)thisCard.CourseTable.Rows[rowIndex].Cells["Grade"];
                    Utilities.updateSemestersList(ref this.semesterList, semesterIndex, rowIndex, gradeComboBox.Value.ToString());
                    updateSemestersGPATables(this.semesterList, this.semesterCardList);
                    semesterCard.updateCourseTableQualityPoints(semesterList[semesterIndex], rowIndex);
                }
            };
            semesterCard.CourseAdded += (object sender, Course e) =>
            {
                SemesterCard thisCard = sender as SemesterCard;
                int semesterIndex = Utilities.getSemesterIndex(thisCard, semesterCardList);
                semesterList[semesterIndex].Courses.Add(e);
                thisCard.addNewCourse(semesterList, semesterIndex);
                semestersPanel.VerticalScroll.Value = semestersPanel.VerticalScroll.Maximum;
            };
            semesterCard.CourseDeleted += (object sender, int courseIndex) =>
            {
                SemesterCard thisCard = sender as SemesterCard;
                int semesterIndex = Utilities.getSemesterIndex(thisCard, semesterCardList);
                semesterList[semesterIndex].Courses.RemoveAt(courseIndex);
                updateSemestersGPATables(this.semesterList, this.semesterCardList);
                semesterCard_CgpaUpdate(null, EventArgs.Empty);
            };
            semesterCard.CgpaUpdate += new System.EventHandler(this.semesterCard_CgpaUpdate);
        }
        private void semesterCard_CgpaUpdate(object sender, EventArgs e)
        {
            double cGPA = semester.calculateOverallGPA(semesterList, semesterList.Count - 1);
            cgpaLabel.Text = $"CGPA: {cGPA.ToString("0.0000")}";

            double specialGPA = Utilities.calculateSpecialGPA(semesterList);
            if (Double.IsNaN(specialGPA))
            {
                specialGPATxt.Visible = false;
                gpaSeparator.Visible = false;
            }
            else
            {
                specialGPATxt.Text = $"Special GPA: {specialGPA.ToString("0.0000")}";
                specialGPATxt.Visible = true;
                gpaSeparator.Visible = true;
            }
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
                    semestersPanel.VerticalScroll.Value = semestersPanel.VerticalScroll.Maximum;    //Scroll to the bottom to view the added semester card
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

        private void dragFilePanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
        private void dragFilePanel_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string filePath = files[0];
            readInputFile(filePath);
        }
        private void sourceCodeBtn_Click(object sender, EventArgs e)
        {
            string url = "https://github.com/MoEssamKhattab/ZC-GPA-Calculator";
            try
            {
                Process.Start(new ProcessStartInfo() { FileName = url, UseShellExecute = true });
            }
            catch (System.ComponentModel.Win32Exception noBrowserExeption)
            {
                if (noBrowserExeption.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowserExeption.Message);
            }
            catch (System.Exception otherExeption)
            {
                MessageBox.Show(otherExeption.Message);
            }
        }
    }
}