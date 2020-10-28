using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace LinearArrayBeamCalculation
{
    class Program
    {
        static void Main(string[] args)
        {
            const string file_name = "LinearArray.csv";

            if (!File.Exists(file_name))
            {
                Console.WriteLine("Файл данных не существует!");
                Console.ReadLine();
                return;
            }

            double[] X = GetCoordinats(file_name);

            const double f0 = 3;
            const double lambda0 = 30 / f0;

            const double deg = Math.PI / 180;
            const double theta1 = 5 * deg;

            //Complex F1 = GetBeamPattern(X, theta1, lambda0);

            const double theta_min = -90 * deg;
            const double theta_max = 90 * deg;
            const double d_theta = 0.1 * deg;

            Complex[] pattern = GetBeamPatter(X, lambda0, theta_min, theta_max, d_theta);

            WritePatternToFile(pattern, theta_min, d_theta, "beam.csv");

            Console.WriteLine("Работа завершена...");
            Console.ReadLine();
        }

        private static void WritePatternToFile(Complex[] Pattern,
            double ThetaMin, double dTheta,
            string FileName)
        {
            //StreamWriter writer = File.CreateText(FileName);
            StreamWriter writer = new StreamWriter(FileName);

            for (int i = 0; i < Pattern.Length; i++)
            {
                Complex value = Pattern[i];

                writer.WriteLine("{4};{0};{1};{2};{3}",
                    value.Magnitude, value.Phase,
                    value.Real, value.Imaginary,
                    (i * dTheta + ThetaMin) * 180 / Math.PI);
            }

            writer.Close();
        }

        private static Complex[] GetBeamPatter(
            double[] X, double Lambda,
            double ThetaMin, double ThetaMax, double dTheta)
        {
            List<Complex> result = new List<Complex>();

            double theta = ThetaMin;
            while (theta <= ThetaMax)
            {
                Complex beam = GetBeamPattern(X, theta, Lambda);
                result.Add(beam);

                theta += dTheta;
            }

            return result.ToArray();
        }

        private static Complex GetBeamPattern(double[] X, double Theta, double Lambda)
        {
            Complex sum = 0;
            double k = 2 * Math.PI / Lambda;

            for (int i = 0; i < X.Length; i++)
            {
                double x = X[i];


                double power = -1 * k * x * Math.Sin(Theta);
                Complex exp = Complex.Exp(new Complex(0, power));

                sum += exp;
            }

            return sum / X.Length;
        }

        private static double[] GetCoordinats(string FileName)
        {
            StreamReader reader = new StreamReader(FileName);

            List<double> X_list = new List<double>();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();

                double x = double.Parse(line);
                X_list.Add(x);
                //Console.WriteLine("x = {0:f3}", x);
            }

            return X_list.ToArray();
        }
    }
}
