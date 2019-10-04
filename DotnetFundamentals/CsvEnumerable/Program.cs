using System;

namespace CsvEnumerable
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var data = new CsvEnumerable<User>("File.csv");
            foreach(User record in data)
            {
                Console.WriteLine(record.Name);
            }
        }
    }
}
