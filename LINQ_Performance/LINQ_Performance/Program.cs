using System;
using System.Collections.Generic;
using StandardObjects.Education;
using System.Diagnostics;
using System.Linq;

namespace LINQ_Performance
{
	class MainClass
	{
		private static List<Student> students;
		private static Stopwatch For_Stopwatch;
		private static Stopwatch LINQ_Stopwatch;
		public static void Main (string[] args)
		{
			students = new List<Student> ();

			students.Add (new Student (1, "John", 'K', "Smith", 'M'));
			students.Add (new Student (2, "Mary", 'M', "Franklin", 'F'));
			students.Add (new Student (3, "Cary", 'R', "Phillips", 'F'));
			students.Add (new Student (4, "Mike", 'L', "Winter", 'M'));

			students [0].Assignments.Add (new Assignment (1, "Assignment 1", "", 100, 95));
			students [0].Assignments.Add (new Assignment (2, "Assignment 2", "", 90, 81));
			students [0].Assignments.Add (new Assignment (3, "Assignment 3", "", 110, 77));
			students [0].Assignments.Add (new Assignment (4, "Assignment 4", "", 200, 175));

			students [1].Assignments.Add (new Assignment (1, "Assignment 1", "", 100, 84));
			students [1].Assignments.Add (new Assignment (2, "Assignment 2", "", 90, 79));
			students [1].Assignments.Add (new Assignment (3, "Assignment 3", "", 110, 105));
			students [1].Assignments.Add (new Assignment (4, "Assignment 4", "", 200, 150));

			students [2].Assignments.Add (new Assignment (1, "Assignment 1", "", 100, 44));
			students [2].Assignments.Add (new Assignment (2, "Assignment 2", "", 90, 61));
			students [2].Assignments.Add (new Assignment (3, "Assignment 3", "", 110, 99));
			students [2].Assignments.Add (new Assignment (4, "Assignment 4", "", 200, 185));

			students [3].Assignments.Add (new Assignment (1, "Assignment 1", "", 100, 65));
			students [3].Assignments.Add (new Assignment (2, "Assignment 2", "", 90, 85));
			students [3].Assignments.Add (new Assignment (3, "Assignment 3", "", 110, 88));
			students [3].Assignments.Add (new Assignment (4, "Assignment 4", "", 200, 180));


			For_Stopwatch = new Stopwatch ();
			LINQ_Stopwatch = new Stopwatch ();

			Test1 ();
			Console.ReadKey ();
		}

		public static void Test1(){
			Dictionary<int,double> ForGrades = new Dictionary<int, double> ();
			double MaxPoints = 0;
			double StudentPoints = 0;

			For_Stopwatch.Start ();
			foreach (var s in students) {
				MaxPoints = 0;
				StudentPoints = 0;
				foreach (var a in s.Assignments) {
					MaxPoints += a.Weight;
					StudentPoints += a.Score;
				}
				ForGrades.Add (s.StudentID, StudentPoints / MaxPoints);
			}
			For_Stopwatch.Stop ();

			LINQ_Stopwatch.Start ();
			var LINQGrades = (from n in students 
				select new {StudentID = n.StudentID,
					Grade = n.Assignments.Sum(k => k.Score) / n.Assignments.Sum(k => k.Weight)}).ToDictionary(n=> n.StudentID, n=> n.Grade);
			LINQ_Stopwatch.Stop ();

			Console.WriteLine ("For loop ticks: {0}", For_Stopwatch.ElapsedTicks);
			foreach (var s in ForGrades) {
				Console.WriteLine ("Student: {0} Grade: {1}", s.Key, s.Value);
			}

			Console.WriteLine ("LINQ ticks: {0}", LINQ_Stopwatch.ElapsedTicks);
			foreach (var s in LINQGrades) {
				Console.WriteLine ("Student: {0} Grade: {1}", s.Key, s.Value);
			}



		}

	}
}
