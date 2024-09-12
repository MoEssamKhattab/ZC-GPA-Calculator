using static ZC_GPA_Calculator.GradeCalculator;

namespace ZC_GPA_Calculator;

public class Course
{
    string code;
    string title;
    CourseSubtype subtype;
    string grade;
    byte credits;
    byte gpaCredits;
    double qualityPoints;
    sbyte repeatedIn;      // Initially = null, to be assigned to index of the semester in which the course is later repeated
    bool isRepeat;         // indicates if the current course is a repeated version of an old one.
    public Course(string code, string title, CourseSubtype subtype, string grade, byte credits, int year /*= 2020*/, string semester /*= "Fall"*/, sbyte repeatedIn = -1, bool isRepeat = false)
    {
        this.code = code;
        this.title = title;
        this.subtype = subtype;
        this.grade = grade;
        this.credits = credits;
        this.repeatedIn = repeatedIn;
        this.isRepeat = isRepeat;

        if (IsGPAGrade(grade))       // TR === transfer
            gpaCredits = credits;
        else
            gpaCredits = 0;

        QualityPoints = CalculateQualityPoints(year, semester);
    }
    public string Code { get => code; set => code = value; }
    public string Title { get => title; set => title = value; }
    public string Grade
    {
        get => grade;
        set
        {
            grade = value;
            if (IsGPAGrade(grade))
                gpaCredits = credits;
            else
                gpaCredits = 0;
        }
    }
    public byte Credits { get => credits; set => credits = value; }
    internal CourseSubtype Subtype { get => subtype; set => subtype = value; }
    public byte GpaCredits { get => gpaCredits; set => gpaCredits = value; }
    public double QualityPoints { get => qualityPoints; set => qualityPoints = value; }
    public sbyte RepeatedIn { get => repeatedIn; set => repeatedIn = value; }
    public bool IsRepeat { get => isRepeat; set => isRepeat = value; }

    public double CalculateQualityPoints(int year, string semster)
    {
        bool isNewQPtsSchema = Utilities.IsNewQPtsSchema(year, semster);
        return Math.Round(Utilities.StringToGrade(this.grade, isNewQPtsSchema) * this.credits, 2);
    }
    //public double calculateQualityPoints()
    //{
    //    return Math.Round(Utilities.stringToGrade(this.grade) * this.credits, 2);
    //}
}