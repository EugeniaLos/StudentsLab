using System;
using System.IO;

namespace CsvEnumerable
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var data = new CsvEnumerable<User>(Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "File.csv"));
            foreach(User record in data)
            {
                Console.WriteLine(record.Name);
            }
        }
    }
}
