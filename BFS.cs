using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://www.ics.uci.edu/~eppstein/161/960208.html#topo

namespace Graph
{
    class Program
    {
        public class Node:ICloneable
        {
            public int value;
            public int distance;
            public List<Node> next = new List<Node>();
            public Node(int val)
            {
                value = val;
            }           
            //copy constructor to create new Node with same members
            //This did not copy list associated with next member
            public Node(Node otherNode)
            {
                value = otherNode.value;                
                distance = otherNode.distance;
                next = otherNode.next;
            }
            //Since copy constrctor did not work in deep copying list assocated with next member
            //I tried cloning object using IClonable
            //However, even cloning did not copy list associated with next member
            // Please note that Clone method only retuns object type
            // I tried returning Node type using this method and code did not compile.
            public object Clone()
            {
                Node result = new Node(this.value);
                result.distance = this.distance;
                result.next = this.next;
                return result;
            }

        }
        public class Graph
        {
            public List<Node> adjList = new List<Node>();
            public void printGraph()
            {
                foreach (Node i in adjList)
                {
                    Console.Write("Node value " + i.value + " is connected to nodes : ");
                    foreach (Node j in i.next)
                    {
                        Console.Write(j.value + " ");
                    }
                    Console.WriteLine();
                }
            }        
            public void BFS(Node start, List<Node> adjList)
            {
                Console.WriteLine("Printing BFS");                
                foreach(Node n in adjList)
                {
                    // initializing all distance members in adjList as Max
                    n.distance = int.MaxValue;
                    foreach (Node nList in n.next)
                    {
                        // initializing all distance members in adjList as Max
                        nList.distance = int.MaxValue;
                    }
                }
                
                //Creating a queue to hold all node values 
                Queue<Node> bfsQueue = new Queue<Node>();

                // Distance for start node in BFS travel is initialized as zero
                adjList.Find(x => x.value == start.value).distance = 0;
                
                //Enqueue start node in Queue
                bfsQueue.Enqueue(adjList.Find(x => x.value == start.value));

                while (bfsQueue.Count != 0)
                {
                    Console.WriteLine("Node: " + bfsQueue.Peek().value + " level: " + bfsQueue.Peek().distance);

                    // Syntax to use Clone method for cloning objet
                    Node n = bfsQueue.Dequeue().Clone() as Node; 
                    
                    // Since deep copy of adjlist had failed, I used lamda expressions to find Nodes in adjList
                    // I have values of Nodes in adjList.                   
                    // Modifying values of nList or using values of n would result in erronous code
                    foreach(Node nList in adjList.Find(x => x.value == n.value).next)
                    { 
                        // for each node in adjList, we check its neighbouring nodes and enqueue them in Queue
                        if(adjList.Find(x => x.value == nList.value).distance == int.MaxValue)
                        {
                            // Since we modify the distance of each Node to be enqueued
                            // Same nodes will not be repeated in Queue
                            adjList.Find(x => x.value == nList.value).distance = adjList.Find(x => x.value == n.value).distance + 1;                            
                            bfsQueue.Enqueue(adjList.Find(x => x.value == nList.value));                            
                        }
                    }                                     
                }

            }
        }
        static void Main(string[] args)
        {
            try
            {
                Graph graphObj = new Graph();
                var fileStream = new FileStream(@"C:\Jayesh\Graph1.txt", FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line;
                    int i = 1;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        Node nodeObj = new Node(i);
                        graphObj.adjList.Add(nodeObj);
                        string[] listNodes = line.Split(' ');
                        foreach (string str in listNodes)
                        {
                            int connectedNode = int.Parse(str);
                            graphObj.adjList.Last().next.Add(new Node(connectedNode));
                        }
                        i++;
                    }
                }

                graphObj.printGraph();
                graphObj.BFS(graphObj.adjList[2], graphObj.adjList);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }

        }    
    }
}
