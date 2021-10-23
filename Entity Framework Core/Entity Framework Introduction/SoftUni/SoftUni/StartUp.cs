using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni
{
  public  class StartUp
    {
        static void Main()
        {
            SoftUniContext context = new SoftUniContext();
            Console.WriteLine(DeleteProjectById(context));
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            //3
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .OrderBy(x => x.EmployeeId)
                .Select(x => new
                {
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    middleName= x.MiddleName,
                    jobTitle = x.JobTitle,
                    salary = x.Salary,
                }).ToList();
            foreach (var employee in employees)
            {
                sb.AppendLine(
                    $"{employee.firstName} {employee.lastName} {employee.middleName} {employee.jobTitle} {employee.salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            //4
            StringBuilder content = new StringBuilder();

            var employees = context.Employees
                .Where(x => x.Salary > 50000)
                .OrderBy(x => x.FirstName)
                .ToList();

            foreach (var employee in employees)
            {
                content.AppendLine($"{employee.FirstName} - {employee.Salary:F2}");
            }

            return content.ToString().TrimEnd();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            //5
            StringBuilder content = new StringBuilder();

            var employees = context.Employees
                .Where(x => x.Department.Name == "Research and Development")
                .OrderBy(x => x.Salary)
                .ThenByDescending(x=>x.FirstName)
                .ToList();

            foreach (var employee in employees)
            {
                content.AppendLine($"{employee.FirstName} {employee.LastName} from Research and Development - ${employee.Salary:F2}");
            }

            return content.ToString().TrimEnd();
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            //6
            StringBuilder sb = new StringBuilder();
            Address address = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            var nakov = context.Employees
                .FirstOrDefault(employee => employee.LastName == "Nakov");

            nakov.Address = address;

            context.SaveChanges();

            var employeeAddresses = context.Employees
                .OrderByDescending(employee => employee.Address.AddressId)
                .Take(10)
                .Select(employee => employee.Address.AddressText);

            foreach (string employeeAddress in employeeAddresses)
            {
                sb.AppendLine(employeeAddress);
            }
            return sb.ToString().TrimEnd();

        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            //7

            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Include(x => x.Manager)
                .Include(x => x.EmployeesProjects)
                .ThenInclude(x => x.Project)
                .Where(employee => employee.EmployeesProjects
                    .Any(project => project.Project.StartDate.Year >= 2001
                                    && project.Project.StartDate.Year <= 2003))
                .Take(10)
                .ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} - Manager: {employee.Manager.FirstName} {employee.Manager.LastName}");

                foreach (var project in employee.EmployeesProjects)
                {
                    sb.AppendLine($"--{project.Project.Name} -" +
                                       $" {project.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)} - " +
                                       $"{(project.Project.EndDate == null ? "not finished" : project.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture))}");
                }
            }

            return sb.ToString().TrimEnd();

        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            //8
            StringBuilder sb = new StringBuilder();

            var employees = context.Addresses
                .Include(x=>x.Employees)
                .Include(x=>x.Town)
                .OrderByDescending(x=>x.Employees.Count)
                .ThenBy(x=>x.Town.Name)
                .Take(10)
                .ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.AddressText}, {employee.Town.Name} - {employee.Employees.Count} employees");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmloyee147(SoftUniContext context)
        {
            var employees = context
                .Employees
                .Where(x => x.EmployeeId == 147)
                .Select(e => new
                {
                    firstName = e.FirstName,
                    lastName = e.LastName,
                    jobTitle = e.JobTitle,
                    projecs = e.EmployeesProjects
                        .Select(p => p.Project.Name)
                })
                .ToList();

            var sb = new StringBuilder();

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.firstName} {emp.lastName} - {emp.jobTitle}");

                foreach (var p in emp.projecs.OrderBy(pr => pr))
                {
                    sb.AppendLine($"{p}");
                }
            }

            return sb.ToString().Trim();
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            //9
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Select(e => new
                {
                    id= e.EmployeeId,
                    firstName = e.FirstName,
                    lastName = e.LastName,
                    jobTitle = e.JobTitle,
                    projecs = e.EmployeesProjects
                        .Select(p => p.Project.Name)
                })
                .Where(x=>x.id==147)
                .ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.firstName} {employee.lastName} - {employee.jobTitle}");

                foreach (var p in employee.projecs.OrderBy(x=>x))
                {
                    sb.AppendLine($"{p}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            //10
            StringBuilder sb = new StringBuilder();

            var departments = context.Departments
                .Include(x=>x.Manager)
                .Where(x=>x.Employees.Count>5)
                .OrderBy(x=>x.Employees.Count)
                .ThenBy(x=>x.Name);


            foreach (var department in departments)
            {
                sb.AppendLine($"{department.Name} - {department.Manager.FirstName}  {department.Manager.LastName}");
                foreach (var employee in department.Employees)
                {
                    sb.AppendLine($"${employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                }
            }
            return sb.ToString().TrimEnd();
        }
        public static string    GetLatestProjects(SoftUniContext context)
        {
            //11
            StringBuilder sb = new StringBuilder();

            var projects = context.Projects.OrderByDescending(project => project.StartDate)
                .OrderBy(project => project.Name)
                .ToList();

            foreach (var project in projects)
            {
                sb.AppendLine($"{project.Name}");
                sb.AppendLine($"{project.Description}");
                sb.AppendLine($"{project.StartDate.ToString("M/d/yyyy h:mm:ss tt")}");
            }

        
            return sb.ToString().TrimEnd();
        }

        public static string IncreaseSalaries(SoftUniContext context)
        {
            //12
            StringBuilder sb = new StringBuilder();

            var salaries = context.Employees
                .Where(employee =>
                    employee.Department.Name == "Engineering"
                    || employee.Department.Name == "Tool Design"
                    || employee.Department.Name == "Marketing"
                    || employee.Department.Name == "Information Services")
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();

            foreach (var e in salaries)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary * 1.12m:F2})");
            }

            return sb.ToString().TrimEnd();
        }

        public static string DeleteProjectById(SoftUniContext context)
        {
            //13
            StringBuilder sb = new StringBuilder();

            var project = context.Projects.Find(2);
            var employes = context.EmployeesProjects.Where(x => x.Project == project).ToList();

            foreach (var employe in employes)
            {
                context.EmployeesProjects.Remove(employe);
            }

            context.Projects.Remove(project);

            context.SaveChanges();

            var employees = context.Employees.
                Take(10)
                .ToList();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
