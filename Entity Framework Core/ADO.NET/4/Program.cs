using System;
using  System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;


namespace _4
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] info = Console.ReadLine().Split(' ').ToArray();
            string name = info[1];
            int age = int.Parse(info[2]);
            string town = info[3];

            string villan = Console.ReadLine().Split(' ').ToArray()[1];

            //connect to minions
            using SqlConnection connection = new SqlConnection("Server=.;Database=MinionsDB;Integrated Security=true");
            connection.Open();

            string townCheck = @"SELECT Id FROM Towns WHERE @Name = Name";
            string minionCheck = @"SELECT Id FROM Minions WHERE @Name = Name";
            string villainCheck = @"SELECT Id FROM Villains WHERE @Name = Name";
            //town
            using SqlCommand commandTown = new SqlCommand(townCheck, connection);
            commandTown.Parameters.AddWithValue("@Name",town);
            object result = commandTown.ExecuteScalar();
            if (result is null)
            {
                string addTown = @"INSERT INTO Towns (Name) VALUES (@townName)";
                using SqlCommand commandAddTown = new SqlCommand(addTown, connection);
                commandAddTown.Parameters.AddWithValue("@townName", town);
                commandAddTown.ExecuteNonQuery();
                Console.WriteLine($"Town {town} was added to the database.");
            }
            //villain
            using SqlCommand commandVillain = new SqlCommand(villainCheck, connection);
            commandVillain.Parameters.AddWithValue("@Name", villainCheck);
            object resultVillain = commandVillain.ExecuteScalar();
            if (resultVillain is null)
            {
                string addVillain = @"INSERT INTO Villains (Name, EvilnessFactorId) VALUES (@Name, 4)";
                using SqlCommand commandAddVillain = new SqlCommand(addVillain, connection);
                commandAddVillain.Parameters.AddWithValue("@Name", villan);
                commandVillain.ExecuteNonQuery();
                Console.WriteLine($"Villain {villan} was added to the database.");
            }
            //minion
            using SqlCommand commandMinions = new SqlCommand(minionCheck, connection);
            commandMinions.Parameters.AddWithValue("@Name", name);
            object resultMinion = commandMinions.ExecuteScalar();
            if (resultMinion is null)
            {
             
                using SqlCommand commandTownId = new SqlCommand(townCheck, connection);
                commandTownId.Parameters.AddWithValue("@TownName", town);
                int townId = (int)commandTownId.ExecuteScalar();

                string addMinion = @"INSERT INTO Minions (Name, Age, TownId) VALUES (@Name, @Age, @TownId)";
                using SqlCommand commandAddMinion = new SqlCommand(addMinion, connection);
                commandAddMinion.Parameters.AddWithValue("@Name", name);
                commandAddMinion.Parameters.AddWithValue("@Age", age);
                commandAddMinion.Parameters.AddWithValue("@TownId", townId);
                commandAddMinion.ExecuteNonQuery();
            }
            else
            {
                
            }
        }
    }
}
