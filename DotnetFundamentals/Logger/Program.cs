﻿using System;

namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var l = new Logger();
            l.Error("No!!!!!!!");
            Console.ReadLine();
        }
    }
}
