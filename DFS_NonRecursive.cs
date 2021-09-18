using System;
using System.Collections.Generic;
using System.Linq;

namespace DFS_NonRecursive
{

    public class Node
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool visited { get; set; }
        public List<Node> connectedNodes;
        public Node(int id, string name, List<Node> connectedNodes = null)
        {
            this.id = id;
            this.name = name;
            if (connectedNodes != null && connectedNodes.Any())
            { this.connectedNodes = new List<Node>(connectedNodes); }
        }

    }


    public class Graph
    {
        public List<Node> Nodes;
        public Graph(List<Node> Nodes)
        {
            this.Nodes = Nodes;
        }
        public void DFS_NonRecusrive(Graph graph)
        {
            // edge case for graph input
            if (graph == null || graph.Nodes == null || !graph.Nodes.Any())
            {
                return;
            }

            // create a stack for recursive calls of DFS recursive algorithm
            Stack<Node> callStack = new Stack<Node>();
            // set first Node as visited and push it into stack
            graph.Nodes[0].visited = true;
            callStack.Push(graph.Nodes[0]);

            // whle the callstack is not emptied out, perform following steps
            while (callStack.Any())
            {
                // get the top node from callstack
                Node topNode = callStack.Peek();

                // if top node has no children
                // or all the children/connected nodes of top node are already visited
                // print the topNode
                if ( topNode.connectedNodes == null || 
                    !topNode.connectedNodes.Where(x => x.visited == false).Any() )
                {
                    Console.WriteLine($"{topNode.id} : {topNode.name}");
                    //once node is printed, pop the node
                    callStack.Pop();
                    continue;
                }

                // get the next unvisited node from topNode's children/connectedNodes
                foreach (var connectedNode in topNode.connectedNodes.Where(x => x.visited == false))
                {
                    // mark unvisited node as visited
                    connectedNode.visited = true;
                    // push that node in callstack
                    callStack.Push(connectedNode);
                    break;
                }

            }
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Non Recusrive DFS code!");

            // Set up for Nodes and Graph
            Node A = new Node(1, "A");

            // B C D are connected to A
            List<Node> BList = new List<Node>();
            BList.Add(A);
            Node B = new Node(2, "B", BList);
            Node C = new Node(3, "C", BList);
            Node D = new Node(4, "D", BList);

            // A is connected to B C D
            A.connectedNodes = new List<Node>() { B, C, D };

            // B is connected to E
            List<Node> EList = new List<Node>();
            EList.Add(B);
            Node E = new Node(5, "E", EList);
            // E is connected to B
            B.connectedNodes.Add(E);

            // All nodes form a Graph
            Graph graph = new Graph(new List<Node>() { A, B, C, D, E });

            graph.DFS_NonRecusrive(graph);

            Console.ReadKey();

        }
    }
}
