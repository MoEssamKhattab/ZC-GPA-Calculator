using Guna.UI2.AnimatorNS;
using System.Windows.Forms;

namespace ZC_GPA_Calculator
{
    public partial class Form1 : Form
    {
        Controller controller;
        List<semester> semesterList;

        static string studentName;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            controller = new Controller();
        }
        private void browseFileBtn_Click(object sender, EventArgs e)
        {
            tabs.SelectTab(tab2);
            
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Choose Your Transcript";
            //openFileDialog.InitialDirectory = @"E:\CIE\Y3\Fall\Probability\Projects\2\StatisText";
            openFileDialog.Filter = "PDF|*.pdf";      //Select .pdf files only

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;     //files path

                semesterList = Controller.readTranscript(filePath, out studentName);

                semesterCard1.fill(semesterList[0]);

                //if (this.t.ColumnCount == 0 || this.X.ColumnCount == 0)
                //{
                //    //Switch to SimulationMode    VIEW 0
                //    tabControl1.SelectTab(tabPage1);
                //    MessageBox.Show("File is empty.\nPlease enter a file with valid data.");
                //}
                //else
                //{
                //    //start ineractiveMode
                //    tabControl1.SelectTab(tabPage2);
                //    ACF = new double[X.RowCount, X.ColumnCount];
                //}
            }
        }


    }
}