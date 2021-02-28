//Problem Statement
//=================
//There's some unrest in the minion ranks: minions with ID numbers like "1", "42", and other "good" numbers have been 
//lording it over the poor minions who are stuck with more boring IDs. To quell the unrest, Commander Lambda has tasked you with 
//reassigning everyone new, random IDs based on her Completely Foolproof Scheme. 
//She 's concatenated the prime numbers in a single long string: "2357111317192329...". 
//Now every minion must draw a number from a hat. That number is the starting index in that string of primes, 
//and the minion's new ID number will be the next five digits in the string. So if a minion draws "3", their ID number will be "71113". 
//Help the Commander assign these IDs by writing a function solution(n) which takes in the starting index n of Lambda's string of all primes, 
//and returns the next five digits in the string. 
//Commander Lambda has a lot of minions, so the value of n will always be between 0 and 10000.

//Test cases
//==========
//Input:
//Solution.solution(0)
//Output:
//    23571

//Input:
//Solution.solution(3)
//Output:
//    71113

using System;

namespace FooBar
{
    class MinionIdGenerator
    {
        // This class Generates a 5 Digit ID assuming the Number Drawn is between 0 & 10,000.
        // Sieve of Eratosthenes Algorithm is used to generate Prime Numbers.
        // Since the Number drawn is limited to 10000, the function may ever only need the first 100005 digits from the Prime Numbers.
        // 100005 digits can be obtained from the first 2,286 Prime Numbers (4 1-Digit, 21 2-Digit, 143 3-Digit, 1061 4-Digit & 1057 5-Digit Prime Numbers.
        // Also, 20,231 is the 2,286th Prime Number.
        // Hence the maxPrime is set to 20231.
        private static int maxPrime = 20231;
        private static int idLength = 5;
        private static int numberDrawnLimit = 10000;
        private static bool[] crossedOut;

        public static string getId(int numberDrawn)
        {
            if (numberDrawn > numberDrawnLimit) return "Invalid Input Number";

            generateArrayWithPrimePositions(maxPrime);

            var digits = getPrimeDigitsForID(numberDrawn);

            return String.Join("", digits);
        }

        private static void generateArrayWithPrimePositions(int maxValue)
        {
            crossedOut = new bool[maxValue + 1];
            crossOutMultiples();
        }

        private static void crossOutMultiples()
        {
            // Every multiple in the array has a prime factor that is less than on equal to the root of the array size.
            // So we don't have to cross out multiples of numbers larger than the root. 
            int limit = (int)Math.Sqrt(crossedOut.Length);
            for (int i = 2; i <= limit; i++)
                if (notCrossed(i)) crossedOutMultiplesOf(i);
        }

        private static void crossedOutMultiplesOf(int number)
        {
            for (int multiple = 2 * number; multiple < crossedOut.Length; multiple += number)
                crossedOut[multiple] = true;
        }

        private static bool notCrossed(int index)
        {
            return crossedOut[index] == false;
        }

        private static char[] getPrimeDigitsForID(int numberDrawn)
        {
            var result = new char[idLength];
            int digitCounter = 0;

            for (int j = 0, i = 2; j < idLength; i++)
            {
                if (notCrossed(i))
                    foreach (var digit in i.ToString())
                    {
                        if (j >= idLength) break;
                        if (digitCounter >= numberDrawn) result[j++] = digit;
                        digitCounter++;
                    }
            }

            return result;
        }

    }
}
