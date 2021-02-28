//Problem Statement
//=================
//You need to pass a message to the bunny prisoners, but to avoid detection, the code you agreed to use is... obscure, to say the least. 
//The bunnies are given food on standard-issue prison plates that are stamped with the numbers 0-9 for easier sorting, 
//and you need to combine sets of plates to create the numbers in the code. 
//The signal that a number is part of the code is that it is divisible by 3. You can do smaller numbers like 15 and 45 easily, 
//but bigger numbers like 144 and 414 are a little trickier. 
//Write a program to help yourself quickly create large numbers for use in the code, given a limited number of plates to work with.
//You have L, a list containing some digits (0 to 9). 
//Write a function solution(L) which finds the largest number that can be made from some or all of these digits and is divisible by 3. 
//If it is not possible to make such a number, return 0 as the solution. L will contain anywhere from 1 to 9 digits.  
//The same digit may appear multiple times in the list, but each element in the list may only be used once.


//Test cases
//==========
//Input:
//Solution.solution({3, 1, 4, 1})
//Output:
//    4311

//Input:
//Solution.solution({3, 1, 4, 1, 5, 9})
//Output:
//    94311


namespace FooBar
{
    class PlateMessenger
    {
        public static int[] inputArray;
        private static int[] resultArray;
        private static bool terminate;

        public static int GetCode(int[] l)
        {
            ArrayExtensions.SortDescending(l);

            if (ArrayExtensions.IsDivisibleByThree(l)) return ArrayExtensions.ConvertToNumber(l);

            inputArray = l;
            resultArray = null;
            SearchForArray();

            return (resultArray == null) ? 0 : ArrayExtensions.ConvertToNumber(resultArray);
        }

        private static void SearchForArray()
        {
            terminate = false;

            for (int arraySize = inputArray.Length - 1; arraySize > 0; arraySize--)
            {
                FindCombinations(new int[arraySize], 0, 0, numberOfDigits: inputArray.Length, arraySize);
                if (terminate) break;
            }
        }

        private static void FindCombinations(int[] indexCombination, int index, int start, int numberOfDigits, int arraySize)
        {
            if (index == arraySize)
            {
                var potentialArray = GetPotentialArray(indexCombination);

                if (ArrayExtensions.IsDivisibleByThree(potentialArray))
                {
                    resultArray = potentialArray;
                    terminate = true;
                }
                return;
            }

            //Special Expression in Loop: (ArraySize - Position) <= (NumberOfDigits - CurrentValue)
            // => arraySize - (index+1) <= numberOfDigits - (i+1)
            // => arraySize - index <= numberOfDigits - i
            // => i-index <= numberOfDigits-arraySize
            // => Refer: https://medium.com/enjoy-algorithm/find-all-possible-combinations-of-k-numbers-from-1-to-n-88f8e3fad33c
            // => Solution 2: Fix elements and recur for creating a combination of K numbers
            for (int i = start; i < numberOfDigits &&  i-index <= numberOfDigits-arraySize; i++)
            {
                if (i>start && inputArray[i] == inputArray[i-1]) continue; //Avoid Duplicate Combinations
                indexCombination[index] = i;
                FindCombinations(indexCombination, index + 1, i + 1, numberOfDigits, arraySize);
                if (terminate) return;
            }
        }

        private static int[] GetPotentialArray(int[] indexes)
        {
            var result = new int[indexes.Length];

            for (int i = 0; i < result.Length; i++)
                result[i] = inputArray[indexes[i]];

            return result;
        }
    }

    public static class ArrayExtensions
    {
        //Insertion Sort Algorithm
        public static void SortDescending(int[] arr)
        {
            int n = arr.Length;
            for (int i = 1; i < n; ++i)
            {
                int key = arr[i];
                int j = i - 1;

                while (j >= 0 && arr[j] < key)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
        }

        public static bool IsDivisibleByThree(int[] digits)
        {
            int sum = 0;
            foreach(int i in digits) sum += i;
            return sum % 3 == 0;
        }

        public static int ConvertToNumber(int[] digits)
        {
            int num = 0;
            foreach (int a in digits) num = 10 * num + a;
            return num;
        }
    }
}
