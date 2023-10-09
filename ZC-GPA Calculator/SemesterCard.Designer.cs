using Guna.UI2.WinForms;
using System.Windows.Forms;

namespace ZC_GPA_Calculator
{
    partial class SemesterCard
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            this.semesterCardPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.semesterCardTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.addCourseBtn = new Guna.UI2.WinForms.Guna2Button();
            this.courseTable = new Guna.UI2.WinForms.Guna2DataGridView();
            this.course = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grade = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.credits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qualityPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.semesterCardSeparator = new Guna.UI2.WinForms.Guna2Separator();
            this.calculationsTable = new Guna.UI2.WinForms.Guna2DataGridView();
            this.header = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.attemptedCredits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.earnedCredits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalCredits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GPACredits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transferCredits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quality_Points = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GPA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cardHeaderPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.semesterTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.courseTableContextMenuStrip = new Guna.UI2.WinForms.Guna2ContextMenuStrip();
            this.deleteCourseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.semesterCardPanel.SuspendLayout();
            this.semesterCardTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.courseTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calculationsTable)).BeginInit();
            this.cardHeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.courseTableContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // semesterCardPanel
            // 
            this.semesterCardPanel.BackColor = System.Drawing.Color.Transparent;
            this.semesterCardPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(31)))), ((int)(((byte)(35)))));
            this.semesterCardPanel.BorderRadius = 20;
            this.semesterCardPanel.BorderThickness = 2;
            this.semesterCardPanel.Controls.Add(this.semesterCardTableLayoutPanel);
            this.semesterCardPanel.Controls.Add(this.cardHeaderPanel);
            this.semesterCardPanel.CustomizableEdges = customizableEdges7;
            this.semesterCardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.semesterCardPanel.FillColor = System.Drawing.Color.White;
            this.semesterCardPanel.Location = new System.Drawing.Point(0, 0);
            this.semesterCardPanel.Name = "semesterCardPanel";
            this.semesterCardPanel.ShadowDecoration.CustomizableEdges = customizableEdges8;
            this.semesterCardPanel.Size = new System.Drawing.Size(1700, 760);
            this.semesterCardPanel.TabIndex = 0;
            // 
            // semesterCardTableLayoutPanel
            // 
            this.semesterCardTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.semesterCardTableLayoutPanel.ColumnCount = 1;
            this.semesterCardTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.semesterCardTableLayoutPanel.Controls.Add(this.addCourseBtn, 0, 1);
            this.semesterCardTableLayoutPanel.Controls.Add(this.courseTable, 0, 0);
            this.semesterCardTableLayoutPanel.Controls.Add(this.semesterCardSeparator, 0, 2);
            this.semesterCardTableLayoutPanel.Controls.Add(this.calculationsTable, 0, 3);
            this.semesterCardTableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.semesterCardTableLayoutPanel.Location = new System.Drawing.Point(20, 53);
            this.semesterCardTableLayoutPanel.Margin = new System.Windows.Forms.Padding(10);
            this.semesterCardTableLayoutPanel.Name = "semesterCardTableLayoutPanel";
            this.semesterCardTableLayoutPanel.RowCount = 4;
            this.semesterCardTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.semesterCardTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.semesterCardTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.semesterCardTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.semesterCardTableLayoutPanel.Size = new System.Drawing.Size(1660, 680);
            this.semesterCardTableLayoutPanel.TabIndex = 11;
            // 
            // addCourseBtn
            // 
            this.addCourseBtn.Animated = true;
            this.addCourseBtn.BackColor = System.Drawing.Color.Transparent;
            this.addCourseBtn.BorderColor = System.Drawing.Color.DimGray;
            this.addCourseBtn.BorderRadius = 8;
            this.addCourseBtn.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.addCourseBtn.BorderThickness = 1;
            this.addCourseBtn.CustomizableEdges = customizableEdges1;
            this.addCourseBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.addCourseBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.addCourseBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.addCourseBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.addCourseBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addCourseBtn.FillColor = System.Drawing.Color.White;
            this.addCourseBtn.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.addCourseBtn.ForeColor = System.Drawing.Color.DimGray;
            this.addCourseBtn.Location = new System.Drawing.Point(3, 369);
            this.addCourseBtn.Name = "addCourseBtn";
            this.addCourseBtn.ShadowDecoration.CustomizableEdges = customizableEdges2;
            this.addCourseBtn.Size = new System.Drawing.Size(1654, 45);
            this.addCourseBtn.TabIndex = 10;
            this.addCourseBtn.Text = "Add Course";
            this.addCourseBtn.Visible = false;
            this.addCourseBtn.Click += new System.EventHandler(this.addCourseBtn_Click);
            // 
            // courseTable
            // 
            this.courseTable.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.courseTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.courseTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.courseTable.ColumnHeadersHeight = 60;
            this.courseTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.courseTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.course,
            this.title,
            this.subtype,
            this.grade,
            this.credits,
            this.qualityPoints});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.courseTable.DefaultCellStyle = dataGridViewCellStyle3;
            this.courseTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.courseTable.GridColor = System.Drawing.Color.White;
            this.courseTable.Location = new System.Drawing.Point(3, 3);
            this.courseTable.Name = "courseTable";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.courseTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.courseTable.RowHeadersVisible = false;
            this.courseTable.RowHeadersWidth = 50;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.courseTable.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.courseTable.RowTemplate.Height = 50;
            this.courseTable.Size = new System.Drawing.Size(1654, 360);
            this.courseTable.TabIndex = 1;
            this.courseTable.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.courseTable.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.courseTable.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.courseTable.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.courseTable.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.courseTable.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.courseTable.ThemeStyle.GridColor = System.Drawing.Color.White;
            this.courseTable.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.courseTable.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.courseTable.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.courseTable.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.courseTable.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.courseTable.ThemeStyle.HeaderStyle.Height = 60;
            this.courseTable.ThemeStyle.ReadOnly = false;
            this.courseTable.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.courseTable.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.courseTable.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.courseTable.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.courseTable.ThemeStyle.RowsStyle.Height = 50;
            this.courseTable.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.courseTable.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.courseTable.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.courseTable_CellMouseUp);
            this.courseTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.courseTable_CellValueChanged);
            this.courseTable.CurrentCellDirtyStateChanged += new System.EventHandler(this.courseTable_CurrentCellDirtyStateChanged);
            this.courseTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.courseTable_KeyDown);
            // 
            // course
            // 
            this.course.FillWeight = 64.17112F;
            this.course.HeaderText = "Course";
            this.course.MinimumWidth = 110;
            this.course.Name = "course";
            this.course.ReadOnly = true;
            this.course.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // title
            // 
            this.title.FillWeight = 248.3437F;
            this.title.HeaderText = "Title";
            this.title.MinimumWidth = 6;
            this.title.Name = "title";
            this.title.ReadOnly = true;
            // 
            // subtype
            // 
            this.subtype.FillWeight = 100.7273F;
            this.subtype.HeaderText = "Subtype";
            this.subtype.MinimumWidth = 6;
            this.subtype.Name = "subtype";
            this.subtype.ReadOnly = true;
            // 
            // grade
            // 
            this.grade.FillWeight = 34.25575F;
            this.grade.HeaderText = "Grade";
            this.grade.MinimumWidth = 80;
            this.grade.Name = "grade";
            this.grade.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // credits
            // 
            this.credits.FillWeight = 76.25104F;
            this.credits.HeaderText = "Credits";
            this.credits.MinimumWidth = 6;
            this.credits.Name = "credits";
            this.credits.ReadOnly = true;
            // 
            // qualityPoints
            // 
            this.qualityPoints.FillWeight = 76.25104F;
            this.qualityPoints.HeaderText = "Quality Points";
            this.qualityPoints.MinimumWidth = 6;
            this.qualityPoints.Name = "qualityPoints";
            this.qualityPoints.ReadOnly = true;
            // 
            // semesterCardSeparator
            // 
            this.semesterCardSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.semesterCardSeparator.Location = new System.Drawing.Point(3, 420);
            this.semesterCardSeparator.Name = "semesterCardSeparator";
            this.semesterCardSeparator.Size = new System.Drawing.Size(1654, 12);
            this.semesterCardSeparator.TabIndex = 3;
            // 
            // calculationsTable
            // 
            this.calculationsTable.AllowUserToAddRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            this.calculationsTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.calculationsTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.calculationsTable.ColumnHeadersHeight = 60;
            this.calculationsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.calculationsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.header,
            this.attemptedCredits,
            this.earnedCredits,
            this.totalCredits,
            this.GPACredits,
            this.transferCredits,
            this.Quality_Points,
            this.GPA});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.calculationsTable.DefaultCellStyle = dataGridViewCellStyle8;
            this.calculationsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calculationsTable.GridColor = System.Drawing.Color.White;
            this.calculationsTable.Location = new System.Drawing.Point(3, 438);
            this.calculationsTable.Name = "calculationsTable";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.calculationsTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.calculationsTable.RowHeadersVisible = false;
            this.calculationsTable.RowHeadersWidth = 50;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.calculationsTable.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.calculationsTable.RowTemplate.Height = 50;
            this.calculationsTable.Size = new System.Drawing.Size(1654, 239);
            this.calculationsTable.TabIndex = 2;
            this.calculationsTable.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.calculationsTable.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.calculationsTable.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.calculationsTable.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.calculationsTable.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.calculationsTable.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.calculationsTable.ThemeStyle.GridColor = System.Drawing.Color.White;
            this.calculationsTable.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.calculationsTable.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.calculationsTable.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.calculationsTable.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.calculationsTable.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.calculationsTable.ThemeStyle.HeaderStyle.Height = 60;
            this.calculationsTable.ThemeStyle.ReadOnly = false;
            this.calculationsTable.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.calculationsTable.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.calculationsTable.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.calculationsTable.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.calculationsTable.ThemeStyle.RowsStyle.Height = 50;
            this.calculationsTable.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.calculationsTable.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // header
            // 
            this.header.HeaderText = "";
            this.header.MinimumWidth = 6;
            this.header.Name = "header";
            this.header.ReadOnly = true;
            // 
            // attemptedCredits
            // 
            this.attemptedCredits.HeaderText = "Attempted Credits";
            this.attemptedCredits.MinimumWidth = 6;
            this.attemptedCredits.Name = "attemptedCredits";
            this.attemptedCredits.ReadOnly = true;
            // 
            // earnedCredits
            // 
            this.earnedCredits.HeaderText = "Earned Credits";
            this.earnedCredits.MinimumWidth = 6;
            this.earnedCredits.Name = "earnedCredits";
            this.earnedCredits.ReadOnly = true;
            // 
            // totalCredits
            // 
            this.totalCredits.HeaderText = "Total Credits";
            this.totalCredits.MinimumWidth = 6;
            this.totalCredits.Name = "totalCredits";
            this.totalCredits.ReadOnly = true;
            // 
            // GPACredits
            // 
            this.GPACredits.HeaderText = "GPA Credits";
            this.GPACredits.MinimumWidth = 6;
            this.GPACredits.Name = "GPACredits";
            this.GPACredits.ReadOnly = true;
            // 
            // transferCredits
            // 
            this.transferCredits.HeaderText = "Transfer Credits";
            this.transferCredits.MinimumWidth = 6;
            this.transferCredits.Name = "transferCredits";
            this.transferCredits.ReadOnly = true;
            // 
            // Quality_Points
            // 
            this.Quality_Points.HeaderText = "Quality Points";
            this.Quality_Points.MinimumWidth = 6;
            this.Quality_Points.Name = "Quality_Points";
            this.Quality_Points.ReadOnly = true;
            // 
            // GPA
            // 
            this.GPA.HeaderText = "GPA";
            this.GPA.MinimumWidth = 6;
            this.GPA.Name = "GPA";
            this.GPA.ReadOnly = true;
            // 
            // cardHeaderPanel
            // 
            this.cardHeaderPanel.BorderColor = System.Drawing.Color.Transparent;
            this.cardHeaderPanel.Controls.Add(this.guna2PictureBox1);
            this.cardHeaderPanel.Controls.Add(this.semesterTitle);
            this.cardHeaderPanel.CustomizableEdges = customizableEdges5;
            this.cardHeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.cardHeaderPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(31)))), ((int)(((byte)(35)))));
            this.cardHeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.cardHeaderPanel.Name = "cardHeaderPanel";
            this.cardHeaderPanel.ShadowDecoration.CustomizableEdges = customizableEdges6;
            this.cardHeaderPanel.Size = new System.Drawing.Size(1700, 40);
            this.cardHeaderPanel.TabIndex = 0;
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.CustomizableEdges = customizableEdges3;
            this.guna2PictureBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.Image = global::ZC_GPA_Calculator.Properties.Resources.calendar;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.InitialImage = null;
            this.guna2PictureBox1.Location = new System.Drawing.Point(20, 5);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            this.guna2PictureBox1.Size = new System.Drawing.Size(28, 30);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 1;
            this.guna2PictureBox1.TabStop = false;
            // 
            // semesterTitle
            // 
            this.semesterTitle.BackColor = System.Drawing.Color.Transparent;
            this.semesterTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.semesterTitle.ForeColor = System.Drawing.Color.White;
            this.semesterTitle.Location = new System.Drawing.Point(61, 5);
            this.semesterTitle.Name = "semesterTitle";
            this.semesterTitle.Size = new System.Drawing.Size(95, 30);
            this.semesterTitle.TabIndex = 1;
            this.semesterTitle.Text = "Fall, 2020";
            // 
            // courseTableContextMenuStrip
            // 
            this.courseTableContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.courseTableContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteCourseToolStripMenuItem});
            this.courseTableContextMenuStrip.Name = "courseTableContextMenuStrip";
            this.courseTableContextMenuStrip.RenderStyle.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.courseTableContextMenuStrip.RenderStyle.BorderColor = System.Drawing.Color.Gainsboro;
            this.courseTableContextMenuStrip.RenderStyle.ColorTable = null;
            this.courseTableContextMenuStrip.RenderStyle.RoundedEdges = true;
            this.courseTableContextMenuStrip.RenderStyle.SelectionArrowColor = System.Drawing.Color.White;
            this.courseTableContextMenuStrip.RenderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.courseTableContextMenuStrip.RenderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.courseTableContextMenuStrip.RenderStyle.SeparatorColor = System.Drawing.Color.Gainsboro;
            this.courseTableContextMenuStrip.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.courseTableContextMenuStrip.Size = new System.Drawing.Size(172, 28);
            // 
            // deleteCourseToolStripMenuItem
            // 
            this.deleteCourseToolStripMenuItem.Name = "deleteCourseToolStripMenuItem";
            this.deleteCourseToolStripMenuItem.Size = new System.Drawing.Size(171, 24);
            this.deleteCourseToolStripMenuItem.Text = "Delete Course";
            this.deleteCourseToolStripMenuItem.Click += new System.EventHandler(this.deleteCourseToolStripMenuItem_Click);
            // 
            // SemesterCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.semesterCardPanel);
            this.Name = "SemesterCard";
            this.Size = new System.Drawing.Size(1700, 760);
            this.Load += new System.EventHandler(this.SemesterCard_Load);
            this.semesterCardPanel.ResumeLayout(false);
            this.semesterCardTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.courseTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calculationsTable)).EndInit();
            this.cardHeaderPanel.ResumeLayout(false);
            this.cardHeaderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.courseTableContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel semesterCardPanel;
        private Guna.UI2.WinForms.Guna2Panel cardHeaderPanel;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2HtmlLabel semesterTitle;
        private Guna.UI2.WinForms.Guna2DataGridView calculationsTable;
        private Guna.UI2.WinForms.Guna2Separator semesterCardSeparator;
        private Guna2DataGridView courseTable;
        private Guna2Button addCourseBtn;
        private Guna2ContextMenuStrip courseTableContextMenuStrip;
        private ToolStripMenuItem deleteCourseToolStripMenuItem;
        private DataGridViewTextBoxColumn header;
        private DataGridViewTextBoxColumn attemptedCredits;
        private DataGridViewTextBoxColumn earnedCredits;
        private DataGridViewTextBoxColumn totalCredits;
        private DataGridViewTextBoxColumn GPACredits;
        private DataGridViewTextBoxColumn transferCredits;
        private DataGridViewTextBoxColumn Quality_Points;
        private DataGridViewTextBoxColumn GPA;
        private TableLayoutPanel semesterCardTableLayoutPanel;
        private DataGridViewTextBoxColumn course;
        private DataGridViewTextBoxColumn title;
        private DataGridViewTextBoxColumn subtype;
        private DataGridViewComboBoxColumn grade;
        private DataGridViewTextBoxColumn credits;
        private DataGridViewTextBoxColumn qualityPoints;

        public Guna2DataGridView CourseTable { get => courseTable; set => courseTable = value; }
    }
}
