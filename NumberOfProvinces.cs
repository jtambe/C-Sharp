    class NumberOfProvinces_LeetCode547
    {

        public static int FindCircleNum(int[][] isConnected)
        {

            int numberOfNodes = isConnected.Length;
            //maintain Parents of each node
            //initially every node will be parent of itself before any merging operation begins
            int[] parentArray = new int[numberOfNodes];
            for (int i = 0; i < numberOfNodes; i++)
            {
                parentArray[i] = i;
            }

            //rankArray maintains number of children elements under node's tree
            //initially, all nodes are by themselves, so they only contain themselves in their tree
            int[] rankArray = new int[numberOfNodes];
            for (int i = 0; i < numberOfNodes; i++)
            {
                rankArray[i] = 1;
            }

            int provinces = numberOfNodes;
            HashSet<string> visitedEdges = new HashSet<string>();

            for (int i = 0; i < numberOfNodes; i++)
            {
                for (int j = 0; j < numberOfNodes; j++)
                {
                    if (isConnected[i][j] == 1 && i != j && !visitedEdges.Contains(i + "_" + j) && !visitedEdges.Contains(j + "_" + i))
                    {
                        visitedEdges.Add(i + "_" + j);
                        visitedEdges.Add(j + "_" + i);

                        if (Union(i, j, parentArray, rankArray))
                        {
                            provinces--;
                        }
                    }
                }
            }

            return provinces;
        }

        //union of two connected sets
        public static bool Union(int node1, int node2, int[] parentArray, int[] rankArray)
        {
            int parent1 = FindParent(node1, parentArray);
            int parent2 = FindParent(node2, parentArray);

            //if parents are same that means, sets are already joined together
            //so they were not joined in this step, so retrun false
            if (parent1 == parent2) return false;

            //always merge smaller set into bigger set
            if (rankArray[parent2] > rankArray[parent1])
            {
                parentArray[parent1] = parent2;
                rankArray[parent2]++;
            }
            else
            {
                parentArray[parent2] = parent1;
                rankArray[parent1]++;
            }

            //sets were merged, so return true
            return true;

        }

        public static int FindParent(int node, int[] parentArray)
        {
            int parentNode = node;

            //while parentNode is not same as current node, keep finding parent
            while (parentNode != parentArray[parentNode])
            {
                //path compression technique
                //code works without path compression
                parentArray[parentNode] = parentArray[parentArray[parentNode]];


                parentNode = parentArray[parentNode];
            }

            return parentNode;

        }



        static void Main(string[] args)
        {
            Console.WriteLine("Number of Provinces");
           
            Console.WriteLine();
            var result3 = FindCircleNum(
                new int[][]
                {
                    new int[] {1,0,0,0,0,0,0,0,1,0,0,0,0,0,0},
                    new int[] {0,1,1,0,0,0,0,0,0,0,0,0,0,1,0},
                    new int[] {0,1,1,0,0,0,0,0,0,0,0,1,0,0,1},
                    new int[] {0,0,0,1,0,1,0,0,1,0,0,0,0,1,0},
                    new int[] {0,0,0,0,1,0,0,0,0,0,0,1,0,0,0},
                    new int[] {0,0,0,1,0,1,0,0,0,0,0,1,0,0,0},
                    new int[] {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0},
                    new int[] {0,0,0,0,0,0,0,1,0,0,0,0,0,0,0},
                    new int[] {1,0,0,1,0,0,0,0,1,1,1,0,0,1,0},
                    new int[] {0,0,0,0,0,0,0,0,1,1,0,1,1,0,0},
                    new int[] {0,0,0,0,0,0,0,0,1,0,1,1,0,0,0},
                    new int[] {0,0,1,0,1,1,0,0,0,1,1,1,0,0,0},
                    new int[] {0,0,0,0,0,0,0,0,0,1,0,0,1,0,1},
                    new int[] {0,1,0,1,0,0,0,0,1,0,0,0,0,1,0},
                    new int[] {0,0,1,0,0,0,0,0,0,0,0,0,1,0,1}

                }
            );
            Console.Write(result3);


            Console.ReadKey();

        }

    }
