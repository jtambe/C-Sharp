using System;
using System.Collections.Generic;
using System.Text;

namespace DFS_NonRecursive
{

    //https://leetcode.com/problems/letter-combinations-of-a-phone-number/
    /*
        Constraints:
        0 <= digits.length <= 4
        digits[i] is a digit in the range ['2', '9'].
    */

    public class PhoneNumbersToChars
    {
        public static IList<string> LetterCombinations(string digits)
        {
            IList<string> result = new List<string>();
            // return if empty
            if (string.IsNullOrEmpty(digits))
            {
                return result;
            }
            // return if invalid input
            foreach (char c in digits)
            {
                if ((int)c > (int)'9' || (int)c < (int)'2')
                {
                    return result;
                }

            }
            // set up dictionary 
            Dictionary<char, string> digitCharsMapping = new Dictionary<char, string>();
            digitCharsMapping.Add('2', "abc");
            digitCharsMapping.Add('3', "def");
            digitCharsMapping.Add('4', "ghi");
            digitCharsMapping.Add('5', "jkl");
            digitCharsMapping.Add('6', "mno");
            digitCharsMapping.Add('7', "pqrs");
            digitCharsMapping.Add('8', "tuv");
            digitCharsMapping.Add('9', "wxyz");

            // hashset is used to detect if a character is already visted
            HashSet<string> visited = new HashSet<string>();

            // call stack is used to create string to add in result set
            Stack<char> callStack = new Stack<char>();

            for (int i = 0; i < digits.Length; i++)
            {
                foreach (char c in digitCharsMapping[digits[i]])
                {
                    if (!visited.Contains(c+ "_"+ i))
                    {
                        // hashset adds c+_+i because, input can be "222" and we can determine which level(i) of a character from 2 is visited
                        visited.Add(c + "_" + i);
                        callStack.Push(c);

                        if (i == digits.Length -1)
                        {
                            // create string for result
                            var charArray = callStack.ToArray();
                            StringBuilder str = new StringBuilder();
                            for (int l = charArray.Length - 1; l >= 0; l--)
                            {
                                str.Append(charArray[l]);
                            }
                            result.Add(str.ToString());
                            callStack.Pop();

                            // go back one step in digits
                            --i;
                        }
                        // once entry is added, break to go back immediately
                        break;
                    }

                    // check if visited contains all the chars for number
                    bool visitedAllCharsForNumber = true;
                    foreach (char s in digitCharsMapping[digits[i]])
                    {
                        if (!visited.Contains(s + "_" + i))
                        {
                            visitedAllCharsForNumber = false;
                        }

                    }
                    // if it does that means last level is printed, remove those chars for this iteration
                    // but if it is at level 0 then it means all the combinatations are added in result and now it is time to exit
                    if(visitedAllCharsForNumber && i == 0)
                    {
                        return result;
                    }

                    if (visitedAllCharsForNumber)
                    {
                        foreach (char s in digitCharsMapping[digits[i]])
                        {
                            visited.Remove(s + "_" + i);
                        }
                        // once you remove all nodes at a level from visited,
                        // you need to go back to previous level.
                        // here we are doing i = i-2 because the for loop will make it i++ and that will be i-1 for next iteration.
                        i = i - 2;
                        // get the last element out from previous level, so that new branch of tree can be printed
                        callStack.Pop();
                        // break foreach loop so that you can go back to outer for loop to start from i-1 level
                        break;
                    }
                }
            }
            return result;

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Letter Combinations");

            //var result = LetterCombinations("23");
            //var result = LetterCombinations("234");
            //var result = LetterCombinations("2345");
            var result = LetterCombinations("22");
            foreach (var str in result)
            {
                Console.WriteLine(str);
            }

            Console.ReadKey();

        }

    }
}
