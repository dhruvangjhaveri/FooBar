//Problem Statement
//=================
//With her LAMBCHOP doomsday device finished, Commander Lambda is preparing for her debut on the galactic stage - 
//but in order to make a grand entrance, she needs a grand staircase! As her personal assistant, 
//you've been tasked with figuring out how to build the best staircase EVER. 

//Lambda has given you an overview of the types of bricks available, plus a budget. 
//You can buy different amounts of the different types of bricks (for example, 3 little pink bricks, or 5 blue lace bricks).
//Commander Lambda wants to know how many different types of staircases can be built with each amount of bricks, 
//so she can pick the one with the most options. 

//Each type of staircase should consist of 2 or more steps.  No two steps are allowed to be at the same height - 
//each step must be lower than the previous one. All steps must contain at least one brick. 
//A step's height is classified as the total amount of bricks that make up that step.

//For example, when N = 3, you have only 1 choice of how to build the staircase, 
//with the first step having a height of 2 and the second step having a height of 1: (# indicates a brick)
//#
//##
//21

//When N = 4, you still only have 1 staircase choice:
//#
//#
//##
//31

//But when N = 5, there are two ways you can build a staircase from the given bricks. 
//The two staircases can have heights (4, 1) or(3, 2), as shown below:

//#
//#
//#
//##
//41

//#
//##
//##
//32

//Write a function called solution(n) that takes a positive integer n and returns the number of different staircases 
//that can be built from exactly n bricks. n will always be at least 3 (so you can have a staircase at all), 
//but no more than 200, because Commander Lambda's not made of money!


using System;

namespace FooBar
{
    class Staircase
    {
        //The Below Function derives all distinct partitions of an Integer n. It uses following 2 properties:
        //1. The number of partitions of n with largest part k is the same as the number of partitions into k parts
        //2. The number of partitions of n into positive integers no greater than k is equal to the number of partitions 
        //   of n + k(k+1)/2 into k distinct positive integers
        public static int GetDistinctPartitions(int total)
        {                         
            int[] partitionArray = new int[total + 1];
            partitionArray[0] = 1;

            int maxTerms = InverseSumUpto(total);
            int distinctPartsCount = 0;
            for (int j = 1; j <= maxTerms; j++)
            {
                int sumUptoJ = SumUpto(j);
                for (int k = j; k <= total - sumUptoJ; k++)
                    partitionArray[k] += (partitionArray[k - j]);

                distinctPartsCount += (partitionArray[total - sumUptoJ]);
            }
            return distinctPartsCount-1;
        }                 

        public static int SumUpto(int x)
        {
            return ((x * (x + 1)) / 2);
        }

        public static int InverseSumUpto(int x)
        {
            return (int)(((int)Math.Sqrt(8 * x + 1) - 1) / 2);
        }
    }
}
