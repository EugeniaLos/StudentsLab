using System;

namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var l = new Logger();
            l.Error("New message");
            Console.ReadLine();
        }
    }
}
