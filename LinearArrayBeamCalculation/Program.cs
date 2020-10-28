using System;
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


        }
    }
}
