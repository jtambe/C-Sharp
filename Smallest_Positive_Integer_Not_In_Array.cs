/*
Write a function:

class Solution { public int solution(int[] A); }

that, given an array A of N integers, returns the smallest positive integer (greater than 0) that does not occur in A.

For example, given A = [1, 3, 6, 4, 1, 2], the function should return 5.

Given A = [1, 2, 3], the function should return 4.

Given A = [−1, −3], the function should return 1.

Write an efficient algorithm for the following assumptions:

N is an integer within the range [1..100,000];
each element of array A is an integer within the range [−1,000,000..1,000,000].
*/



using System;

namespace Sort1
{

    public class Program
    {

        public static int solution(int[] nums)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)

            int answer = 1;
            Array.Sort(nums);

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] <= 0)
                {
                    // continue as long number is less than or equal to 0
                    continue;
                }
                else
                {
                    if ((nums[i] > 1 && i - 1 >= 0 && nums[i - 1] <= 0) || (nums[i] > 1 && i == 0))
                    {
                        //case 1. after 0 or negative numbers, series jumps 1 and goes to number > 1
                        //case 2. number series starts from number > 1
                        return answer;
                    }
                    else
                    {
                        if (i + 1 < nums.Length && (nums[i + 1] == nums[i] + 1 || nums[i + 1] == nums[i]))
                        {
                            // if there are more numbers and next == current or if next = current + 1
                            answer = nums[i] + 1;
                            continue;
                        }
                        else if (i + 1 < nums.Length && (nums[i + 1] != nums[i] + 1 && nums[i + 1] != nums[i]))
                        {
                            // if there are more numbers and next != current or next != current + 1
                            answer = nums[i] + 1;
                            break;
                        }
                        else if (i == nums.Length - 1)
                        {
                            // if the series continued in constant increment of 1 then finally result is last number + 1
                            answer = nums[i] + 1;
                            break;
                        }
                    }

                }
            }


            return answer;


        }



        static void Main(string[] args)
        {

            //int[] A = { 1, 3, 6, 4, 1, 2 };
            //int[] A = { 1, 2, 0 };
            //int[] A = { 7, 8, 9, 11, 12 };
            int[] A = { -1, -2, -60, 40, 43 };




            var result = solution(A);
            Console.WriteLine("Solution: " + result);
            Console.ReadKey();

        }
    }
}
