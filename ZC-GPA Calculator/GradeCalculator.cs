
using Microsoft.VisualBasic.Devices;
using System.ComponentModel;
using System.Linq;

namespace ZC_GPA_Calculator;

internal static class GradeCalculator
{
	public static bool IsGPAGrade(string grade)
		=> !(grade is "P" or "I" or "IP" or "W" or "WP" or "WF" or "TR");
	public static bool IsDroppedCourse(string courseGrade)
		=> courseGrade is "W" or "WP" or "WF";
	public static bool IsAtteptedCredits(string grade)
		=> grade != "IP";
	public static bool IsEarnedCredits(string grade, byte gpaCredits, sbyte repeatedIn)
		=> gpaCredits is not 0 || (grade is "P" or "TR") && repeatedIn is -1;
	public static bool IsTransferCredits(string grade)
		=> grade == "TR";

	#region Semester Methods
	/// Extention methods for semster calculations
	public static double CalculateQualityPoints(this Semester semester)
	{
		return semester.Courses
			.Sum(course => course.QualityPoints);
	}
	public static double CalculateGPACredits(this Semester semester)
	{
		return semester.Courses
			.Sum(course => course.GpaCredits); 
	}
	public static double CalculateAttemptedCredits(this Semester semester)
	{
		return semester.Courses
			.Where(c => IsAtteptedCredits(c.Grade))
			.Sum(c => c.Credits);
	}
	public static double CalculateEarnedCredits(this Semester semester)
	{
		return semester.Courses
			.Where(c => IsEarnedCredits(c.Grade, c.GpaCredits, c.RepeatedIn))
			.Sum(c => c.Credits);
	}
	public static double CalculateTotalCredits(this Semester semester)
	{
		return semester.Courses
			.Sum(c => c.Credits);
	}
	public static double CalculateTransferCredits(this Semester semester)
	{
		return semester.Courses
			.Where(c => IsTransferCredits(c.Grade))
			.Sum(c => c.Credits);
	}
	// GPA //
	public static double CalculateGPA(this Semester semester)
	{
		return Math.Round(semester.CalculateQualityPoints() / semester.CalculateGPACredits(), 4);
	}
	
	#region Overall
	// shared for all overall calculations to reduce redundancy
	public static double CalculateOverall(BindingList<Semester> semesters, Func<Course, bool> filter, Func<Course, double> criteria, int endIndex, int startIndex = 0)
	{
		return semesters
			.Skip(startIndex)
			.Take(endIndex - startIndex + 1)
			.SelectMany(semester => semester.Courses)
			.Where(filter)
			.Sum(criteria);
	}
	public static double CalculateOverallQualityPoints(BindingList<Semester> semesters, int endIndex, int startIndex = 0)
	{
		return CalculateOverall(semesters,
			course => course.RepeatedIn == -1 || course.RepeatedIn > endIndex,
			course => course.QualityPoints,
			endIndex,
			startIndex);
	}  
	public static double CalculateOverallGPACredits(BindingList<Semester> semesters, int endIndex, int startIndex = 0)
	{
		return CalculateOverall(semesters,
			course => course.RepeatedIn == -1 || course.RepeatedIn > endIndex,
			course => course.GpaCredits,
			endIndex,
			startIndex);
	}
	public static double CalculateOverallAttemptedCredits(BindingList<Semester> semesters, int endIndex, int startIndex = 0)
	{
		return CalculateOverall(semesters,
			course => IsAtteptedCredits(course.Grade),
			course => course.Credits,
			endIndex,
			startIndex);
	}
	public static double CalculateOverallEarnedCredits(BindingList<Semester> semesters, int endIndex, int startIndex = 0)
	{
		return CalculateOverall(semesters,
			course => IsEarnedCredits(course.Grade, course.GpaCredits, course.RepeatedIn),
			course => course.Credits,
			endIndex,
			startIndex);
	}
	public static double CalculateOverallTransferCredits(BindingList<Semester> semesters, int endIndex, int startIndex = 0)
	{
		return CalculateOverall(semesters,
			course => IsTransferCredits(course.Grade),
			course => course.Credits,
			endIndex,
			startIndex);
	}
	public static double CalculateOverallTotalCredits(BindingList<Semester> semesters, int endIndex, int startIndex = 0)
	{
		return semesters
			.Skip(startIndex)
			.Take(endIndex - startIndex + 1)
			.SelectMany(semester => semester.Courses)
			.Sum(course => course.Credits);
	}
	public static double CalculateOverallGPA(BindingList<Semester> semesters, int endIndex)
	{
		return Math.Round(CalculateOverallQualityPoints(semesters, endIndex) / CalculateOverallGPACredits(semesters, endIndex), 4);
	}
	#endregion
	#endregion
}