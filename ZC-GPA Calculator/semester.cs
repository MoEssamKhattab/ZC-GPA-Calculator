
namespace ZC_GPA_Calculator;

public class Semester
{
    SemesterType title;
    int year;
    List<Course> courses;
    public Semester()
    {
        title = new();
        year = 0;
        courses = new();
    }
    public string Name => $"{year} {title}";
    public int Year { get => year; set => year = value; }
    internal List<Course> Courses { get => courses; set => courses = value; }
    internal SemesterType Title { get => title; set => title = value; }
}