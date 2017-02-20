using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTitanOne
{
    class Program
    {
        static string electionWinner(string[] votes)
        {

            string result = "";
            if(votes.Count() == 1)
            {
                result = votes[0];
                return result;
            }

            Dictionary<string, int> cands = new Dictionary<string, int>();

            foreach (string cand in votes)
            {
                if (cands.ContainsKey(cand))
                {
                    cands[cand] += 1; 
                }
                else
                {
                    cands.Add(cand, 1);
                }
            }

            var sortVotes = from pair in cands
                            orderby pair.Value descending,
                                    pair.Key descending
                            select pair;

            var first = sortVotes.First();
            result = first.Key;

            return result;


        }
        static void Main(string[] args)
        {
            string[] candidates = new string[10];
            candidates[0] = "Mike";
            candidates[1] = "Mili";
            candidates[2] = "Mike";
            candidates[3] = "Mili";
            candidates[4] = "Mike";
            candidates[5] = "Jay";
            candidates[6] = "Mili";
            candidates[7] = "Amy";
            candidates[8] = "Jay";
            candidates[9] = "Jay";

            Console.WriteLine("Winner is: " + electionWinner(candidates));
            //Console.WriteLine("This is test one");
            Console.ReadLine();
        }
    }

    
    //class ProgramTwo
    //{
    //    static void Main(string[] args)
    //    {
    //        Console.WriteLine("This is test two");
    //        Console.ReadLine();
    //    }
    //}
}
