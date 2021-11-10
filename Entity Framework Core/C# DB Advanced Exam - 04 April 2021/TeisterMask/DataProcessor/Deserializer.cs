using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Castle.Core.Internal;
using Newtonsoft.Json;
using TeisterMask.Data.Models;
using TeisterMask.Data.Models.Enums;
using TeisterMask.DataProcessor.ImportDto;

namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;

    using Data;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {

        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(ProjectImportModel[]), new XmlRootAttribute("Projects"));
            var textReader = new StringReader(xmlString);
            var projectConvert = serializer.Deserialize(textReader) as ProjectImportModel[];
            var all = new List<Project>();
            foreach (var real in projectConvert)
            {
                if (!IsValid(real))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime OpenDate;
                bool isValidOpenDate = DateTime.TryParseExact(real.OpenDate, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out OpenDate);

                if (!isValidOpenDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                DateTime? DueDate=null;
                if (!String.IsNullOrWhiteSpace( real.DueDate))
                {
                    DateTime nqkakakyvDateTime;
                    bool isValidDueDate = DateTime.TryParseExact(real.DueDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out nqkakakyvDateTime);
                    if (!isValidDueDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    DueDate = nqkakakyvDateTime;
                }

                 Project project = new Project()
                {
                    Name = real.Name,
                    OpenDate = OpenDate,
                    DueDate = DueDate

                };

                foreach (var task in real.Tasks)
                {
                    if (!IsValid(task))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime OpenDateTask;
                    bool isValidOpenDateTask = DateTime.TryParseExact(task.OpenDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out OpenDateTask);

                    if (!isValidOpenDateTask)
                    {

                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime DueDateTask;
                    
                    bool isValidDueDateTask = DateTime.TryParseExact(task.DueDate, "dd/MM/yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out DueDateTask);

                    if (!isValidDueDateTask)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (OpenDateTask<OpenDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (DueDate.HasValue && DueDateTask>DueDate.Value)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Task realTask = new Task()
                    {
                        Name = task.Name,
                        OpenDate = OpenDateTask,
                        DueDate = DueDateTask,
                        ExecutionType = (ExecutionType)task.ExecutionType,
                        LabelType = (LabelType)task.LabelType
                    };
                    project.Tasks.Add(realTask);
                }
                all.Add(project);
                sb.AppendLine($"Successfully imported project - {project.Name} with {project.Tasks.Count} tasks.");
            }


            context.Projects.AddRange(all);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            EmployeesImportModel[] Allemployees = JsonConvert.DeserializeObject<EmployeesImportModel[]>(jsonString);
            List<Employee> employees = new List<Employee>();
            foreach (var employee in Allemployees)
            {
                if (!IsValid(employee))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Employee employeeReal = new Employee()
                {
                    Username = employee.Username,
                    Email = employee.Email,
                    Phone = employee.Phone
                };

                foreach (var task in employee.Tasks.Distinct())
                {
                    if (!context.Tasks.Any(x=>x.Id== task))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var realTask = new EmployeeTask()
                    {
                        TaskId = task
                    };
                    employeeReal.EmployeesTasks.Add(realTask);
                }
                employees.Add(employeeReal);
                sb.AppendLine($"Successfully imported employee - {employeeReal.Username} with {employeeReal.EmployeesTasks.Count} tasks.");
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}