using Predmetni_projekat_Formula1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Predmetni_projekat_Formula1
{

    class Vozac
    {
        private uint id;
        private string first_Name;
        private string last_Name;
        private string team;
        private string nationality;
        private string chassis_Number;
        private int num_Races;
        private int num_Wins;
        private string picture_path;

        public Vozac(uint ID, string First_Name, string Last_Name, string Team, string Nationality, string Chassis_Number, int Num_Races, int Num_Wins, string Picture_path) {
            this.id = ID;
            this.first_Name = First_Name;
            this.last_Name = Last_Name;
            this.team = Team;
            this.nationality = Nationality;
            this.chassis_Number = Chassis_Number;
            this.num_Races = Num_Races;
            this.num_Wins = Num_Wins;
            this.picture_path = Picture_path;
        }

        public uint ID { get { return id; } set { id = value; } }
        public string First_Name { get { return first_Name; } set { first_Name = value; } }
        public string Last_Name { get { return last_Name; } set { last_Name = value; } }
        public string Team { get { return team; } set { team = value; } }
        public string Nationality { get { return nationality; } set { nationality = value; } }
        public string Chassis_Number { get { return chassis_Number; } set { chassis_Number = value; } }
        public int Num_Races { get { return num_Races; } set { num_Races = value; } }
        public int Num_Wins { get { return num_Wins; } set { num_Wins = value; } }
        public string Picture_path { get { return picture_path; } set { picture_path = value; } }

        public override string ToString() { 
        string str = id.ToString() + ". " + first_Name + " " + last_Name + " " + team + " " + nationality + " " + chassis_Number + " " + num_Races.ToString() + " " + num_Wins.ToString();

            return str;
        }
    }
}
