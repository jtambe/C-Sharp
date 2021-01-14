using System;
using System.Collections.Generic;
using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Shopper_item_budget_problem
{
    class Program
    {
        // Given list of prices for pairOfJeans, pairOfShoes, Skirts, tops and dollarsas budget
        // find out how many options a customer has to purchase 1 of each product

        /*
         * 1 <= a,b,c,d <= 10^3
         * 1 <= dollars <= 10^9
         * 1 <= price of each item <= 10^9
         * 
         * a,b,c,d are sizes of four price arrays
         */

        public class Shopper
        {
            public List<int> pairOfJeans;
            public List<int> pairOfShoes;
            public List<int> tops;
            public List<int> skirts;
            public int dollarsBudget;
        }

        public static Shopper GenerateData()
        {
            Shopper shopper = new Shopper();
            int thousand = (int)Math.Pow(10, 3);
            int billion = (int)Math.Pow(10, 9);
            Random rnd = new Random();
            int araySize = rnd.Next(900, thousand + 1);
            int dollarsBudget = rnd.Next(50000000, billion + 1);

            //pairOfJeans
            List<int> pairOfJeans = new List<int>();
            for (int i = 0; i < araySize; i++)
            {
                pairOfJeans.Add(rnd.Next(50000000, billion + 1));
            }

            araySize = rnd.Next(900, thousand + 1);
            //pairOfShoes
            List<int> pairOfShoes = new List<int>();
            for (int i = 0; i < araySize; i++)
            {
                pairOfShoes.Add(rnd.Next(50000000, billion + 1));
            }

            araySize = rnd.Next(900, thousand + 1);
            //tops
            List<int> tops = new List<int>();
            for (int i = 0; i < araySize; i++)
            {
                tops.Add(rnd.Next(50000000, billion + 1));
            }

            araySize = rnd.Next(900, thousand + 1);
            //skirts
            List<int> Skirts = new List<int>();
            for (int i = 0; i < araySize; i++)
            {
                Skirts.Add(rnd.Next(50000000, billion + 1));
            }


            shopper.dollarsBudget = dollarsBudget;
            shopper.pairOfJeans = pairOfJeans;
            shopper.pairOfShoes = pairOfShoes;
            shopper.tops = tops;
            shopper.skirts = Skirts;

            return shopper;

        }


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            
            long answer;

            for (int i = 1; i <= 100; i++)
            {

                Stopwatch stopwatch = new Stopwatch();

                // Begin timing.
                stopwatch.Start();

                // Do something.
                Console.WriteLine("Attempt No. " + i);
                Shopper shopper = GenerateData();
                answer = NumberOfOptionsMemo1(shopper.pairOfJeans, shopper.pairOfShoes, shopper.tops, shopper.skirts, shopper.dollarsBudget);
                Console.WriteLine("NumberOfOptionsMemo1: " + answer);

                // Stop timing.
                stopwatch.Stop();

                // Write result.
                Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                
            }
            

            //answer = NumberOfOptions(shopper.pairOfJeans, shopper.pairOfShoes, shopper.tops, shopper.skirts, shopper.dollarsBudget);
            //Console.WriteLine("NumberOfOptions: " + answer);

            //var summary = BenchmarkRunner.Run<Shopper>();

            Console.ReadKey();

        }

        public static long NumberOfOptions(List<int> pairOfJeans, List<int> pairOfShoes, List<int> tops, List<int> Skirts, int dollars)
        {
            long numberOfOptions = 0;

            for (int a = 0; a < pairOfJeans.Count; a++)
            {
                for (int b = 0; b < pairOfShoes.Count; b++)
                {
                    for (int c = 0; c < Skirts.Count; c++)
                    {
                        for (int d = 0; d < tops.Count; d++)
                        {
                            if (pairOfJeans[a] + pairOfShoes[b] + Skirts[c] + tops[d] <= dollars)
                            {
                                numberOfOptions++;
                            }
                        }
                    }
                }
            }

            return numberOfOptions;
        }

        //[Benchmark]
        public static long NumberOfOptionsMemo1(List<int> pairOfJeans, List<int> pairOfShoes,  List<int> tops, List<int> Skirts, int dollars)
        {
            long numberOfOptions = 0;

            Dictionary<string, int> memoizationCD = new Dictionary<string, int>();
            Dictionary<string, int> memoizationAB = new Dictionary<string, int>();

            // Get rid of all values greater than or equal to dollar amount for just one item
            // which means if skirt costs as much as budget then shopper cannot buy other items so get rid of that price for skirt
            int cutAt = 0;
            int numberOfItemsRemove = 0;
            tops.Sort();
            Skirts.Sort();
            pairOfShoes.Sort();
            pairOfJeans.Sort();

            for (int i = 0; i < tops.Count; i++)
            {
                if (tops[i] >= dollars)
                {
                    cutAt = i;
                    numberOfItemsRemove = tops.Count - (i + 1);
                    break;
                }
            }
            tops.RemoveRange(cutAt + 1, numberOfItemsRemove);

            for (int i = 0; i < Skirts.Count; i++)
            {
                if (Skirts[i] >= dollars)
                {
                    cutAt = i;
                    numberOfItemsRemove = Skirts.Count - (i + 1);
                    break;
                }
            }
            Skirts.RemoveRange(cutAt + 1, numberOfItemsRemove);

            for (int i = 0; i < pairOfShoes.Count; i++)
            {
                if (pairOfShoes[i] >= dollars)
                {
                    cutAt = i;
                    numberOfItemsRemove = pairOfShoes.Count - (i + 1);
                    break;
                }
            }
            pairOfShoes.RemoveRange(cutAt + 1, numberOfItemsRemove);

            for (int i = 0; i < pairOfJeans.Count; i++)
            {
                if (pairOfJeans[i] >= dollars)
                {
                    cutAt = i;
                    numberOfItemsRemove = pairOfJeans.Count - (i + 1);
                    break;
                }
            }
            pairOfJeans.RemoveRange(cutAt + 1, numberOfItemsRemove);

            
            // use memoization dictionary and only send data for prices that are less than budget
            for (int c = 0; c < Skirts.Count; c++)
            {
                for (int d = 0; d < tops.Count; d++)
                {
                    if (Skirts[c] + tops[d] < dollars)
                    {
                        memoizationCD["c" + c + "d" + d] = Skirts[c] + tops[d];
                    }
                    else
                    {
                        break;
                    }
                }
            }


            // use memoization dictionary and only send data for prices that are less than budget
            for (int a = 0; a < pairOfJeans.Count; a++)
            {
                for (int b = 0; b < pairOfShoes.Count; b++)
                {
                    if (pairOfJeans[a] + pairOfShoes[b] < dollars)
                    {
                        memoizationAB["a" + a + "b" + b] = pairOfJeans[a] + pairOfShoes[b];
                    }
                    else
                    {
                        break;
                    }
                }
            }

            Console.WriteLine("memoizationCD Count: " + memoizationCD.Count);
            Console.WriteLine("memoizationAB Count: " + memoizationAB.Count);
            foreach (var outerPair in memoizationAB)
            {
                foreach (var innerPair in memoizationCD)
                {
                    if (innerPair.Value + outerPair.Value <= dollars)
                    {
                        numberOfOptions++;
                    }
                    else 
                    {
                        break;
                    }
                }
            }

            return numberOfOptions;

        }

    }
}
