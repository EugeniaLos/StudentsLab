using System;

namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var l = Logger.Instance;
            l.Warning("Troubling situation!");
            Console.ReadLine();
        }
    }
}
