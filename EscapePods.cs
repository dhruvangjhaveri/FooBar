//Problem Statement
//=================
//You 've blown up the LAMBCHOP doomsday device and broken the bunnies out of Lambda's prison - and now you need to escape from the space station 
//as quickly and as orderly as possible! The bunnies have all gathered in various locations throughout the station, and need to make their way 
//towards the seemingly endless amount of escape pods positioned in other parts of the station. You need to get the numerous bunnies through the 
//various rooms to the escape pods. Unfortunately, the corridors between the rooms can only fit so many bunnies at a time. 
//What's more, many of the corridors were resized to accommodate the LAMBCHOP, so they vary in how many bunnies can move through them at a time. 

//Given the starting room numbers of the groups of bunnies, the room numbers of the escape pods, and how many bunnies can fit through at a time 
//in each direction of every corridor in between, figure out how many bunnies can safely make it to the escape pods at a time at peak.

//Write a function solution(entrances, exits, path) that takes an array of integers denoting where the groups of gathered bunnies are, 
//an array of integers denoting where the escape pods are located, and an array of an array of integers of the corridors, 
//returning the total number of bunnies that can get through at each time step as an int. The entrances and exits are disjoint and thus will never overlap. 
//The path element path[A][B] = C describes that the corridor going from A to B can fit C bunnies at each time step.  
//There are at most 50 rooms connected by the corridors and at most 2000000 bunnies that will fit at a time.

//For example, if you have:
//entrances = [0, 1]
//exits = [4, 5]
//path = [
//  [0, 0, 4, 6, 0, 0],  # Room 0: Bunnies
//  [0, 0, 5, 2, 0, 0],  # Room 1: Bunnies
//  [0, 0, 0, 0, 4, 4],  # Room 2: Intermediate room
//  [0, 0, 0, 0, 6, 6],  # Room 3: Intermediate room
//  [0, 0, 0, 0, 0, 0],  # Room 4: Escape pods
//  [0, 0, 0, 0, 0, 0],  # Room 5: Escape pods
//]

//Then in each time step, the following might happen:
//0 sends 4 / 4 bunnies to 2 and 6/6 bunnies to 3
//1 sends 4/5 bunnies to 2 and 2/2 bunnies to 3
//2 sends 4/4 bunnies to 4 and 4/4 bunnies to 5
//3 sends 4/6 bunnies to 4 and 4/6 bunnies to 5

//So, in total, 16 bunnies could make it to the escape pods at 4 and 5 at each time step.  
//(Note that in this example, room 3 could have sent any variation of 8 bunnies to 4 and 5, such as 2/6 and 6/6, but the final solution remains the same.)

//Test cases
//==========
//Input:
//Solution.solution({ 0, 1}, { 4, 5}, { { 0, 0, 4, 6, 0, 0}, { 0, 0, 5, 2, 0, 0}, { 0, 0, 0, 0, 4, 4}, { 0, 0, 0, 0, 6, 6}, { 0, 0, 0, 0, 0, 0}, { 0, 0, 0, 0, 0, 0} })
//Output:
//16

//Input:
//Solution.solution({ 0}, { 3}, { { 0, 7, 0, 0}, { 0, 0, 6, 0}, { 0, 0, 0, 8}, { 9, 0, 0, 0} })
//Output:
//6

using System.Collections.Generic;
using System.Linq;

namespace FooBar
{
    public static class EscapePods
    {
        static int[] _entrances;
        static int[] _exits;
        static int[][] _path;
        static List<int[]> availablePathways;

        public static void Initialize()
        {
            _entrances = new int[2] { 0, 1 };
            _exits = new int[2] { 4, 5 };
            _path = new int[6][];

            _path[0] = new int[] { 0, 0, 4, 6, 0, 0 };  // Room 0: Bunnies
            _path[1] = new int[] { 0, 0, 5, 2, 0, 0 };  // Room 1: Bunnies
            _path[2] = new int[] { 0, 0, 0, 0, 4, 4 };  // Room 2: Intermediate room
            _path[3] = new int[] { 0, 0, 0, 0, 6, 6 };  // Room 3: Intermediate room
            _path[4] = new int[] { 0, 0, 0, 0, 0, 0 };  // Room 4: Escape pods
            _path[5] = new int[] { 0, 0, 0, 0, 0, 0 };  // Room 5: Escape pods

            //_entrances = new int[1] { 0 };
            //_exits = new int[1] { 3 };
            //_path = new int[4][];

            //_path[0] = new int[] { 0, 7, 0, 0 };
            //_path[1] = new int[] { 0, 0, 6, 0 };
            //_path[2] = new int[] { 0, 0, 0, 8 };
            //_path[3] = new int[] { 9, 0, 0, 0 };
        }

        public static int Solution()
        {
            Initialize();

            availablePathways = new List<int[]>();
            int finalscore = 0;

            foreach (int entrance in _entrances)
            {
                FindSource(new int[1]{ entrance });
            }

            bool iterate = true;
            while (iterate)
            {
                iterate = false;

                foreach (var pathway in availablePathways)
                {
                    var update = true;
                    for (int i = 0; i < pathway.Count() - 1; i++)
                    {
                        if (_path[pathway[i]][pathway[i + 1]] == 0)
                        {
                            update = false;
                            break;
                        }
                    }

                    if (update)
                    {
                        iterate = true;
                        for (int i = 0; i < pathway.Count() - 1; i++) _path[pathway[i]][pathway[i + 1]]--;
                        finalscore++;
                    }
                }
            }
            return finalscore;
        }

        private static void FindSource(int[] currentPath)
        {
            for (int i = 0; i < _path.Length; i++)
            {
                if (_path[currentPath.Last()][i] > 0  && !currentPath.Contains(i) && !_entrances.Contains(i))
                {
                    var newCurrentPath = new int[currentPath.Length + 1];
                    for(int k = 0; k < currentPath.Length; k++) newCurrentPath[k] = currentPath[k];
                    newCurrentPath[newCurrentPath.Length - 1] = i;

                    if (_exits.Contains(i)) availablePathways.Add(newCurrentPath);
                    else FindSource(newCurrentPath);
                }
            }
        }
    }
}


