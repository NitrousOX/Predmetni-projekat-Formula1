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

        public uint ID { get; set; }
        public string First_Name { get; set;}
        public string Last_Name { get; set;}
        public string Team { get; set;}
        public string Nationality { get; set;}
        public string Chassis_Number { get; set;}
        public int Num_Races { get; set;}
        public int Num_Wins { get; set;}
        public string Picture_path { get; set;}

    }
}
