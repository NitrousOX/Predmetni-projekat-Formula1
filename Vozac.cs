using Predmetni_projekat_Formula1;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Predmetni_projekat_Formula1
{

    public class Vozac : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private uint id;
        private string ?first_Name;
        private string ?last_Name;
        private string ?team;
        private string ?nationality;
        private string ?chassis_Number;
        private int num_Races;
        private int num_Wins;
        private string ?picture_path;
        private double left;
        private double top;

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

        public Vozac() { }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public uint ID
        {
            get { return id; }
            set { if (id != value) { id = value; OnPropertyChanged("ID"); } }
        }

        public string? First_Name
        {
            get { return first_Name; }
            set { if (first_Name != value) { first_Name = value; OnPropertyChanged("First_Name"); } }
        }

        public string? Last_Name
        {
            get { return last_Name; }
            set { if (last_Name != value) { last_Name = value; OnPropertyChanged("Last_Name"); } }
        }

        public string? Team
        {
            get { return team; }
            set { if (team != value) { team = value; OnPropertyChanged("Team"); } }
        }

        public string? Nationality
        {
            get { return nationality; }
            set { if (nationality != value) { nationality = value; OnPropertyChanged("Nationality"); } }
        }

        public string? Chassis_Number
        {
            get { return chassis_Number; }
            set { if (chassis_Number != value) { chassis_Number = value; OnPropertyChanged("Chassis_Number"); } }
        }

        public int Num_Races
        {
            get { return num_Races; }
            set { if (num_Races != value) { num_Races = value; OnPropertyChanged("Num_Races"); } }
        }

        public int Num_Wins
        {
            get { return num_Wins; }
            set { if (num_Wins != value) { num_Wins = value; OnPropertyChanged("Num_Wins"); } }
        }

        public string? Picture_path
        {
            get { return picture_path; }
            set { if (picture_path != value) { picture_path = value; OnPropertyChanged("Picture_path"); } }
        }

        public double Left
        {
            get { return left; }
            set { if (left != value) { left = value; OnPropertyChanged("Left"); } }
        }
        public double Top
        {
            get { return top; }
            set { if (top != value) { top = value; OnPropertyChanged("Top"); } }
        }

        public override string ToString() { 
        string str = id.ToString() + ". " + first_Name + " " + last_Name + " " + team + " " + nationality + " " + chassis_Number + " " + num_Races.ToString() + " " + num_Wins.ToString();

            return str;
        }
    }
}
