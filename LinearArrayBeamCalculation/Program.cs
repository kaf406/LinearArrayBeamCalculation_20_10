using System;
using System.Collections.Generic;
using System.IO;

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

            //StreamReader reader = File.OpenText(file_name);
            StreamReader reader = new StreamReader(file_name);

            //string line = reader.ReadLine();
            //double[] X = new double[10];
            List<double> X_list = new List<double>();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();

                double x = double.Parse(line);
                X_list.Add(x);
                Console.WriteLine("x = {0:f3}", x);
            }

            reader.Close();

        }
    }
}
