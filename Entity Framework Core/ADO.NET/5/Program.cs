using System;
using System.Collections.Generic;
using  System.Data.SqlClient;

namespace _5
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = Console.ReadLine();

            //connect to minions
            using SqlConnection connection = new SqlConnection("Server=.;Database=MinionsDB;Integrated Security=true");
            connection.Open();

            string update = @"UPDATE Towns 
                            SET [Name] = UPPER([Name])
                            WHERE CountryCode = (SELECT ID FROM Countries
					                            WHERE [Name] = @name)";

            using SqlCommand commandUpper = new SqlCommand(update, connection);
            commandUpper.Parameters.AddWithValue("@name", name);

            int count = commandUpper.ExecuteNonQuery();

            if (count ==0 )
            {
                Console.WriteLine("No town names were affected.");
            }
            else
            {
                Console.WriteLine($"{count} town names were affected.");
                string getName = @"SELECT [Name]  FROM Towns
                                    WHERE CountryCode = (SELECT ID FROM Countries
					                WHERE [Name] = @name)";

                using SqlCommand commandGetNames = new SqlCommand(getName, connection);
                using SqlDataReader info =    commandGetNames.ExecuteReader();
                List<string> towns = new List<string>();

                while (info.Read())
                {
                    towns.Add(info["Name"].ToString());
                }

                Console.WriteLine(string.Join(", ", towns));

            }
        }
    }
}
