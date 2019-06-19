using System;

using LogParser.ConsoleParser.Infrastructure;

namespace LogParser.ConsoleParser
{
    class Program
    {
        static void Main(string[] args)
        {
            new LogEngine().Run().Wait();
            Console.WriteLine("\r\nDone");
            Console.Read();
        }
    }
}
