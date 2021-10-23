using System;
using System.Data;
using  System.Data.SqlClient;

namespace _9
{
    class Program
    {
        static void Main(string[] args)
        {

            int id = int.Parse(Console.ReadLine());

            //connect to minions
            using SqlConnection connection = new SqlConnection("Server=.;Database=MinionsDB;Integrated Security=true");
            connection.Open();

            string proc = @"usp_GetOlder";

            using SqlCommand proCommand = new SqlCommand(proc, connection);
            proCommand.CommandType = CommandType.StoredProcedure;
            proCommand.Parameters.AddWithValue("@MinionId", id);
            proCommand.ExecuteNonQuery();


            string getInfo = @"SELECT [Name] , Age  FROM Minions
                              WHERE Id = @MinionId";
            using SqlCommand commandGet = new SqlCommand(getInfo, connection);
            commandGet.Parameters.AddWithValue("@MinionId", id);
            using SqlDataReader info = commandGet.ExecuteReader();
            while (info.Read())
            {
                Console.WriteLine($"{info["Name"].ToString()} {info["Age"].ToString()}");
            }


        }
    }
}
