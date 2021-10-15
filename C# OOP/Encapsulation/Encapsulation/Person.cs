using System;
using System.Collections.Generic;
using System.Text;

namespace PersonsInfo
{
    class Person
    {
        private string firstName;
        private int age;
        private decimal salary;
        private string lastName;
        public string FirstName 
        { get
            {
                return this.firstName;
            }
            private set
            {
                if (value.Length<3)
                {
                    throw new ArgumentException("First name cannot contain fewer than 3 symbols!");
                }
                else
                {
                    firstName= value;
                }
            }
       }
        public string LastName
        {
            get
            {
                return this.lastName;
            }
            private set
            {
                if (value.Length < 3)
                {
                    throw new ArgumentException("Last name cannot contain fewer than 3 symbols!");
                }
                else
                {
                    
                    lastName= value;
                }
            }
        }
        public int Age {
            get 
            {
                return this.age;
            }
            private set
            {
                if (value<=0)
                {
                    throw new ArgumentException(	"Age cannot be zero or a negative integer!");
                }
                else
                {
                    age = value;
                }
            }
                }
        public decimal Salary {
            get
            {
                return this.salary;
            }
            private set 
            {
                if (value>460)
                {
                    throw new AggregateException("Salary cannot be less than 460 leva!");
                }
                salary = value;
            }
        }
        public Person(string firstName, string lastName, int age, decimal salary)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.Salary = salary;

        }
        public void IncreaseSalary(decimal percentage)
        {
            if (this.Age>30)
            {
                this.Salary += Salary * percentage / 100;
            }
            else
            {
                this.Salary += this.Salary * percentage / 200;
            }
        }
        public override string ToString()
        {
            return $"{this.FirstName} , {this.LastName} , {this.Age}, {this.Salary}";
        }
    }
}
