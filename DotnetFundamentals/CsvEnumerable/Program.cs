using System;

namespace CsvEnumerable
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var data = new CsvEnumerable("File.csv");
            foreach(string record in data)
            {
                Console.WriteLine(record);
            }
        }
    }
}
