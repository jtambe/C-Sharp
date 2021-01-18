using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shopper_item_budget_problem
{
    class Two_Sum
    {
        public class myStruct
        {
            public int Index;
            public int Val;
        }

        public static int[] TwoSum(int[] nums, int target)
        {

            List<myStruct> numsList = new List<myStruct>();
            for (int a = 0; a < nums.Length; a++)
            {
                numsList.Add(new myStruct { Index = a, Val = nums[a] });
            }

            int[] result = new int[2];
            var numsListOrderd = numsList.OrderBy(x => x.Val).ToList();

            int i = 0; int j = nums.Length - 1;

            while (i != j)
            {
                if (numsListOrderd[i].Val + numsListOrderd[j].Val == target)
                {
                    result[0] = numsListOrderd[i].Index;
                    result[1] = numsListOrderd[j].Index;
                    break;
                }
                else if (numsListOrderd[i].Val + numsListOrderd[j].Val < target)
                {
                    i++;
                }
                else if (numsListOrderd[i].Val + numsListOrderd[j].Val > target)
                {
                    j--;
                }

            }

            return result;

        }

        static void Main()
        {
            System.Console.WriteLine("Hello World!");

            int[] input = new int[3] { 3, 2, 4 };
            int target = 6;

            int[] result = TwoSum(input, target);
            Console.Write("{ ");
            foreach (var item in result)
            {
                Console.Write(item + " ");
            }
            Console.Write("}");

            Console.ReadKey();

        }



    }
}
