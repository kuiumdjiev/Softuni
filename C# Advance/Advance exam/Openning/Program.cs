namespace BakeryOpenning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Bakery
    {
        public List<Employee> Employees { get; set; }

        public int Capasity { get; set; }

        public string Name { get; set; }

        public Bakery( string name ,int capasity)
        {
            Employees = new List<Employee>();
            Capasity = capasity;
            Name = name;

        }
        public void Add(Employee employee)
        {
            if (Capasity > Employees.Count)
            {
                Employees.Add(employee);
            }

        }
        public bool Remove(string name)
        {
            Employee employee = Employees.FirstOrDefault(x => x.Name == name);
            return Employees.Remove(employee);
        }
        public Employee GetOldestEmployee()
        {
            Employee employee = Employees.OrderByDescending(x => x.Age).FirstOrDefault();

            return employee;
        }
        public Employee GetEmployee(string name)
        {
            Employee employee = Employees.FirstOrDefault(x => x.Name == name);
            return employee;
        }
        
        
        public string Report()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Employees working at Bakery {Name}:");
            foreach (var item in Employees)
            {
                stringBuilder.AppendLine(item.ToString());
            }
            return string.Format(stringBuilder.ToString()).TrimEnd();
        }

        public int Count()
        {
            return Employees.Count();
        }
    }
    public class Employee
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public int Age { get; set; }

        public Employee(string name, int age ,string country)
        {
            Name = name;
            Age = age;
            Country = country;
        }
        public override string ToString()
        {
            return $"Employee: {Name}, {Age} ({Country})";
        }
    }
    public class StartUp
    {

        static void Main(string[] args)
        {
            //Initialize the repository
            Bakery bakery = new Bakery("Barny", 10);
            //Initialize entity
            Employee employee = new Employee("Stephen", 40, "Bulgaria");
            //Print Employee
            Console.WriteLine(employee); //Employee: Stephen, 40 (Bulgaria)

            //Add Employee
            bakery.Add(employee);
            //Remove Employee
            Console.WriteLine(bakery.Remove("Employee name")); //false

            Employee secondEmployee = new Employee("Mark", 34, "UK");

            //Add Employee
            bakery.Add(secondEmployee);

            Employee oldestEmployee = bakery.GetOldestEmployee(); // Employee with name Stephen
            Employee employeeStephen = bakery.GetEmployee("Stephen"); // Employee with name Stephen
            Console.WriteLine(oldestEmployee); //Employee: Stephen, 40 (Bulgaria)
            Console.WriteLine(employeeStephen); //Employee: Stephen, 40 (Bulgaria)

            Console.WriteLine(bakery.Count()); //2

            Console.WriteLine(bakery.Report());
            //Employees working at Bakery Barny:
            //Employee: Stephen, 40 (Bulgaria)
            //Employee: Mark, 34 (UK)

        }
    }
}
