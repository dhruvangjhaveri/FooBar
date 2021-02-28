//Problem Statement
//=================
//You 've been assigned the onerous task of elevator maintenance - ugh! It wouldn't be so bad, except that all the elevator 
//documentation has been lying in a disorganized pile at the bottom of a filing cabinet for years, and you don't even know what 
//elevator version numbers you'll be working on. 

//Elevator versions are represented by a series of numbers, divided up into major, minor and revision integers. 
//New versions of an elevator increase the major number, e.g. 1, 2, 3, and so on. When new features are added to an 
//elevator without being a complete new version, a second number named "minor" can be used to represent those new additions, 
//e.g. 1.0, 1.1, 1.2, etc.Small fixes or maintenance work can be represented by a third number named "revision", 
//e.g. 1.1.1, 1.1.2, 1.2.0, and so on. The number zero can be used as a major for pre-release versions of elevators, 
//e.g. 0.1, 0.5, 0.9.2, etc (Commander Lambda is careful to always beta test her new technology, with her loyal henchmen as subjects!).

//Given a list of elevator versions represented as strings, write a function solution(l) that returns 
//the same list sorted in ascending order by major, minor, and revision number so that you can identify the current elevator version. 
//The versions in list l will always contain major numbers, but minor and revision numbers are optional. 
//If the version contains a revision number, then it will also have a minor number.

//For example, given the list l as ["1.1.2", "1.0", "1.3.3", "1.0.12", "1.0.2"], the function solution(l) would 
//return the list["1.0", "1.0.2", "1.0.12", "1.1.2", "1.3.3"].If two or more versions are equivalent but one version 
//contains more numbers than the others, then these versions must be sorted ascending based on how many numbers they have, 
//e.g ["1", "1.0", "1.0.0"]. The number of elements in the list l will be at least 1 and will not exceed 100.

//Test cases
//==========
//Input:
//Solution.solution({ "1.11", "2.0.0", "1.2", "2", "0.1", "1.2.1", "1.1.1", "2.0"})
//Output:
//0.1,1.1.1,1.2,1.2.1,1.11,2,2.0,2.0.0

//Input:
//Solution.solution({ "1.1.2", "1.0", "1.3.3", "1.0.12", "1.0.2"})
//Output:
//1.0,1.0.2,1.0.12,1.1.2,1.3.3

using System;

namespace FooBar
{
    class ElevatorMaintenance
    {
        public static string[] SortVersions(string[] l)
        {
            ElevatorVersion[] my = CreateElevatorVersions(l);

            Array.Sort(my);

            for (int i = 0; i < l.Length; i++)
            {
                l[i] = my[i].Number;
            }

            return l;
        }

        private static ElevatorVersion[] CreateElevatorVersions(string[] input)
        {
            ElevatorVersion[] result = new ElevatorVersion[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                result[i] = new ElevatorVersion(input[i]);
            }
            return result;
        }

    }

    public class ElevatorVersion : IComparable<ElevatorVersion>
    {
        private int[] numberArray = new int[3] { -1, -1, -1 };

        public ElevatorVersion(string versionNumber)
        {
            StoreVersion(versionNumber);
            Number = versionNumber;
        }

        public string Number { get; }

        private void StoreVersion(string versionNumber)
        {
            string number = "";
            int index = 0;

            foreach (var character in versionNumber)
            {
                if (character.ToString() == ".")
                {                    
                    this.numberArray[index] = int.Parse(number);
                    number = "";
                    index++;
                    if (index > 2) throw new ArgumentException($"Invalid Version Number:{versionNumber}");
                    continue;
                }
                number += character;
            }

            this.numberArray[index] = int.Parse(number);
        }

        public int CompareTo(ElevatorVersion other)
        {
            if (this.numberArray[0] != other.numberArray[0]) return this.numberArray[0] - other.numberArray[0];
            if (this.numberArray[1] != other.numberArray[1]) return this.numberArray[1] - other.numberArray[1];
            if (this.numberArray[2] != other.numberArray[2]) return this.numberArray[2] - other.numberArray[2];
            else return 0;
        }
    }
}
