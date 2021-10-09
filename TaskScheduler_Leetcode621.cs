
/*
 * LeetCode 621 - Task scheduler
 * Given a characters array tasks, representing the tasks a CPU needs to do, 
where each letter represents a different task. Tasks could be done in any order. 
Each task is done in one unit of time. For each unit of time, the CPU could complete either one task or just be idle.
However, there is a non-negative integer n that represents the cooldown period between two *same tasks* (the same letter in the array), 
that is that there must be at least n units of time between any two same tasks.
Return the least number of units of times that the CPU will take to finish all the given tasks.
 
*Example 1:*

*Input:* tasks = ["A","A","A","B","B","B"], n = 2
*Output:* 8

*Explanation:* 
A -> B -> idle -> A -> B -> idle -> A -> B

There is at least 2 units of time between any two same tasks.

*Example 2:*

*Input:* tasks = ["A","A","A","B","B","B"], n = 0
*Output:* 6

*Explanation:* On this case any permutation of size 6 would work since n = 0.
["A","A","A","B","B","B"]
["A","B","A","B","A","B"]
["B","B","B","A","A","A"]
...
And so on.

*Example 3:*

*Input:* tasks = ["A","A","A","A","A","A","B","C","D","E","F","G"], n = 2
*Output:* 16

*Explanation:* 
One possible solution is
A -> B -> C -> A -> D -> E -> A -> F -> G -> A -> idle -> idle -> A -> idle -> idle -> A
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DFS_NonRecursive
{
    public class myTask
    {
        public string name;
        public int frequency;
    }
    public class AmazonTaskScheduler
    {

        public static int TaskScheduler(string[] tasks, int n)
        {
            if (n == 0 || tasks.Length == 0)
            {
                return tasks.Length;
            }

            int taskCompletedCount = 0;
            List<string> tasksCovered = new List<string>();
            Dictionary<string, int> charFrequency = new Dictionary<string, int>(); // maintain chars and the frequency
            Dictionary<string, int> addTasksForIdle = new Dictionary<string, int>(); // maintain chars and the required idle time before adding the char back in charFrequency
            Dictionary<string, int> charFreAux = new Dictionary<string, int>(); // maintain chars and the frequency on the side while char was removed (set to -1) in charFrequency

            foreach (var task in tasks)
            {
                if (charFrequency.TryGetValue(task, out var fre))
                {
                    charFrequency[task] = fre + 1;
                }
                else
                {
                    charFrequency.Add(task, 1);
                }

                addTasksForIdle[task] = -1;
                charFreAux[task] = -1;
            }

            myTask removeTask = new myTask();

            // if I can use c# PriorityQueue from C# 10.0 over here it would be helpful to reduce this sorting of O(n log n)
            charFrequency = charFrequency.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value); 

            var pair = charFrequency.FirstOrDefault(x => x.Value > 0);
            removeTask.name = pair.Key;
            removeTask.frequency = pair.Value;
            removeTask.frequency--;
            
            charFrequency[removeTask.name] = -1; // marking it as -1, making it as good as removed for this problem. Since modifying collections gives runtime exception when running loop

            tasksCovered.Add(removeTask.name);

            charFreAux[removeTask.name] = removeTask.frequency; // keep the removed task data here before adding it back on after timer for that task is over

            addTasksForIdle[removeTask.name] = n-1;

            TaskSchedulerUtil(tasks, tasksCovered, charFrequency, addTasksForIdle, charFreAux, ++taskCompletedCount, n);
            return tasksCovered.Count();
        }

        public static void TaskSchedulerUtil(string[] tasks, List<string> tasksCovered, 
            Dictionary<string, int> charFrequency, Dictionary<string, int> addTasksForIdle, Dictionary<string, int> charFreAux,
            int taskCompletedCount, int n)
        {
            // keep doing this until all tasks ae completed
            if (taskCompletedCount == tasks.Length)
            {
                return;
            }

            myTask removeTask = new myTask();
            // if I can use c# PriorityQueue from C# 10.0 over here it would be helpful to reduce this sorting of O(n log n)
            charFrequency = charFrequency.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            var pair = charFrequency.FirstOrDefault(x => x.Value > 0);
            if (pair.Key == null)
            {
                tasksCovered.Add("idle");
            }
            else
            {
                removeTask.name = pair.Key;
                removeTask.frequency = pair.Value;
                removeTask.frequency--;

                charFrequency[removeTask.name] = -1; // marking it as -1, making it as good as removed for this problem. Since modifying collections gives runtime exception when running loop

                tasksCovered.Add(removeTask.name);

                charFreAux[removeTask.name] = removeTask.frequency; // keep the removed task data here before adding it back on after timer for that task is over

                taskCompletedCount++;
            }

            // for every task that was removed for main dictionary/queue keep the timer decreasing, so they can be added when timer is zero
            foreach (var key in addTasksForIdle.Keys.ToList())
            {
                if (addTasksForIdle[key] == 0)
                {
                    charFrequency[key] = charFreAux[key];
                }
                addTasksForIdle[key] = addTasksForIdle[key] - 1;
                
            }

            // if there was a task done (not "idle") then removed task needs to be added in the timer/idle dictionary counter
            if (removeTask.name != null)
            {
                addTasksForIdle[removeTask.name] = n-1;
            }

            TaskSchedulerUtil(tasks, tasksCovered, charFrequency, addTasksForIdle, charFreAux, taskCompletedCount, n);
        }



        static void Main(string[] args)
        {

            Console.WriteLine("Task Scheduler");

            Console.WriteLine();
            string[] input = { "A", "A", "A", "B", "B", "B" };
            var result = TaskScheduler(input, 2);
            Console.WriteLine(result);

            string[] input2 = { "A", "A", "A", "B", "B", "B" };
            result = TaskScheduler(input2, 0);
            Console.WriteLine(result);

            string[] input3 = { "A", "A", "A", "A", "A", "A", "B", "C", "D", "E", "F", "G" };
            result = TaskScheduler(input3, 2);
            Console.WriteLine(result);

            string[] input4 = { "A", "A", "A", "B", "B", "B", "C", "C", "C", "D", "D", "E" };
            // A B C A B C D A B C D E
            result = TaskScheduler(input4, 2);
            Console.WriteLine(result);

            Console.ReadKey();
        }

    }
}
