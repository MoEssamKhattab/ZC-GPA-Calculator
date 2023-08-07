using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;


namespace ZC_GPA_Calculator
{
    enum CourseSubType : byte { LECTURE, LAB };
    enum Semester : byte { Fall = 0, Spring = 1, Summer = 2 };
    public struct Grades
    {
        public const double A = 4.0;
        public const double A_minus = 3.7;
        public const double B_plus = 3.3;
        public const double B = 3;
        public const double B_minus = 2.7;
        public const double C_plus = 2.3;
        public const double C = 2;
        public const double C_minus = 1.7;
        public const double F = 0.0;

        public double ToGrade(string grade)
        {
            switch (grade)
            {
                case "A":
                    return A;
                case "A-":
                    return A_minus;
                case "B+":
                    return B_plus;
                case "B":
                    return B;
                case "B-":
                    return B_minus;
                case "C+":
                    return C_plus;
                case "C":
                    return C;
                case "C-":
                    return C_minus;
                case "F":
                    return F;
                default:
                    return 0.0;
            }
        }
    }
    struct course
    {
        string code;
        string title;
        CourseSubType subType;
        string grade;
        int credits;

        public course(string code, string title, CourseSubType subType, string grade, int credits)
        {
            this.Code = code;
            this.Title = title;
            this.SubType = subType;
            this.Grade = grade;
            this.Credits = credits;
        }

        public string Code { get => code; set => code = value; }
        public string Title { get => title; set => title = value; }
        public string Grade { get => grade; set => grade = value; }
        public int Credits { get => credits; set => credits = value; }
        internal CourseSubType SubType { get => subType; set => subType = value; }
    }
    struct semester
    {
        Semester title;
        int year;
        List<course> courses;
        public semester()
        {
            this.title = new();
            this.year = 0;
            this.courses = new();
        }
        public int Year { get => year; set => year = value; }
        internal List<course> Courses { get => courses; set => courses = value; }
        internal Semester Title { get => title; set => title = value; }
    }

    internal class Controller
    {
        public static List<semester> readTranscript(string path, out string studentName)
        {
            List<semester> semestersList = new();   //List of all semesters

            //read PDF document
            string allPdfText = ReadPDFfile(path);

            string[] allTextSeparator = { "12588", "Name" };
            string[] separatedText = allPdfText.Split(allTextSeparator, StringSplitOptions.RemoveEmptyEntries);

            studentName = separatedText[1].Trim(Environment.NewLine.ToCharArray());     //(or) *.Trim('\n', '\r');
            string allSemestersString = separatedText[2];

            string[] separatedSemesters = allSemestersString.Split("Term");

            string[] semestersKeywords = Enum.GetNames(typeof(Semester));
            string[] coursesKeywords = Enum.GetNames(typeof(CourseSubType));

            for (int i = 1; i < separatedSemesters.Length; i++)     //ignoring the first element since it's not a real semster data
            {
                semester _semester = new();

                using (StringReader stringReader = new StringReader(separatedSemesters[i]))
                {
                    string line;

                    string courseCode;
                    string courseTitle;
                    CourseSubType courseSubType;
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
                            courseSubType = (CourseSubType)Enum.Parse(typeof(CourseSubType), courseStingArray[courseStingArray.Length - 4]);

                            courseGrade = courseStingArray[courseStingArray.Length - 3];

                            // TODO: Handle the case of any repeated course //

                            courseCredits = Convert.ToInt32(Math.Floor(Convert.ToDouble(courseStingArray[courseStingArray.Length - 2])));

                            course _course = new course(courseCode, courseTitle, courseSubType, courseGrade, courseCredits);
                            _semester.Courses.Add(_course);
                        }
                    }
                }
                semestersList.Add(_semester);
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
    }
}
