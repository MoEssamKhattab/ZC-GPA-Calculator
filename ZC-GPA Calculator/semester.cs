using System.ComponentModel;

namespace ZC_GPA_Calculator
{
    public class Semester
    {
        SemesterType title;
        int year;
        List<Course> courses;
        public Semester()
        {
            this.title = new();
            this.year = 0;
            this.courses = new();
        }
        public string Name => $"{year} {title}";
        public int Year { get => year; set => year = value; }
        internal List<Course> Courses { get => courses; set => courses = value; }
        internal SemesterType Title { get => title; set => title = value; }
        public double calculateQualityPoints()
        {
            double _qualityPoints = 0;
            foreach (var course in this.courses)
            {
                _qualityPoints += course.QualityPoints;
            }
            return _qualityPoints;
        }
        public static double calculateOverallQualityPoints(BindingList<Semester> semesters, int endIndex, int startIndex = 0)
        {
            double _qualityPoints = 0;

            for (int i = startIndex; i <= endIndex; i++)
            {
                foreach (var course in semesters[i].Courses)
                {
                    if (course.RepeatedIn == -1 || course.RepeatedIn > endIndex)      // Once repeated, exclude the effect of the old grade
                        _qualityPoints += course.QualityPoints;
                }
            }
            return _qualityPoints;
        }
        public double calculateAttemptedCredits()
        {
            double _credits = 0;
            foreach (var course in this.courses)
            {
                if (course.Grade != "IP")
                    _credits += course.Credits;
            }
            return _credits;
        }
        public static double calculateOverallAttemptedCredits(BindingList<Semester> semesters, int index)
        {
            double _overallCredits = 0;

            for (int i = 0; i <= index; i++)
            {
                foreach (var course in semesters[i].Courses)
                {
                    if (course.Grade != "IP")
                        _overallCredits += course.Credits;
                }
            }
            return _overallCredits;
        }
        public double calculateEarnedCredits()
        {
            double _credits = 0;
            foreach (var course in this.courses)
            {
                //if ((course.Grade == "A" || course.Grade == "A-" || course.Grade == "B+" || course.Grade == "B" || course.Grade == "B-" || course.Grade == "C+" || course.Grade == "C" || course.Grade == "C-" || course.Grade == "P" || course.Grade == "TR") && course.RepeatedIn == -1)
                // ===
                if ((course.GpaCredits != 0 || course.Grade == "P" || course.Grade == "TR") && course.RepeatedIn == -1)
                    _credits += course.Credits;
            }
            return _credits;
        }
        public static double calculateOverallEarnedCredits(BindingList<Semester> semesters, int index)
        {
            double _overallCredits = 0;
            for (int i = 0; i <= index; i++)
            {
                foreach (var course in semesters[i].Courses)
                {
                    if ((course.GpaCredits != 0 || course.Grade == "P" || course.Grade == "TR") && course.RepeatedIn == -1)
                        _overallCredits += course.Credits;
                }
            }
            return _overallCredits;
        }
        public double calculateTotalCredits()
        {
            double _credits = 0;
            foreach (var course in this.courses)
            {
                _credits += course.Credits;
            }
            return _credits;
        }
        public static double calculateOverallTotalCredits(BindingList<Semester> semesters, int index)
        {
            double _overallCredits = 0;
            for (int i = 0; i <= index; i++)
            {
                foreach (var course in semesters[i].Courses)
                {
                    _overallCredits += course.Credits;
                }
            }
            return _overallCredits;
        }
        public double calculateTransferCredits()
        {
            double _credits = 0;
            foreach (var course in this.courses)
            {
                if (course.Grade == "TR")
                    _credits += course.Credits;
            }
            return _credits;
        }
        public static double calculateOverallTransferCredits(BindingList<Semester> semesters, int index)
        {
            double _overallCredits = 0;

            for (int i = 0; i <= index; i++)
            {
                foreach (var course in semesters[i].Courses)
                {
                    if (course.Grade == "TR")
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
        public static double calculateOverallGPACredits(BindingList<Semester> semesters, int endIndex, int startIndex = 0)
        {
            double _overallGPACredits = 0;

            for (int i = startIndex; i <= endIndex; i++)
            {
                foreach (var course in semesters[i].Courses)
                {
                    if (course.RepeatedIn == -1 || course.RepeatedIn > endIndex)        // Not repeated at all (or) not yet repeat, its effect must be counted
                        _overallGPACredits += course.GpaCredits;
                }
            }
            return _overallGPACredits;
        }
        public double calculateGPA()
        {
            return Math.Round(calculateQualityPoints() / calculateGPACredits(), 4);
        }
        public static double calculateOverallGPA(BindingList<Semester> semesters, int endIndex)
        {
            return Math.Round(calculateOverallQualityPoints(semesters, endIndex) / calculateOverallGPACredits(semesters, endIndex), 4);
        }
    }
}
