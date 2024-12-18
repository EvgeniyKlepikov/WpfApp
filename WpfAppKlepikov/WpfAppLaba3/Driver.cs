using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLaba3
{
    enum GENDER { male, female, variant };
    enum COLOREYES { brown, green, blue, gray, black };

    public class Driver : INotifyPropertyChanged
    {
        int number;
        char class1;
        double hgt;
        string name;
        string address;
        GENDER gender;
        COLOREYES eyes;
        DateTime dob;
        DateTime iss;
        DateTime exp;
        bool donor;
        string uriImage;

        public Driver()
        {
        }

        public int Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged(nameof(number));
            }
        }
        public char Class1
        {
            get => class1;
            set
            {
                class1 = value;
                OnPropertyChanged(nameof(class1));
            }
        }
        public double Hgt 
        { 
            get => hgt; 
            set 
            {
                hgt = value;
                OnPropertyChanged(nameof(hgt));
            } 
        }
        public string Name 
        { 
            get => name; 
            set 
            {
                name = value;
                OnPropertyChanged(nameof(name));
            } 
        }
        public string Address 
        { 
            get => address; 
            set 
            {
                address = value;
                OnPropertyChanged(nameof(address));
            } 
        }
        public DateTime Dob
        {
            get => dob;
            set
            {
                dob = value;
                OnPropertyChanged($"Dob");
            }
        }
        public DateTime Iss
        {
            get => iss;
            set
            {
                iss = value;
                OnPropertyChanged($"{nameof(iss)}");
            }
        }
        public DateTime Exp
        {
            get => exp;
            set
            {
                exp = value;
                OnPropertyChanged($"{nameof(exp)}");
            }
        }
        public bool Donor 
        { 
            get => donor; 
            set 
            {
                donor = value;
                OnPropertyChanged(nameof(donor));
            } 
        }
        public string UriImage { get => uriImage; set => uriImage = value; }
        internal GENDER Gender { get => gender; set => gender = value; }
        internal COLOREYES Eyes { get => eyes; set => eyes = value; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public override string? ToString()
        {
            return $"N{Number} {Class1} from {Iss:d} to {Exp:d}.\n{Name}, {Gender} Dob({Dob:d}). {Address}. Height {Hgt:0}.\nEyes {Eyes}. " +
                $"{(Donor ? "Donor" : "Not donor")}\n{UriImage}";
        }
    }
}
