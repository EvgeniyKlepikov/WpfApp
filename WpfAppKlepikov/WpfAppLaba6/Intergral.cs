using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLaba6
{
    public class Intergral: IDataErrorInfo
    {
        double a, b;
        int n;
        public Func<double,double> func = (x) => Math.Pow(x, 3);

        public Intergral()
        {
        }

        public Intergral(double a, double b, int n)
        {
            this.A = a;
            this.B = b;
            this.N = n;
        }

        public double A { get => a; set => a = value; }
        public double B { get => b; set => b = value; }
        public int N { get => n; set => n = value; }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "A":
                        if (A < -1 || A > 1) 
                        {
                            error = "Начало диапазона должно быть [-1;1]";
                        }
                        break;
                    case "B":
                        if (B < 0 || B > 10)
                        {
                            error = "Начало диапазона должно быть [0;10]";
                        }
                        break;
                    case "N":
                        if (N < 100)
                        {
                            error = "К-во точек разбиения должно быть >= 100";
                        }
                        break;
                }
                return error;
            }
        }

        public async IAsyncEnumerable<(double,double,double)> GetDoublesAsync()
        {
            double h = (B - A) / N;
            double S = 0;
            for (int i = 0; i <= N; i++)
            {
                double x = A + h * i;
                S += func(x) * h;
                await Task.Delay(10);
                yield return (x,S,(double)i/N);
            };
        }
        public override string? ToString()
        {
            return $"A = {A}, B = {B}, N = {N}";
        }
    }
}
