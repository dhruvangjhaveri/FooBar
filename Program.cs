using System;

namespace FooBar
{
    class Program
    {
        static void Main(string[] args)
        {
            bool again = true;

            while (again)
            {
                Console.WriteLine("Give Number");

                int n2 = int.Parse(Console.ReadLine());

                Console.WriteLine($"Result: {EscapePods.Solution()}");

                Console.WriteLine("That was Fun! Wanna Try Again? Enter n to Exit");

                again = Console.ReadLine() != "n";
            }
        }
    }
}
