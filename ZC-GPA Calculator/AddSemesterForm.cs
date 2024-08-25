using System.Windows.Forms;

namespace ZC_GPA_Calculator;

public partial class AddSemesterForm : Form
{
    public SemesterType SemesterTitle {get; set;}
    public int SemesterYear { get; set;}
    public AddSemesterForm()
    {
        InitializeComponent();
        this.MaximizeBox= false;
        this.MinimizeBox= false;

        this.yearDatePicker.Format = DateTimePickerFormat.Custom;
        this.yearDatePicker.CustomFormat = "yyyy";
        this.yearDatePicker.ShowUpDown = true;
    }
    private void AddSemesterForm_Load(object sender, EventArgs e)
    {    
        this.semesterComboBox.DataSource = new List<string> { "Fall", "Spring", "Summer" } /*Enum.GetNames(typeof(SemesterType))*/;
        this.semesterComboBox.SelectedIndex= 0;
    }
    private void addSemesterBtn_Click(object sender, EventArgs e)
    {
        SemesterTitle = (SemesterType)Enum.Parse(typeof(SemesterType), this.semesterComboBox.Text);
        SemesterYear = this.yearDatePicker.Value.Year;
        this.DialogResult = DialogResult.OK;
    }
}