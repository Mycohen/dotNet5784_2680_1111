using System;// Moshe 

namespace Targil0 // Note: actual namespace depends on the project name.
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome2680();
            Welcome1111();
            Console.ReadKey();

        }
        static partial void Welcome1111();
        private static void Welcome2680()
        {
            Console.WriteLine("Enter your name:");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console aplication", userName);
        }
    }
}