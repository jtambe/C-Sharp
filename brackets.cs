using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Collections;
class Solution {

    
    public static bool IsValidBracket(string s){
        
        //bool result = false;
        if(s.Length == 1){
            return false;
        }
        
        Stack<char> brackets = new Stack<char>();        
            foreach(char ch in s){
        
                if(ch == '{' || ch == '[' || ch == '('){
                    brackets.Push(ch);
                }
                
                if(ch == '}'){
                    if(brackets.Count == 0){
                        return false;
                    }
                    if(brackets.Peek() == '{'){                        
                        brackets.Pop();
                    }            
                    else{
                        return false;
                    }                    
                }
                if(ch == ']'){
                    if(brackets.Count == 0){
                        return false;
                    }                    
                    if(brackets.Peek() == '['){                        
                        brackets.Pop();
                    }            
                    else{
                        return false;
                    }                    
                }
                if(ch == ')'){
                    if(brackets.Count == 0){
                        return false;
                    }                    
                    if(brackets.Peek() == '('){                        
                        brackets.Pop();
                    }            
                    else{
                        return false;
                    }                    
                }
            }
        
        if(brackets.Count > 0){
            return false;
        }
        else{
            return true;
        }
    }
 
    static void Main(String[] args) {
        int t = Convert.ToInt32(Console.ReadLine());
        for(int a0 = 0; a0 < t; a0++){
            string s = Console.ReadLine();
            if(IsValidBracket(s)){
                Console.WriteLine("YES");
            }
            else{
                Console.WriteLine("NO");
            }
        }
    }
}


