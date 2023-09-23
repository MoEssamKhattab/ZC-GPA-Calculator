
namespace ZC_GPA_Calculator
{
    public struct Course
    {
        string code;
        string title;
        CourseSubtype subtype;
        string grade;
        int credits;
        int gpaCredits;
        double qualityPoints;
        int repeatedIn;      // Initially = -1, to be assigned to index of the semester in which the course is later repeated
        public Course(string code, string title, CourseSubtype subtype, string grade, int credits, int repeatedIn = -1)
        {
            this.code = code;
            this.title = title;
            this.subtype = subtype;
            this.Grade = grade;
            this.credits = credits;
            this.repeatedIn = repeatedIn;

            //if (grade == "A" || grade == "A-" || grade == "B+" || grade == "B" || grade == "B-" || grade == "C+" || grade == "C" || grade == "C-" || grade == "F" )
            //    this.gpaCredits = credits;
            //else
            //    this.gpaCredits = 0;
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
        internal CourseSubtype Subtype { get => subtype; set => subtype = value; }
        public int GpaCredits { get => gpaCredits; set => gpaCredits = value; }
        public double QualityPoints { get => qualityPoints; set => qualityPoints = value; }
        public int RepeatedIn { get => repeatedIn; set => repeatedIn = value; }

        public double calculateQualityPoints()
        {
            return Math.Round(Utilities.stringToGrade(this.grade) * (double)this.credits, 2);
        }
    }
}
