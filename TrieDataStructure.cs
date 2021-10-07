namespace DFS_NonRecursive
{
    public class TrieNode
    {
        public char val;
        public HashSet<char> childrenChars = new HashSet<char>();
        public List<TrieNode> childrenNodes = new List<TrieNode>();
        public bool isWord = false;
    }
    public class Trie
    {
        //https://leetcode.com/problems/implement-trie-prefix-tree/
        // leetcode 208
        
        TrieNode root;
        public Trie()
        {
            root = new TrieNode();
            root.val = '*';
        }

        public void Insert(string word)
        {
            var currentTrieNode = root;
            int charCounter = 0;
            while(charCounter < word.Length)
            {
                // while current node has required char, keep using that char
                while (currentTrieNode.childrenChars.Contains(word[charCounter]))
                {
                    currentTrieNode = currentTrieNode.childrenNodes.FirstOrDefault(x => x.val == word[charCounter]);
                    if (charCounter == word.Length - 1)
                    {
                        currentTrieNode.isWord = true;
                        break;
                    }
                    charCounter++;
                }

                // if it doesn't then create new node for non existing char in the chain
                var newTrieNode = new TrieNode();
                newTrieNode.val = word[charCounter];
                currentTrieNode.childrenNodes.Add(newTrieNode);
                currentTrieNode.childrenChars.Add(word[charCounter]);
                if (charCounter == word.Length - 1)
                {
                    newTrieNode.isWord = true;
                    break;
                }
                else
                {
                    charCounter++;
                    currentTrieNode = newTrieNode;
                }
            }

        }

        public bool Search(string word)
        {
            var currentTrieNode = root;
            int charCounter = 0;
            while (charCounter < word.Length && currentTrieNode.childrenChars.Contains(word[charCounter]))
            {
                currentTrieNode = currentTrieNode.childrenNodes.FirstOrDefault(x => x.val == word[charCounter]);
                charCounter++;
            }
            // previous charcounter++ increments for last exsisting count, needs to be reversed
            // applee is shown as valid word otherwise
            charCounter--;
            if (charCounter < word.Length -1)
            {
                return false;
            }
            return currentTrieNode.isWord;
            
        }

        public bool StartsWith(string prefix)
        {
            var currentTrieNode = root;
            int charCounter = 0;
            while (charCounter < prefix.Length && currentTrieNode.childrenChars.Contains(prefix[charCounter]))
            {
                currentTrieNode = currentTrieNode.childrenNodes.FirstOrDefault(x => x.val == prefix[charCounter]);
                charCounter++;
            }
            charCounter--;
            if (charCounter < prefix.Length - 1)
            {
                return false;
            }

            return true;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Prefix Tree,Trie data structure");

            Console.WriteLine();
            Trie obj = new Trie();
            obj.Insert("apple");
            bool param = false;
            param = obj.Search("apple");
            param = obj.Search("applee");
            param = obj.Search("app");
            param = obj.StartsWith("app");
            obj.Insert("app");
            param = obj.Search("app");


            Console.ReadKey();

        }

    }
}
