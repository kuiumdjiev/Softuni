using System;
using System.Collections.Generic;

namespace PersonsInfo
{
    public class Team
    {
        private string name;
        private List<Person> firstTeam;
        private List<Person> reserveTeam;

        public Team(string name)
        {
            
        }

        public string Name { get;private set; }
        public  IReadOnlyCollection<Person> FirstTeam=>firstTeam.AsReadOnly();
        public IReadOnlyCollection<Person> ReverseTeam => reserveTeam.AsReadOnly();
        public void AddPlayer(Person person)
        {
            if (person.Age < 40)
            {
                firstTeam.Add(person);
            }
            else
            {
                reserveTeam.Add(person);
            }
        }

        public override string ToString()
        {
            return $"First team has {FirstTeam.Count} players." + Environment.NewLine +
                   $"Reserve team has {ReverseTeam.Count} players.";
        }
    }
}