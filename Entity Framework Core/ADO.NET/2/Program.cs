using System;
using System.Data.SqlClient;

namespace _2
{
    class Program
    {
        static void Main(string[] args)
        {
            //connect to minions
            using SqlConnection connection = new SqlConnection("Server=.;Database=MinionsDB;Integrated Security=true");
            connection.Open();

            string query = @"SELECT v.Name + ' - ' + CONVERT(NVARCHAR,COUNT(mv.MinionId)) AS Output FROM Villains AS v 
                           JOIN MinionsVillains AS mv ON mv.VillainId = v.Id
                           GROUP BY v.Id, v.Name
                           HAVING COUNT(mv.MinionId) > 2
                           ORDER BY COUNT(mv.MinionId)";

            using SqlCommand command = new SqlCommand(query, connection);
            using SqlDataReader reader =command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader["Output"]}");                
            }


        }
    }
}
