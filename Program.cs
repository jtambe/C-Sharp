using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace SingletonParseFiles
{


    public class Singleton
    {

        static string path = @"C:\Users\tambe\Desktop\Jayesh\Programming\CSharp\singleton";
        private static Singleton _instance = null;
        private static readonly object padlock = new object(); // Lock synchronization object

        Singleton() //constructor
        { }

        public static Singleton Instance()
        {
            // Support multithreaded applications through
            // 'Double checked locking' pattern which (once
            // the instance exists) avoids locking each
            // time the method is invoked
            if (_instance == null)
            {
                lock (padlock)
                {
                    if(_instance == null)
                    {
                        _instance = new Singleton();
                    }                    
                }
            }
            return _instance;
        }

        public int ReadFiles(string fileName)
        {
            try
            {
                string currentfileData = File.ReadAllText(fileName);
                Console.WriteLine(currentfileData);
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public void FindFiles()
        {
            try
            {
                if (Directory.Exists(path))
                {
                    string[] allFilesList = Directory.GetFiles(path);
                    foreach (string fileName in allFilesList)
                    {
                        Thread thread = new Thread(() => {
                            int success = ReadFiles(fileName);
                        });
                        thread.Start();
                        thread.Join();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }

    class Program
    {
        
        static void Main(string[] args)
        {          
            Singleton s1 = Singleton.Instance();
            s1.FindFiles();
            Console.ReadKey();
        }
    }
}
