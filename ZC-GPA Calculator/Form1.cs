using Guna.UI2.AnimatorNS;
using System.Windows.Forms;

namespace ZC_GPA_Calculator
{
    public partial class Form1 : Form
    {
        Controller controller;
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
            controller = new Controller();
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
                    addSemestersCards();
                    tabs.SelectTab(tab2);
                }
                else
                    MessageBox.Show("Enter a valid ");
            }
        }
        public void addSemestersCards()
        {
            for (int i=0;  i<semesterList.Count; i++)
            {
                SemesterCard card = new SemesterCard();
                card.fill(semesterList, i);
                card.Parent= this.semestersPanel;
                int x = (semestersPanel.Width - card.Width) / 2;
                int y = i * (card.Height + 10);
                card.Location = new System.Drawing.Point(x, y);
            }
        }
    }
}