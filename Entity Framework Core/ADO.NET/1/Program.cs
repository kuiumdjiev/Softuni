using System;
using System.Data.SqlClient;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            //create database minions
            using SqlConnection sqlConnection =
                new SqlConnection("Server=.; Database = master; Integrated Security = true");
            sqlConnection.Open();

            string query = "CREATE DATABASE MinionsDB";
            using SqlCommand comand = new SqlCommand(query, sqlConnection);
            comand.ExecuteNonQuery();


            //connect to minions
            using SqlConnection secondConnection =
                new SqlConnection("Server=.;Database=MinionsDB;Integrated Security=true");
            secondConnection.Open();

            //Countries

            string countries = @"CREATE TABLE Countries 
                                (
                           Id     INT PRIMARY KEY IDENTITY,
                                [Name] NVARCHAR(50)
                                )";


            Program.Create(countries, secondConnection);

            //Towns

            string towns = @"CREATE TABLE Towns
                            (
                             Id     INT PRIMARY KEY IDENTITY,
                                [Name] NVARCHAR(50) ,
                                CountryCode INT REFERENCES Countries(Id)
                             )";

            Program.Create(towns, secondConnection);

            //EvilnessFactors

            string evilnessFactors = @"CREATE TABLE  EvilnessFactors
                                    (
                                   Id    INT PRIMARY KEY IDENTITY,
                                     [Name] NVARCHAR(50)
                                    )";

          Program.Create(evilnessFactors, secondConnection);

            //Minions
            string minions = @"CREATE TABLE  Minions
                                    (
                                  Id     INT PRIMARY KEY IDENTITY,
                                     [Name] NVARCHAR(50),
                                     Age INT,
                                     TownId  INT REFERENCES Towns(Id)
                                     )";

            Program.Create(minions, secondConnection);

            //Villains
            string villains = @"CREATE TABLE  Villains
                                    (
                                Id       INT PRIMARY KEY IDENTITY,
                                     [Name] NVARCHAR(50),
                                     EvilnessFactorId  INT REFERENCES EvilnessFactors(Id)
                                     )";

            Program.Create(villains, secondConnection);
            //	MinionsVillains
            string minionsVillains = @"CREATE TABLE MinionsVillains
                                    (
                                      	MinionId  INT REFERENCES 		Minions(Id),
                                     	VillainId  INT REFERENCES 	Villains(Id)
                                     )";

            Program.Create(minionsVillains, secondConnection);


            string dataToCountries = @"INSERT INTO Countries ([Name]) VALUES 
                                  ('Bulgaria'),('Portugal'),
                                  ('Germany'),('England'),
                                  ('Macedonia')";
            Program.Create(dataToCountries, secondConnection);

            // Inserting data into Towns
            string dataToTowns = @"INSERT INTO Towns ([Name], CountryCode) VALUES 
                              ('Sofia', 1),('Pazardzhik', 1),('Tyrnovo', 1),
                              ('Plovdiv', 1),('Albufeira', 2),('Almada', 2),
                              ('Amadora', 2),('Amarante', 2),('Munich', 3),
                              ('Frankfurt', 3),('London', 4)";
            Program.Create(dataToTowns, secondConnection);

            // Inserting data into Minions
            string dataToMinions = @"INSERT INTO Minions (Name,Age, TownId) VALUES
                                ('Jully', 11, 3),('Kevin', 12, 1),('Bob ', 13, 6)
                                ,('Jully', 14, 3),('Cathleen', 15, 2),('Jimmy ', 16, 10)
                                ,('Becky', 17, 5),('Mars', 100, 1),('Steward', 55, 10)
                                ,('Zoe', 225, 5),('Jimmy', 1, 1)";
            Program.Create(dataToMinions, secondConnection);

            // Inserting data into EvilnessFactors
            string dataToEvilnessFactors = @"INSERT INTO EvilnessFactors (Name) VALUES 
                                        ('Super good'),('Good'),('Bad'), ('Evil'),('Super evil')";
            Program.Create(dataToEvilnessFactors, secondConnection);

            //Inserting data into Villains
            string dataToVillains = @"INSERT INTO Villains (Name, EvilnessFactorId) VALUES 
                                 ('Gru',2),('Victor',1),('Gosho',3),
                                 ('Pesho',4),('Hasan',5),('Ivan',1),('Onzi',2)";
            Program.Create(dataToVillains, secondConnection);

            //Inserting data into MinionsVillains
            string dataToMinionsVillains = @"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES 
                                        (1,1),(2,1),(3,5),(2,6),(7,3),(7,1),(8,4),(9,7),
                                        (1,3),(5,7),(5,3),(4,3),(1,2),(11,5),(2,7),(4,2)";
            Program.Create(dataToMinionsVillains, secondConnection);
        }



        static void Create(string comand, SqlConnection database)
        {
            using SqlCommand sqlCommand = new SqlCommand(comand, database);
            sqlCommand.ExecuteNonQuery();
        }
    }
}