
namespace ZC_GPA_Calculator
{
    public enum CourseSubtype : byte { LECTURE, LAB };
    public enum SemesterType : byte { Fall = 0, Spring = 1, Summer = 2 };
    public enum NonGPAGrades : byte 
    { 
        P, 
        I, 
        IP, 
        W, 
        WP, 
        WF, 
        TR,
    };
}
