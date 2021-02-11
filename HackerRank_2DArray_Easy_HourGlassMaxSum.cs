using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

//https://www.hackerrank.com/challenges/2d-array/problem

class Solution {


    public class HourGlass
    {
        public int topRowSum {get; set;}
        public int middleInt {get; set;}
        public int bottomRowSum {get; set;}
        public int Sum 
        {
            get
            {
                return this.topRowSum + this.middleInt + this.bottomRowSum;
            }
        }
        public int Summation { get; set; }
    }

    static int hourglassSum2(int[][] arr)
        {

            int rows = arr.Length;
            int columns = arr[0].Length;
            int maxSum = int.MinValue;
            List<HourGlass> hourGlasses = new List<HourGlass>();
            for (int i = 0; i < rows-2; i++)
            {
                for (int j = 0; j < columns-2; j++)
                {
                    HourGlass hrgs = new HourGlass();
                    int sum = arr[i][j] + arr[i][j + 1] + arr[i][j + 2];
                    sum += arr[i+2][j] + arr[i+2][j + 1] + arr[i+2][j + 2];
                    sum += arr[i + 1][j + 1];
                    hrgs.Summation = sum;
                    hourGlasses.Add(hrgs);
                    maxSum = Math.Max(maxSum, hrgs.Summation);
                }
            }

            int x = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(hourGlasses[x].Summation + " ");
                    x++;
                }
                Console.WriteLine();
            }

            return maxSum;

        }

    // Complete the hourglassSum function below.
    static int hourglassSum(int[][] arr)
        {
            int maxSum = int.MinValue;
            List<HourGlass> hourGlasses = new List<HourGlass>();
            for (int i = 0, k = 2; i < 4 && k < 6; i++, k++)
            {
                int middleIndex = 1;
                int topRowSum = 0;
                int bottomRowSum = 0;
                for (int j = 0; j < 6; j++)
                {
                    HourGlass hrgs = new HourGlass();
                    if (j < 2)
                    {
                        topRowSum += arr[i][j];
                        bottomRowSum += arr[k][j];
                        continue;
                    }
                    else if( j == 2 )
                    {
                        hrgs = new HourGlass();
                        topRowSum += arr[i][j];
                        bottomRowSum += arr[k][j];

                        hrgs.topRowSum = topRowSum;
                        hrgs.bottomRowSum = bottomRowSum;
                        hrgs.middleInt = arr[i + 1][middleIndex];
                    }
                    else
                    {
                        middleIndex++;
                        hrgs = new HourGlass();
                        topRowSum = topRowSum + arr[i][j] - arr[i][j - 3];
                        bottomRowSum = bottomRowSum + arr[k][j] - arr[k][j - 3];

                        hrgs.topRowSum = topRowSum;
                        hrgs.bottomRowSum = bottomRowSum;
                        hrgs.middleInt = arr[i + 1][middleIndex];

                    }
                    hourGlasses.Add(hrgs);
                    maxSum = Math.Max(maxSum, hrgs.Sum);
                }
            }

            int x = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(hourGlasses[x].Sum + " ");
                    x++;
                }
                Console.WriteLine();
            }

            return maxSum;

        }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int[][] arr = new int[6][];

        for (int i = 0; i < 6; i++) {
            arr[i] = Array.ConvertAll(Console.ReadLine().Split(' '), arrTemp => Convert.ToInt32(arrTemp));
        }

        int result = hourglassSum(arr);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
