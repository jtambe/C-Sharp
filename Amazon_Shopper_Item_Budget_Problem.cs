using System;
using System.Collections.Generic;

namespace Shopper_item_budget_problem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }


        // Given list of prices for pairOfJeans, pairOfShoes, Skirts, tops and dollarsas budget
        // find out how many options a customer has to purchase 1 of each product

        public static long NumberOfOptions(List<int> pairOfJeans, List<int> pairOfShoes, List<int> Skirts, List<int> tops, int dollars)
        {
            long numberOfOptions = 0;

            Dictionary<string, int> memoization = new Dictionary<string, int>();

            for (int a = 0; a < pairOfJeans.Count; a++)
            {
                for (int b = 0; b < pairOfShoes.Count; b++)
                {
                    for (int c = 0; c < Skirts.Count; c++)
                    {
                        for (int d = 0; d < tops.Count; d++)
                        {

                            if (memoization.TryGetValue("b" + b + "c" + c + "d" + d, out int suma))
                            {
                                if (pairOfJeans[a] + suma <= dollars)
                                {
                                    numberOfOptions++;
                                }
                            }

                            else if (memoization.TryGetValue("c" + c + "d" + d + "a" + a, out int sumb))
                            {
                                if (pairOfShoes[b] + sumb <= dollars)
                                {
                                    numberOfOptions++;
                                }
                            }

                            else if (memoization.TryGetValue("d" + d + "a" + a + "b" + b, out int sumc))
                            {
                                if (Skirts[c] + sumc <= dollars)
                                {
                                    numberOfOptions++;
                                }
                            }

                            else if (memoization.TryGetValue("a" + a + "b" + b + "c" + c, out int sumd))
                            {
                                if (tops[d] + sumd <= dollars)
                                {
                                    numberOfOptions++;
                                }
                            }
                            else if (pairOfJeans[a] + pairOfShoes[b] + Skirts[c] + tops[d] <= dollars)
                            {
                                memoization["b" + b + "c" + c + "d" + d] = pairOfShoes[b] + Skirts[c] + tops[d];
                                memoization["c" + c + "d" + d + "a" + a] = Skirts[c] + tops[d] + pairOfJeans[a];
                                memoization["d" + d + "a" + a + "b" + b] = tops[d] + pairOfJeans[a] + pairOfShoes[b];
                                memoization["a" + a + "b" + b + "c" + c] = pairOfJeans[a] + pairOfShoes[b] + Skirts[c];
                                numberOfOptions++;
                            }
                        }
                    }
                }
            }

            return numberOfOptions;

        }

    }
}
