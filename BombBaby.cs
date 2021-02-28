//Problem Statement
//=================
//You 're so close to destroying the LAMBCHOP doomsday device you can taste it! But in order to do so, 
//you need to deploy special self-replicating bombs designed for you by the brightest scientists on Bunny Planet. 
//There are two types: Mach bombs (M) and Facula bombs (F). The bombs, once released into the LAMBCHOP's inner workings, will automatically deploy to all the strategic points you've identified and destroy them at the same time. 

//But there's a few catches. First, the bombs self-replicate via one of two distinct processes: 
//Every Mach bomb retrieves a sync unit from a Facula bomb; for every Mach bomb, a Facula bomb is created;
//Every Facula bomb spontaneously creates a Mach bomb.

//For example, if you had 3 Mach bombs and 2 Facula bombs, they could either produce 3 Mach bombs and 5 Facula bombs, 
//or 5 Mach bombs and 2 Facula bombs. The replication process can be changed each cycle. 

//Second, you need to ensure that you have exactly the right number of Mach and Facula bombs to destroy the LAMBCHOP device. 
//Too few, and the device might survive. Too many, and you might overload the mass capacitors and create a 
//singularity at the heart of the space station - not good! 

//And finally, you were only able to smuggle one of each type of bomb - one Mach, one Facula - aboard the ship when you arrived, 
//so that's all you have to start with. (Thus it may be impossible to deploy the bombs to destroy the LAMBCHOP, 
//but that's not going to stop you from trying!) 

//You need to know how many replication cycles (generations) it will take to generate the correct amount of bombs to destroy the LAMBCHOP. 
//Write a function solution(M, F) where M and F are the number of Mach and Facula bombs needed. 
//Return the fewest number of generations (as a string) that need to pass before you'll have the exact number of bombs necessary 
//to destroy the LAMBCHOP, or the string "impossible" if this can't be done! 
//M and F will be string representations of positive integers no larger than 10^50. 
//For example, if M = "2" and F = "1", one generation would need to pass, so the solution would be "1". 
//However, if M = "2" and F = "4", it would not be possible.

//Test cases
//==========
//Input:
//Solution.solution('2', '1')
//Output:
//    1

//Input:
//Solution.solution('4', '7')
//Output:
//    4


using System;

namespace FooBar
{
    public static class BombBaby
    {
        public static string CalculateCycles(string M, string F)
        {
            HugeNumber number1 = new HugeNumber(M);
            HugeNumber number2 = new HugeNumber(F);
            HugeNumber counter = new HugeNumber("0");
            LongDivision result;

            if (number1.CompareTo(number2) > 0)
            {
                HugeNumber temp = number1;
                number1 = number2;
                number2 = temp;
            }

            while (true)
            {
                if (number1.Value == "1")
                {
                    counter = counter.Sum(number2.Difference(new HugeNumber("1")));
                    return counter.Value;
                }

                result = number2.DivideBy(number1);

                if (result.Remainder.Value == "0") return "impossible";
                else counter = counter.Sum(result.Quotient);

                number1 = result.Remainder;
                number2 = result.Denominator;
            }
        }
    }

    public class HugeNumber
    {
        public string Value { get; private set; }
        public int Length { get; private set; }

        public HugeNumber(string number)
        {
            Value = number;
            Length = number.Length;
        }

        public HugeNumber Sum(HugeNumber other)
        {
            char[] first; char[] second;

            if (this.CompareTo(other) >= 0)
            {
                first = this.Value.ToCharArray();
                second = other.Value.ToCharArray();
            }
            else
            {
                first = other.Value.ToCharArray();
                second = this.Value.ToCharArray();
            }

            Array.Reverse(first);
            Array.Reverse(second);

            int n1 = first.Length, n2 = second.Length;
            string str = "";
            int carry = 0;

            for (int i = 0; i < n2; i++)
            {
                int sub = ((int)(first[i] - '0') + (int)(second[i] - '0') + carry);

                if (sub >= 10)
                {
                    sub -= 10;
                    carry = 1;
                }
                else carry = 0;

                str += (char)(sub + '0');
            }

            for (int i = n2; i < n1; i++)
            {
                int sub = ((int)(first[i] - '0') + carry);

                if (sub >= 10)
                {
                    sub -= 10;
                    carry = 1;
                }
                else
                    carry = 0;

                str += (char)(sub + '0');
            }

            if (carry > 0) str += "1";

            char[] ch3 = str.ToCharArray();
            Array.Reverse(ch3);
            return new HugeNumber(new string(ch3));
        }

        public HugeNumber Difference(HugeNumber other)
        {
            char[] first; char[] second;

            if (this.CompareTo(other) >= 0)
            {
                first = this.Value.ToCharArray();
                second = other.Value.ToCharArray();
            }
            else
            {
                first = other.Value.ToCharArray();
                second = this.Value.ToCharArray();
            }

            Array.Reverse(first);
            Array.Reverse(second);

            int n1 = first.Length, n2 = second.Length;
            string str = "";
            int carry = 0;

            for (int i = 0; i < n2; i++)
            {
                int sub = ((int)(first[i] - '0') - (int)(second[i] - '0') - carry);


                if (sub < 0)
                {
                    sub += 10;
                    carry = 1;
                }
                else carry = 0;

                str += (char)(sub + '0');
            }

            // subtract remaining digits of Larger Number
            for (int i = n2; i < n1; i++)
            {
                int sub = ((int)(first[i] - '0') - carry);

                if (sub < 0)
                {
                    sub = sub + 10;
                    carry = 1;
                }
                else carry = 0;

                str += (char)(sub + '0');
            }

            char[] ch3 = str.ToCharArray();
            Array.Reverse(ch3);

            str = new string(ch3).TrimStart(new Char[] { '0' });

            if (String.IsNullOrEmpty(str)) str = "0";

            return new HugeNumber(str);
        }

        public int CompareTo(HugeNumber other)
        {
            if (this.Length != other.Length) return this.Length - other.Length;

            for (int i = 0; i < this.Length; i++)
                if (this.Value[i] != other.Value[i]) return this.Value[i] - other.Value[i];

            return 0;
        }
    
        public bool LessThan(HugeNumber other)
        {
            return this.CompareTo(other) < 0; 
        }

        public LongDivision DivideBy(HugeNumber other)
        {
            LongDivision result = new LongDivision(this, other);
            HugeNumber numerator;
            HugeNumber product;
            string quotient = "";
            string remainder = "";

            for (int i = 0; i < result.Numerator.Length; i++)
            {
                if(result.Numerator.Value[i] == '0' && remainder[0] == '0')
                {
                    quotient += "0";
                    continue;
                }

                remainder += result.Numerator.Value[i];
                numerator = new HugeNumber(remainder);

                if (numerator.LessThan(result.Denominator)) quotient += "0";
                else
                {
                    product = result.Denominator;
                    for(int j = 1; j < 10; j++)
                    {
                        if (numerator.Difference(product).LessThan(result.Denominator))
                        {
                            quotient += j.ToString();
                            remainder = numerator.Difference(product).Value;
                            break;
                        }
                        else product = product.Sum(result.Denominator);
                    }
                }
            }

            result.Remainder = new HugeNumber(remainder);
            result.Quotient = new HugeNumber(quotient);
            return result;
        }

    }

    public class LongDivision
    {
        public LongDivision(HugeNumber numerator, HugeNumber denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
            Quotient = new HugeNumber("0");
            Remainder = new HugeNumber("0");
        }

        public HugeNumber Numerator { get; }
        public HugeNumber Denominator { get; }
        public HugeNumber Quotient { get; set; }
        public HugeNumber Remainder { get; set; }
    }
}

