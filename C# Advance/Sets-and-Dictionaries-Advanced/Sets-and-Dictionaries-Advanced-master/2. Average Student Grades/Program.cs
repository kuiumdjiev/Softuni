using System;
using System.Collections.Generic;
using System.Linq;

namespace _2._Average_Student_Grades
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<decimal>> studentGrades = new Dictionary<string, List<decimal>>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(' ').ToArray();
                string name = input[0];
                decimal grade = decimal.Parse(input[1]);
                if (!studentGrades.ContainsKey(name))
                {
                    studentGrades.Add(name, new List<decimal>());

                }
                studentGrades[name].Add(grade);
            }
            foreach (var gradeAndStudent in studentGrades)
            {
                Console.WriteLine($"{gradeAndStudent.Key} -> {string.Join(" ",gradeAndStudent.Value)}  {gradeAndStudent.Value.Average()} " );
            }
        }
    }
}
