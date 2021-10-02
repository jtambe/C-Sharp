class TopologicalSort_LeetCode210
    {
        //https://leetcode.com/problems/course-schedule-ii/
        
        /*
        This non recursivesolution did not completely work in leetcode test portal
        */
        
        
        public static int[] FindOrder(int numCourses, int[][] prerequisites)
        {
            List<int> result = new List<int>();
            HashSet<int> coursesCompleted = new HashSet<int>();

            // create adjescency list of prerequisites
            Dictionary<int, HashSet<int>> adList = new Dictionary<int, HashSet<int>>();
            foreach (var pre in prerequisites)
            {
                if (adList.TryGetValue(pre[0], out var list))
                {
                    list.Add(pre[1]);
                }
                else
                {
                    adList.Add(pre[0], new HashSet<int>() { pre[1] });
                }
            }

            HashSet<int> adListKeys = new HashSet<int>(adList.Select(x => x.Key));

            for (int i = 0; i < numCourses; i++)
            {
                if (!coursesCompleted.Contains(i) && !adListKeys.Contains(i))
                {
                    result.Add(i);
                    coursesCompleted.Add(i);
                    continue;
                }

                // DFS

                HashSet<int> visited = new HashSet<int>();
                Stack<int> callStack = new Stack<int>();

                visited.Add(i);
                callStack.Push(i);
                while (callStack.Any())
                {
                    var topNode = callStack.Peek();

                    if (adListKeys.Contains(topNode) && adList[topNode].Intersect(callStack).Any())
                    {
                        // cycle observed, topolgical sort not possible
                        return new int[] { };
                    }


                    if (!adListKeys.Contains(topNode) || !adList[topNode].Except(visited).Any())
                    {
                        int lastCourseInAdjChain = callStack.Pop();
                        if (!coursesCompleted.Contains(lastCourseInAdjChain))
                        {
                            result.Add(lastCourseInAdjChain);
                            coursesCompleted.Add(lastCourseInAdjChain);
                        }
                        continue;
                    }

                    foreach (var pre in adList[topNode].Except(visited))
                    {

                        if (!visited.Contains(pre))
                        {
                            visited.Add(pre);
                            callStack.Push(pre);
                            break;
                        }

                    }


                }
            }

            return result.ToArray();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Topological sort Courses");
            var result = FindOrder(2,
                new int[][]
                {
                    new int[] {1,0},
                    new int[] {0,1},
                }
            );
            foreach (var node in result)
            {
                Console.Write(node + " -> ");
            }


            Console.WriteLine();
            var result2 = FindOrder(2,
                new int[][]
                {
                    new int[] {1,0},
                }
            );
            foreach (var node in result2)
            {
                Console.Write(node + " -> ");
            }

            Console.WriteLine();
            var result3 = FindOrder(4,
                new int[][]
                {
                    new int[] {1,0},
                    new int[] {2,0},
                    new int[] {3,1},
                    new int[] {3,2},

                }
            );
            foreach (var node in result3)
            {
                Console.Write(node + " -> ");
            }

            Console.WriteLine();
            var result4 = FindOrder(1,
                new int[][] { }
            );
            foreach (var node in result4)
            {
                Console.Write(node + " -> ");
            }


            Console.ReadKey();

        }
    }
