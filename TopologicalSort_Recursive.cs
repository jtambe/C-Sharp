class TopologicalSort_Recursive
    {

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



            for (int course = 0; course < numCourses; course++)
            {
                if (coursesCompleted.Contains(course))
                {
                    continue;
                }
                // DFS
                Stack<int> callStack = new Stack<int>();
                callStack.Push(course);
                var isDAGCycleFree = DFSRecursive(course, adList, result, coursesCompleted, callStack);

                if (!isDAGCycleFree)
                {
                    return new int[] { };
                }

            }

            return result.ToArray();

        }

        public static bool DFSRecursive(int course, Dictionary<int, HashSet<int>> adList, List<int> result, HashSet<int> coursesCompleted, Stack<int> callStack)
        {
            if (adList.TryGetValue(course, out var preReqs))
            {
                foreach (var pre in preReqs)
                {
                    // check if there is a cycle in there
                    if (callStack.Contains(pre))
                    {
                        return false;
                    }
                    // if a course is completed, its preReqs are also completed. Do not process that course again
                    if (coursesCompleted.Contains(pre))
                    {
                        continue;
                    }
                    else
                    {
                        callStack.Push(pre);
                        // if there is cycle, return immediately, do not allow further instructions
                        if (!DFSRecursive(pre, adList, result, coursesCompleted, callStack)) return false;
                        callStack.Pop();
                    }

                }

            }

            result.Add(course);
            coursesCompleted.Add(course);
            return true;


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
