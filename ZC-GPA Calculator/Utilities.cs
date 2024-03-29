﻿using HtmlAgilityPack;
using System.Net;
using System.ComponentModel;

namespace ZC_GPA_Calculator
{    
    internal static class Utilities
    {
        public static BindingList<Semester> readHtmlTranscript(string filePath, out string studentName, out string major)
        {
            BindingList<Semester> semestersList = new();   //List of all semesters

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
                Semester semester = new Semester();
                HtmlNode div = table.ParentNode.ParentNode;

                var h4Element = div.SelectSingleNode(".//h4");
                if (h4Element != null)
                {
                    string[] semesterHeader = (WebUtility.HtmlDecode(h4Element.InnerText.Trim())).Split(' ');
                    semester.Year = int.Parse(semesterHeader[0]);
                    semester.Title = stringToSemesterType(semesterHeader[1]);
                }

                HtmlNodeCollection rows = table.SelectNodes(".//tr");

                for (int i = 1; i < rows?.Count; i++)        //starting from index 1 to skip the header
                {
                    HtmlNode row = rows[i];
                    HtmlNodeCollection cells = row.SelectNodes(".//th|td");

                    if (cells != null && cells.Count > 1)       // >1 (insted of 0) to skip rows of final grade comments in case of, e.g., incomplete
                    {
                        string courseCode = WebUtility.HtmlDecode(cells[0].InnerText.Trim());
                        string courseTitle = WebUtility.HtmlDecode(cells[1].InnerText.Trim());
                        CourseSubtype courseSubType = (CourseSubtype)Enum.Parse(typeof(CourseSubtype), WebUtility.HtmlDecode(cells[2].InnerText.Trim()));
                        string courseGrade = WebUtility.HtmlDecode(cells[3].InnerText.Trim());
                        byte courseCredits = Convert.ToByte(Math.Floor(Convert.ToDouble(cells[4].InnerText.Trim())));
                        bool isRepeat = courseGrade.StartsWith('[');

                        if (isRepeat) { courseGrade = courseGrade.Substring(1, courseGrade.Length - 2); }
                        //Handling the case of any repeated courses
                        if (isRepeat && !isDroppedCourse(courseGrade) && courseGrade != "I" && courseGrade != "IP")
                        {
                            findRepeatedCourse(courseCode, semestersList);
                        }
                        Course course = new Course(courseCode, courseTitle, courseSubType, courseGrade, courseCredits, -1, isRepeat);
                        semester.Courses.Add(course);
                    }
                }
                semestersList.Add(semester);
            }
            return semestersList;
        }
        public static SemesterType stringToSemesterType(string semesterType)
        {
            SemesterType _semesterType;
            if (Enum.TryParse(semesterType, ignoreCase: true, out _semesterType))
                return _semesterType;
            else
                return SemesterType.Summer1;        // for now, to handle the case of summer 1 semester for the Internship course.
        }

        public static bool isDroppedCourse(string courseGrade)
        {
            return courseGrade == "W" || courseGrade == "WP" || courseGrade == "WF" ;
        }
        public static double stringToGrade(string grade)
        {
            return grade switch
            {
                "A" => Grades.A,
                "A-" => Grades.A_MINUS,
                "B+" => Grades.B_PLUS,
                "B" => Grades.B,
                "B-" => Grades.B_MINUS,
                "C+" => Grades.C_PLUS,
                "C" => Grades.C,
                "C-" => Grades.C_MINUS,
                "D+" => Grades.D_PLUS,
                "D" => Grades.D,
                //"F" =>Grades.F,
                _ => 0.0
            };
        }
        public static void updateSemestersList(BindingList<Semester> semesters, int semesterIndex, int courseIndex, string newGrade)
        {
            semesters[semesterIndex].Courses[courseIndex].Grade = newGrade;
            semesters[semesterIndex].Courses[courseIndex].QualityPoints = semesters[semesterIndex].Courses[courseIndex].calculateQualityPoints();
        }
        public static int getSemesterIndex(SemesterCard semesterCard, List<SemesterCard> semesterCardList)
        {
            return semesterCardList.IndexOf(semesterCard);
        }
        public static void findRepeatedCourse(string courseCode, BindingList<Semester> semestersList)
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
                if (!string.IsNullOrEmpty(oldCourseCode))
                {
                    searchRepeatedCourse(oldCourseCode, semestersList, semestersList.Count - 1);
                }
            }
        }      
        public static bool searchRepeatedCourse(string courseCode, BindingList<Semester> semestersList, int lastIndex)
        {
            for (int i = lastIndex; i >= 0; i--)   // In reverse order to Handle the last occurence
            {
                for (int j = 0; j < semestersList[i].Courses.Count; j++)   // In ordinary order as the course is not repeated at the same semester (it doesn't matter)
                {
                    if (semestersList[i].Courses[j].Code == courseCode)
                    {
                        string courseGrade = semestersList[i].Courses[j].Grade;
                        if (!isDroppedCourse(courseGrade) /*&& courseGrade != "I" && courseGrade != "IP"*/)
                        {
                            semestersList[i].Courses[j].RepeatedIn = Convert.ToSByte(semestersList.Count);
                            return true;
                        }
                        else if (isDroppedCourse(courseGrade) && semestersList[i].Courses[j].IsRepeat)
                        {
                            searchRepeatedCourse(courseCode, semestersList, i - 1);
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static double calculateSpecialGPA(BindingList<Semester> semesters)
        {
            if (semesters.Count <= 2)
                return double.NaN;
            if (semesters.Count == 3 && (isTransferSemester(semesters, 0) || semesters[2].Title == SemesterType.Summer)) 
                return double.NaN;

            int startIndex;
            if (isTransferSemester(semesters, 0) && semesters[3].Title == SemesterType.Summer)
                startIndex = 4;
            else if (semesters[2].Title == SemesterType.Summer || isTransferSemester(semesters, 0))
                startIndex = 3;
            else
                startIndex = 2;

            return Math.Round(Semester.calculateOverallQualityPoints(semesters, semesters.Count-1, startIndex) / Semester.calculateOverallGPACredits(semesters, semesters.Count - 1, startIndex), 4);
        }
        public static bool isTransferSemester(BindingList<Semester> semesters, int semesterIndex)
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
    }
}
