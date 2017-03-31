using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestPalindromicSubsequence
{
    class Program
    {


        public int LPSDynamic(string str)
        {
            int n = str.Length;
            int[,] T = new int[n,n];

            for(int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if(i == j)
                    {
                        T[i, j] = 1;
                    }
                    else
                    {
                        T[i, j] = 0;
                    }                    
                }
            }

            for (int x = 2; x < n+1; ++x)
            {
                for (int i = 0; i < n-x+1; ++i)
                {
                    int j = i + x - 1;

                    if (str[i] == str[j] && x == 2)
                    {
                        T[i, j] = 2;
                    }
                    else if(str[i] == str[j])
                    {
                        T[i, j] = T[i + 1, j - 1] + 2;
                    }
                    else
                    {
                        T[i, j] = Math.Max(T[i,j-1], T[i+1,j]);
                    }
                }
            }

            return T[0, n - 1];

        }


        public int LPSRecur(string str, int start, int length)
        {
            if(length == 1)
            {
                return 1;
            }
            if (length == 0)
            {
                return 0;
            }
            if(str[start] == str[start+length -1])
            {
                return 2 + LPSRecur(str, start + 1, length - 2);
            }
            else
            {
                return Math.Max(LPSRecur(str, start, length - 1), LPSRecur(str, start + 1, length - 1));
            }
        }


        static void Main(string[] args)
        {
            Program obj = new Program();
            string str = "GEEKFORGEEKS";
            Console.WriteLine(obj.LPSRecur(str, 0, str.Length));
            Console.WriteLine(obj.LPSDynamic(str));
            Console.ReadKey();
        }
    }
}
