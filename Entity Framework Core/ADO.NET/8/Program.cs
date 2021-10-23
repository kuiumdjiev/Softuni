using System;
using System.Linq;
using  System.Data.SqlClient;

namespace _8
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] allId = Console.ReadLine().Split(' ').ToArray();
            //connect to minions
            using SqlConnection connection = new SqlConnection("Server=.;Database=MinionsDB;Integrated Security=true");
            connection.Open();

            string update = @"UPDATE Minions
                            SET Age += 1
                            WHERE Id  = @id";
            foreach (string id in allId)
            {
                using SqlCommand updateCommand = new SqlCommand(update, connection);
                updateCommand.Parameters.AddWithValue("@id", id);
                updateCommand.ExecuteNonQuery();
            }

            string getInfo = @"SELECT [Name] , Age  FROM Minions";
            using SqlCommand commandGet = new SqlCommand(getInfo, connection);
            using SqlDataReader info = commandGet.ExecuteReader();
            while (info.Read())
            {
                Console.WriteLine($"{info["Name"].ToString()} {info["Age"].ToString()}");
            }
        }
    }
}
