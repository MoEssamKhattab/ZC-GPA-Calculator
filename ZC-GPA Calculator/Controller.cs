using System.Text;
using HtmlAgilityPack;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using HtmlAgilityPack;
using System.Net;
using System.ComponentModel;

namespace ZC_GPA_Calculator
{
    public enum CourseSubtype : byte { LECTURE, LAB };
    public enum Semester : byte { Fall = 0, Spring = 1, Summer = 2 };
    public static class Grades
    {
        public static readonly double A = 4.0;
        public static readonly double A_minus = 3.7;
        public static readonly double B_plus = 3.3;
        public static readonly double B = 3.0;
        public static readonly double B_minus = 2.7;
        public static readonly double C_plus = 2.3;
        public static readonly double C = 2.0;
        public static readonly double C_minus = 1.7;
        public static readonly double F = 0.0;
    }
    public struct Course
    {
        string code;
        string title;
        CourseSubtype subtype;
        string grade;
        int credits;
        int gpaCredits;
        double qualityPoints;
        bool repeated;      //to be assigned true for the new course
        public Course(string code, string title, CourseSubtype subtype, string grade, int credits, bool repeated = false)
        {
            this.Code = code;
            this.Title = title;
            this.SubType = subtype;
            this.Grade = grade;
            this.Credits = credits;
            this.Repeated= repeated;

            if (grade == "P" || grade == "I" || grade == "IP" || grade == "W" || grade == "WP" || grade == "WF" || grade == "TR")       // TR === transfer
                this.gpaCredits = 0;
            else 
                this.gpaCredits = credits;

            this.QualityPoints = calculateQualityPoints();
        }

        public string Code { get => code; set => code = value; }
        public string Title { get => title; set => title = value; }    
        public string Grade 
        { 
            get => grade;
            set 
            {
                grade = value;
                if (grade == "P" || grade == "I" || grade == "IP" || grade == "W" || grade == "WP" || grade == "WF" || grade == "TR")
                    gpaCredits = 0;
                else 
                    gpaCredits = credits;
            }
        }
        public int Credits { get => credits; set => credits = value; }
        internal CourseSubtype SubType { get => subtype; set => subtype = value; }
        public int GpaCredits { get => gpaCredits; set => gpaCredits = value; }
        public double QualityPoints { get => qualityPoints; set => qualityPoints = value; }
        public bool Repeated { get => repeated; set => repeated = value; }

        public double calculateQualityPoints()
        {      
            return Math.Round(Controller.stringToGrade(this.grade) * (double)this.credits, 2); 
        }
    }
    public struct semester
    {
        Semester title;
        int year;
        List<Course> courses;
        public semester()
        {
            this.title = new();
            this.year = 0;
            this.courses = new();
        }
        public string Name => $"{year} {title.ToString()}";
        public int Year { get => year; set => year = value; }
        internal List<Course> Courses { get => courses; set => courses = value; }
        internal Semester Title { get => title; set => title = value; }
        public double calculateQualityPoints()
        {
            double _qualityPoints = 0;
            foreach (var course in this.courses)
            {
                _qualityPoints += course.QualityPoints;
            }
            return _qualityPoints;
        }
        public double calculateOverallQualityPoints(BindingList<semester> semesters, int index)
        {
            double _qualityPoints = 0;

            for (int i = 0; i <= index; i++)
            {
                foreach (var course in semesters[i].Courses)
                {
                    if (!course.Repeated)
                        _qualityPoints += course.QualityPoints;
                }
            }
            return _qualityPoints;
        }
        public double calculateCredits()
        {
            double _credits = 0;
            foreach (var course in this.courses)
            {
                _credits += course.Credits;
            }
            return _credits;
        }
        public double calculateOverallCredits(BindingList<semester> semesters, int index)
        {
            double _overallCredits = 0;

            for (int i = 0; i <= index; i++)
            {
                foreach (var course in semesters[i].Courses)
                {
                    if (!course.Repeated)
                        _overallCredits += course.Credits;
                }
            }
            return _overallCredits;
        }
        public double calculateGPACredits()
        {
            double _GPACredits = 0;
            foreach (var course in this.courses)
            {
                _GPACredits += course.GpaCredits;
            }
            return _GPACredits;
        }
        public double calculateOverallGPACredits(BindingList<semester> semesters, int index)
        {
            double _overallGPACredits = 0;

            for (int i = 0; i <= index; i++)
            {
                foreach (var course in semesters[i].Courses)
                {
                    if (!course.Repeated)
                        _overallGPACredits += course.GpaCredits;
                }
            }
            return _overallGPACredits;
        }
        public double calculateGPA()
        {
            return Math.Round(calculateQualityPoints()/calculateGPACredits(),2);
        }
        public double calculateOverallGPA(BindingList<semester> semesters, int index)
        {
            return Math.Round(calculateOverallQualityPoints(semesters, index) / calculateOverallGPACredits(semesters, index),2);
        }
    }

    internal class Controller
    {
        public static BindingList<semester> readTranscript(string path, out string studentName, out string major)
        {
            BindingList<semester> semestersList = new();   //List of all semesters

            //read PDF document
            string allPdfText = ReadPDFfile(path);

            string[] allTextSeparator = { "12588", "Name" };
            string[] separatedText = allPdfText.Split(allTextSeparator, StringSplitOptions.RemoveEmptyEntries);

            studentName = separatedText[1].Trim(Environment.NewLine.ToCharArray());     //(or) *.Trim('\n', '\r');
            string allSemestersString = separatedText[2];

            string[] separatedSemesters = allSemestersString.Split("Term");

            string[] semestersKeywords = Enum.GetNames(typeof(Semester));
            string[] coursesKeywords = Enum.GetNames(typeof(CourseSubtype));

            // Extracting the major
            string[] majorTextSeparator = { "Undergraduate/Bachelor of Science/", "No Degree Awarded Yet" };
            string[] majorData = separatedSemesters[1].Split(majorTextSeparator, StringSplitOptions.RemoveEmptyEntries);
            major = majorData[1].Trim(Environment.NewLine.ToCharArray()).Replace("\n", "").Replace("\r", "");

            for (int i = 1; i < separatedSemesters.Length; i++)     //ignoring the first element since it's not a real semster data
            {
                semester _semester = new();

                using (StringReader stringReader = new StringReader(separatedSemesters[i]))
                {
                    string line;

                    string courseCode;
                    string courseTitle;
                    CourseSubtype courseSubType;
                    string courseGrade;
                    int courseCredits;

                    while ((line = stringReader.ReadLine()) != null)
                    {
                        if (semestersKeywords.Any(keyword => line.Contains(keyword)))
                        {
                            string[] semesterHeader = line.Split(' ');     //split the year, and semester title
                            _semester.Year = int.Parse(semesterHeader[0]);
                            _semester.Title = (Semester)Enum.Parse(typeof(Semester), semesterHeader[1]);
                        }
                        //Extracting course data
                        else if (coursesKeywords.Any(keyword => line.Contains(keyword)))
                        {
                            string[] courseStingArray = line.Split(' ');

                            courseCode = string.Join(" ", courseStingArray, 0, 2);
                            courseTitle = string.Join(" ", courseStingArray, 2, courseStingArray.Length - 6);
                            courseSubType = (CourseSubtype)Enum.Parse(typeof(CourseSubtype), courseStingArray[courseStingArray.Length - 4]);
                            courseGrade = courseStingArray[courseStingArray.Length - 3];

                            //Handling the case of any repeated courses
                            if (courseGrade.StartsWith('['))
                            {
                                courseGrade= courseGrade.Substring(1,courseGrade.Length-2);
                                changeRepeatedFlag(courseCode, semestersList);
                            }
                            courseCredits = Convert.ToInt32(Math.Floor(Convert.ToDouble(courseStingArray[courseStingArray.Length - 2])));

                            Course _course = new Course(courseCode, courseTitle, courseSubType, courseGrade, courseCredits);
                            _semester.Courses.Add(_course);
                        }
                    }
                }
                semestersList.Add(_semester);
            }
            return semestersList;
        }

        public static BindingList<semester> readHtmlTranscript(string filePath, out string studentName, out string major)
        {
            BindingList<semester> semestersList = new();   //List of all semesters

            // Load the HTML document using HTMLAgilityPack
            var document = new HtmlAgilityPack.HtmlDocument();
            document.Load(filePath);

            // Select Student Name
            HtmlNode studentNameNode = document.DocumentNode.SelectSingleNode("//*[@id=\"contentPage\"]/div[2]/div/div/div/div[4]/div/p[2]");
            studentName = WebUtility.HtmlDecode(studentNameNode.InnerText.Trim());
            
            // Select Major
            HtmlNode majorNode = document.DocumentNode.SelectSingleNode("//*[@id=\"tblProgramDegreeCurriculum\"]/tbody/tr/th/span");
            major = ((WebUtility.HtmlDecode(majorNode.InnerText.Trim())).Split("Undergraduate/Bachelor of Science/"))[1];

            // Find all table elements with the specific ID
            string coursesTableId = "tblOrganization";
            IEnumerable<HtmlNode> tables = document.DocumentNode.Descendants("table")
                .Where(table => table.GetAttributeValue("id", null) == coursesTableId);

            // Loop through each table's parent (each representing a single semester)
            foreach (HtmlNode table in tables)
            {
                semester semester = new semester();
                HtmlNode div = table.ParentNode.ParentNode;

                var h4Element = div.SelectSingleNode(".//h4");
                if (h4Element != null)
                {
                    string[] semesterHeader = (WebUtility.HtmlDecode(h4Element.InnerText.Trim())).Split(' ');
                    semester.Year = int.Parse(semesterHeader[0]);
                    semester.Title = (Semester)Enum.Parse(typeof(Semester), semesterHeader[1]);
                }

                HtmlNodeCollection rows = table.SelectNodes(".//tr");

                for (int i = 1; i < rows?.Count; i++)        //starting from index 1 to skip the header
                {
                    HtmlNode row = rows[i];
                    HtmlNodeCollection cells = row.SelectNodes(".//th|td");

                    if (cells != null && cells.Count > 0)
                    {
                        string courseCode = WebUtility.HtmlDecode(cells[0].InnerText.Trim());
                        string courseTitle = WebUtility.HtmlDecode(cells[1].InnerText.Trim());
                        CourseSubtype courseSubType = (CourseSubtype)Enum.Parse(typeof(CourseSubtype), WebUtility.HtmlDecode(cells[2].InnerText.Trim()));
                        string courseGrade = WebUtility.HtmlDecode(cells[3].InnerText.Trim());
                        int courseCredits = Convert.ToInt32(Math.Floor(Convert.ToDouble(cells[4].InnerText.Trim())));

                        //Handling the case of any repeated courses
                        if (courseGrade.StartsWith('['))
                        {
                            courseGrade = courseGrade.Substring(1, courseGrade.Length - 2);
                            changeRepeatedFlag(courseCode, semestersList);
                        }
                        Course course = new Course(courseCode, courseTitle, courseSubType, courseGrade, courseCredits);
                        semester.Courses.Add(course);
                    }
                }
                semestersList.Add(semester);
            }
            return semestersList;
        }

        static string ReadPDFfile(string path)
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                using (PdfReader reader = new PdfReader(path))
                {
                    for (int p = 1; p <= reader.NumberOfPages; p++)
                    {
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        string text = PdfTextExtractor.GetTextFromPage(reader, p, strategy);
                        text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));

                        stringBuilder.Append(text);
                    }
                }
                return stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static double stringToGrade(string grade)
        {
            switch (grade)
            {
                case "A":
                    return Grades.A;
                case "A-":
                    return Grades.A_minus;
                case "B+":
                    return Grades.B_plus;
                case "B":
                    return Grades.B;
                case "B-":
                    return Grades.B_minus;
                case "C+":
                    return Grades.C_plus;
                case "C":
                    return Grades.C;
                case "C-":
                    return Grades.C_minus;
                case "F":
                    return Grades.F;
                default:
                    return 0.0;
            }
        }

        public static void updateSemestersList(ref BindingList<semester> semesters, int semesterIndex, int courseIndex, string newGrade)
        {
            Course updatedCourse = semesters[semesterIndex].Courses[courseIndex];
            updatedCourse.Grade = newGrade;
            updatedCourse.QualityPoints = updatedCourse.calculateQualityPoints();

            semesters[semesterIndex].Courses[courseIndex] = updatedCourse;
        }

        #region Testing modules
        public static void printTranscript(List<semester> semestersList)
        {
            foreach (var semester in semestersList)
            {
                Console.WriteLine($"{semester.Year}     {semester.Title}");
                foreach (var course in semester.Courses)
                {
                    Console.WriteLine($"{course.Code}   {course.Title}      {course.Grade}  {course.Credits}");
                }
                Console.WriteLine("============================================");
            }
        }

        #endregion

        public static int getSemesterIndex(SemesterCard semesterCard, List<SemesterCard> semesterCardList)
        {
            return semesterCardList.IndexOf(semesterCard);
        }

        public static void changeRepeatedFlag(string courseCode, BindingList<semester> semestersList)
        {
            for (int i=0; i<semestersList.Count; i++)
            {
                for (int j=0; j< semestersList[i].Courses.Count; j++)
                {
                    if (semestersList[i].Courses[j].Code == courseCode)
                    {
                        Course tempCourse = semestersList[i].Courses[j];
                        tempCourse.Repeated = true;
                        semestersList[i].Courses[j] = tempCourse;
                    }
                }
            }
        }
    }
}
