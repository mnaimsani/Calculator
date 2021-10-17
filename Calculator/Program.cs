using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please enter expression : ");
            var input = Console.ReadLine();
            var result = new Service().Calculate(input);
            Console.WriteLine($"Result = {result}");
        }       
    }    
}
