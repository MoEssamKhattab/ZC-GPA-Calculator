using HtmlAgilityPack;
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.parser;
using System.Net;
using System.ComponentModel;

namespace ZC_GPA_Calculator
{    
    internal static class Utilities
    {
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
                        if (courseGrade.StartsWith('[') && courseGrade != "W" && courseGrade != "WP" && courseGrade != "WF" && courseGrade != "I" && courseGrade != "IP")
                        {
                            courseGrade = courseGrade.Substring(1, courseGrade.Length - 2);
                            findRepeatedCourse(courseCode, semestersList);
                        }
                        Course course = new Course(courseCode, courseTitle, courseSubType, courseGrade, courseCredits);
                        semester.Courses.Add(course);
                    }
                }
                semestersList.Add(semester);
            }
            return semestersList;
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
        public static int getSemesterIndex(SemesterCard semesterCard, List<SemesterCard> semesterCardList)
        {
            return semesterCardList.IndexOf(semesterCard);
        }
        public static void changeRepeatedFlag(string courseCode, BindingList<semester> semestersList)
        {
            bool found = false;
            for (int i = semestersList.Count - 1; i >= 0; i--)   // In reverse order to Handle the last occurence
            {
                for (int j = 0; j < semestersList[i].Courses.Count; j++)   // In ordinary order as the course is not repeated at the same semester (it doesn't matter)
                {
                    if (semestersList[i].Courses[j].Code == courseCode)
                    {
                        Course tempCourse = semestersList[i].Courses[j];
                        tempCourse.RepeatedIn = semestersList.Count;
                        semestersList[i].Courses[j] = tempCourse;
                        found = true;
                    }
                }
            }
            if (found == false)
            {
                MessageBox.Show($"It seems that the repeated course, {courseCode}, was repeated with different corse code. Reach the old course and change its grade to 'P', please!");
            }
        }
        public static void findRepeatedCourse(string courseCode, BindingList<semester> semestersList)
        {
            bool found = searchRepeatedCourse(courseCode, semestersList, semestersList.Count - 1);
            
            if (found == false)
            {
                string oldCourseCode = "";
                using (HandleRepeatsForm handleRepeatsForm = new HandleRepeatsForm(courseCode, semestersList))
                {                   
                    DialogResult dialogResult = handleRepeatsForm.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        oldCourseCode = handleRepeatsForm.OldCourseCode;
                    }
                }
                if (oldCourseCode != "")
                    searchRepeatedCourse(oldCourseCode, semestersList, semestersList.Count - 1);
            }
        }      
        public static bool searchRepeatedCourse(string courseCode, BindingList<semester> semestersList, int lastIndex)
        {
            for (int i = lastIndex; i >= 0; i--)   // In reverse order to Handle the last occurence
            {
                for (int j = 0; j < semestersList[i].Courses.Count; j++)   // In ordinary order as the course is not repeated at the same semester (it doesn't matter)
                {
                    if (semestersList[i].Courses[j].Code == courseCode)
                    {
                        string courseGrade = semestersList[i].Courses[j].Grade;
                        if (courseGrade != "W" && courseGrade != "WP" && courseGrade != "WF" /*&& courseGrade != "I" && courseGrade != "IP"*/)
                        {
                            Course tempCourse = semestersList[i].Courses[j];
                            tempCourse.RepeatedIn = semestersList.Count;
                            semestersList[i].Courses[j] = tempCourse;

                            return true;
                        }
                        /*bool isRepeat = semestersList[i].Courses[j].isRepeat;*/
                        else if ((courseGrade == "W" || courseGrade == "WP" || courseGrade != "WF") /*&& isRepeat*/)
                            searchRepeatedCourse(courseCode, semestersList, i-1);
                        
                        else
                            return false;
                    }
                }
            }
            return false;
        }
        public static double calculateSpecialGPA(BindingList<semester> semesters)
        {
            if (semesters.Count <= 2)
                return double.NaN;
            if (semesters.Count == 3 && (isTransferSemester(semesters, 0) || semesters[2].Title == Semester.Summer)) 
                return double.NaN;

            int startIndex;
            if (isTransferSemester(semesters, 0) && semesters[3].Title == Semester.Summer)
                startIndex = 4;
            else if (semesters[2].Title == Semester.Summer || isTransferSemester(semesters, 0))
                startIndex = 3;
            else
                startIndex = 2;

            return Math.Round(semester.calculateOverallQualityPoints(semesters, semesters.Count-1, startIndex) / semester.calculateOverallGPACredits(semesters, semesters.Count - 1, startIndex), 4);
        }
        public static bool isTransferSemester(BindingList<semester> semesters, int semesterIndex)
        {
            foreach (Course course in semesters[semesterIndex].Courses)
            {
                if (course.Grade != "TR")
                    return false;
            }
            return true;
        }

        // To Reduce Graphics Flicker with Double Buffering For WinForms Controls
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            //Taxes: Remote Desktop Connection and painting
            //http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }

        //public static BindingList<semester> readTranscript(string path, out string studentName, out string major)
        //{
        //    BindingList<semester> semestersList = new();   //List of all semesters

        //    //read PDF document
        //    string allPdfText = ReadPDFfile(path);

        //    string[] allTextSeparator = { "12588", "Name" };
        //    string[] separatedText = allPdfText.Split(allTextSeparator, StringSplitOptions.RemoveEmptyEntries);

        //    studentName = separatedText[1].Trim(Environment.NewLine.ToCharArray());     //(or) *.Trim('\n', '\r');
        //    string allSemestersString = separatedText[2];

        //    string[] separatedSemesters = allSemestersString.Split("Term");

        //    string[] semestersKeywords = Enum.GetNames(typeof(Semester));
        //    string[] coursesKeywords = Enum.GetNames(typeof(CourseSubtype));

        //    // Extracting the major
        //    string[] majorTextSeparator = { "Undergraduate/Bachelor of Science/", "No Degree Awarded Yet" };
        //    string[] majorData = separatedSemesters[1].Split(majorTextSeparator, StringSplitOptions.RemoveEmptyEntries);
        //    major = majorData[1].Trim(Environment.NewLine.ToCharArray()).Replace("\n", "").Replace("\r", "");

        //    for (int i = 1; i < separatedSemesters.Length; i++)     //ignoring the first element since it's not a real semster data
        //    {
        //        semester _semester = new();

        //        using (StringReader stringReader = new StringReader(separatedSemesters[i]))
        //        {
        //            string line;

        //            string courseCode;
        //            string courseTitle;
        //            CourseSubtype courseSubType;
        //            string courseGrade;
        //            int courseCredits;

        //            while ((line = stringReader.ReadLine()) != null)
        //            {
        //                if (semestersKeywords.Any(keyword => line.Contains(keyword)))
        //                {
        //                    string[] semesterHeader = line.Split(' ');     //split the year, and semester title
        //                    _semester.Year = int.Parse(semesterHeader[0]);
        //                    _semester.Title = (Semester)Enum.Parse(typeof(Semester), semesterHeader[1]);
        //                }
        //                //Extracting course data
        //                else if (coursesKeywords.Any(keyword => line.Contains(keyword)))
        //                {
        //                    string[] courseStingArray = line.Split(' ');

        //                    courseCode = string.Join(" ", courseStingArray, 0, 2);
        //                    courseTitle = string.Join(" ", courseStingArray, 2, courseStingArray.Length - 6);
        //                    courseSubType = (CourseSubtype)Enum.Parse(typeof(CourseSubtype), courseStingArray[courseStingArray.Length - 4]);
        //                    courseGrade = courseStingArray[courseStingArray.Length - 3];

        //                    //Handling the case of any repeated courses
        //                    if (courseGrade.StartsWith('['))
        //                    {
        //                        courseGrade= courseGrade.Substring(1,courseGrade.Length-2);
        //                        changeRepeatedFlag(courseCode, semestersList);
        //                    }
        //                    courseCredits = Convert.ToInt32(Math.Floor(Convert.ToDouble(courseStingArray[courseStingArray.Length - 2])));

        //                    Course _course = new Course(courseCode, courseTitle, courseSubType, courseGrade, courseCredits);
        //                    _semester.Courses.Add(_course);
        //                }
        //            }
        //        }
        //        semestersList.Add(_semester);
        //    }
        //    return semestersList;
        //}
        //static string ReadPDFfile(string path)
        //{
        //    StringBuilder stringBuilder = new StringBuilder();
        //    try
        //    {
        //        using (PdfReader reader = new PdfReader(path))
        //        {
        //            for (int p = 1; p <= reader.NumberOfPages; p++)
        //            {
        //                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
        //                string text = PdfTextExtractor.GetTextFromPage(reader, p, strategy);
        //                text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));

        //                stringBuilder.Append(text);
        //            }
        //        }
        //        return stringBuilder.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        return "";
        //    }
        //}

    }
}
