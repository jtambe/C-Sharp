//https://www.youtube.com/watch?v=ihj4IQGZ2zc
    // leetcode 105
    //Definition for a binary tree node.
    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
    

    public class GetTreefromPreOrderInOrder
    {

        public static TreeNode BuildTree(int[] preorder, int[] inorder)
        {


            List<int> preList = new List<int>();
            List<int> inList = new List<int>();

            foreach (var i in preorder)
            {
                preList.Add(i);
            }
            foreach (var i in inorder)
            {
                inList.Add(i);
            }
            // get the first of the root from PreOrder
            var root = new TreeNode(preList[0]);
            // find root's index in InOrder
            var mid = inList.IndexOf(root.val);

            // left subtree is built from
            // preOrder's 1st to length of left subtree which is till the root's index position in preorder
            // and Inorder's 0 to root index
            root.left = BuildSubtree(preList.GetRange(1, mid), inList.GetRange(0, mid));

            // right subtree is built from
            // preOrder's root's index position to end of preorder
            // and Inorder's root's index position to the end of inorder
            root.right = BuildSubtree(preList.GetRange(mid + 1, preList.Count - (mid + 1)), inList.GetRange(mid + 1, inList.Count - (mid + 1)));

            return root;

        }

        public static TreeNode BuildSubtree(List<int> preList, List<int> inList)
        {
            if (preList.Count == 0 || inList.Count == 0)
            {
                return null;
            }
            var root = new TreeNode(preList[0]);
            var mid = inList.IndexOf(root.val);
            // List.GetRange(1,0) is valid when asking for 0 elements starting from 1st index  where List only has one element at 0th index
            root.left = BuildSubtree(preList.GetRange(1, mid), inList.GetRange(0, mid));
            root.right = BuildSubtree(preList.GetRange(mid + 1, preList.Count - (mid + 1)), inList.GetRange(mid + 1, inList.Count - (mid + 1)));

            return root;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Get Tree using Preorder Inorder");
            
            Console.WriteLine();
            var result3 = BuildTree(
                new int[] { 3, 9, 20, 15, 7 },
                new int[] { 9, 3, 15, 20, 7 }
            );
            Console.Write(result3);


            Console.ReadKey();

        }

    }
