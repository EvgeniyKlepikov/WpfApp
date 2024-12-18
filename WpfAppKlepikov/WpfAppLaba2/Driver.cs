using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLaba2
{
    enum GENDER { male, female, variant};
    enum COLOREYES { brown, green, blue, gray, black};

    public class Driver
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

        public int Number { get => number; set => number = value; }
        public char Class1 { get => class1; set => class1 = value; }
        public double Hgt { get => hgt; set => hgt = value; }
        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        public DateTime Dob { get => dob; set => dob = value; }
        public DateTime Iss { get => iss; set => iss = value; }
        public DateTime Exp { get => exp; set => exp = value; }
        public bool Donor { get => donor; set => donor = value; }
        public string UriImage { get => uriImage; set => uriImage = value; }
        internal GENDER Gender { get => gender; set => gender = value; }
        internal COLOREYES Eyes { get => eyes; set => eyes = value; }

        public override string? ToString()
        {
            return $"N{Number} {Class1} from {Iss:d} to {Exp:d}.\n{Name}, {Gender} Dob({Dob:d}). {Address}. Height {Hgt:0}.\nEyes {Eyes}. " +
                $"{(Donor ? "Donor" : "Not donor")}\n{UriImage}";
        }
    }
}
