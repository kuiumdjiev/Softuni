using System;
using System.Data.SqlClient;
using System.Text;

namespace _3
{
    class Program
    {
        static void Main(string[] args)
        {
            int id = int.Parse(Console.ReadLine());

            //connect to minions
            using SqlConnection connection = new SqlConnection("Server=.;Database=MinionsDB;Integrated Security=true");
            connection.Open();

            string takeVilan = @"SELECT [Name] FROM Villains
                                WHERE Id = @Id";

            using SqlCommand commandVilan = new SqlCommand(takeVilan, connection);
            commandVilan.Parameters.AddWithValue("@Id", id);

            string nameVillain = commandVilan.ExecuteScalar()?.ToString();

            if (string.IsNullOrEmpty(nameVillain))
            {
                Console.WriteLine($"No villain with ID {id} exists in the database.");
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Villain: {nameVillain}");

                string getMinions = @"SELECT m.[Name], m.Age FROM MinionsVillains AS mv
                                     JOIN Minions AS m ON m.Id =  mv.MinionId
                                     WHERE mv.VillainId = @Id
                                     ORDER BY m.[Name]";

                using SqlCommand commandMinions = new SqlCommand(getMinions, connection);
                commandMinions.Parameters.AddWithValue("@Id", id);

                using SqlDataReader info = commandMinions.ExecuteReader();
                if (!info.HasRows)
                {
                    Console.WriteLine("(no minions)");
                }
                else
                {
                    int row = 1;
                    while (info.Read())
                    {
                        sb.AppendLine($"{row} {info["Name"]} {info["Age"]}");
                        row++;
                    }

                    Console.WriteLine(sb.ToString().Trim());
                }
            }
        }
    }
}
