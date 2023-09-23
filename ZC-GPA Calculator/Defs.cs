
namespace ZC_GPA_Calculator
{
    public enum CourseSubtype : byte { LECTURE, LAB };
    public enum Semester : byte { Fall = 0, Spring = 1, Summer = 2 };
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

    // repeated courses

    /*
     Attempted Hours:
        Attempted hours are defined as all hours accumulated throughout a student's career at an institution 
        (including all passed courses, failed courses, repeated courses, courses dropped after drop-add period, summer courses and transfer work).
        
        Note: Hours currently in progress are not calculated in attempted hours

    Earned Hours:
        Earned hours are defined as credits attained from all courses (including transfer work, AP classes and summer courses) 
        a student has successfully passed.  Withdrawals and failed courses do not count as earned hours.


        Note:  Hours currently in progress are not calculated in earned hours
     */
}
