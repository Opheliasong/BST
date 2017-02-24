using System;

namespace BiTree
{
    class Node
    {
        public bool init {
            get{
                if(parent == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool isLeaf {
            get {
                if(children[0] == null && children[1] == null)
                {
                    return true;
                }
                return false;
            }
        }
        public int value;
        public Node parent = null;
        public Node[] children = new Node[2];
        public Node() {}
        public Node(int val)
        {
            value = val;
        }


    }

    class BSTree
    {
        public Node root = null;
        public void Insert(int val)
        {
            if(root == null)
            {
                root = new Node(val);
            }
            else
            {
                InjectNode(ref root, val);
            }
        }

        void InjectNode(ref Node node, int val)
        {
            if(node == null)
            {
                node = new Node(val);
                return;
            }

            if(node.value >= val)
            {
                InjectNode(ref node.children[0], val);
                node.children[0].parent = node;
            }
            else
            {
                InjectNode(ref node.children[1], val);
                node.children[1].parent = node;
            }
        }

        public Node Search(int val)
        {
            if(root == null)
            {
                Console.Write("Tree is empty");
                return null;
            }

            return Search(ref root, val);
        }

        Node Search(ref Node node, int val)
        {
            if(node == null)
            { 
                return null;
            }

            if(node.value == val)
            {
                return node;
            }
            else if(node.value > val)
            {
                if(node.children[0] == null)
                {
                    return null;
                }
                return Search(ref node.children[0], val);
            }
            else
            {
                if(node.children[1] == null)
                {
                    return null;
                }
                return Search(ref node.children[1], val);
            }
        }

        public void Remove(int val)
        {
            Remove(ref root, val);
        }

        void Remove(ref Node node, int val)
        {
            if(node == null)
            {
                return;
            }

            bool find = false;
            while(!find)
            {
                if(node == null)
                {
                    return;
                }
                else if(node.value > val)
                {
                    node = node.children[0];
                }
                else if(node.value < val)
                {
                    node = node.children[1];
                }
                else if(node.value == val)
                {
                    find = true;
                }
            }

            if(node.children[0] != null && node.children[1] != null)
            {
                //has two children has node

                // 1. find largest leftSubtree node or smallest rightSubtree
                Node rhs = FindLargestChildNode(ref node.children[0], node.children[0].value);
                // 2. swap the largest node and find node
                Swap(ref node, ref rhs);
                Remove(ref rhs, rhs.value);
            }
            else if(node.children[0] != null || node.children[1] != null)
            {
                //has one child has node
                int idx = 0;
                if(node.children[1] != null)
                {
                    idx = 1;
                }

                if(node.parent.children[0] == node)
                {
                    node.parent.children[0] = node.children[idx];
                }
                else
                {
                    node.parent.children[1] = node.children[idx];
                }
                node.children[idx].parent = node.parent;

                //temp remove
                node = null;
            }
            else
            {
                //this node have no one child
                if(node.parent.children[0] == node)
                {
                    node.parent.children[0] = null;
                }
                else
                {
                    node.parent.children[1] = null;
                }
            }
        }

        Node FindLargestChildNode(ref Node node, int val)
        {
            if(node.children[1] == null)
            {
                return node;
            }
            else if(node.children[1].value > val)
            {
                return FindLargestChildNode(ref node.children[1], node.children[1].value);
            }
            else
            {
                return node.children[1];
            }
        }

        void Swap(ref Node lhs, ref Node rhs)
        {
            Node temp = rhs;
            Node old_rhs_l_child = rhs.children[0];
            Node old_rhs_r_child = rhs.children[1];
            Node old_rhs_parent = rhs.parent;

            Node old_lhs_l_child = lhs.children[0];
            Node old_lhs_r_child = lhs.children[1];
            Node old_lhs_parent = lhs.parent;
            int rhsIdx = 0;

            //Do parent to rhs's edges break
            if(rhs.parent != null)
            {
                if(rhs.parent.children[0] == rhs)
                {
                    rhs.parent.children[0] = null;
                }
                else
                {
                    rhsIdx = 1;
                    rhs.parent.children[1] = null;
                }
            }
            rhs = lhs;
            lhs = temp;

            //swap the node
            if(old_rhs_parent != null)
            {
                old_rhs_parent.children[rhsIdx] = rhs;
                rhs.parent = old_rhs_parent;
            }
            else
            {
                rhs.parent = null;
            }

            if(old_rhs_l_child != null)
            {
                old_rhs_l_child.parent = rhs;
            }
            rhs.children[0] = old_rhs_l_child;

            if(old_rhs_r_child != null)
            {
                old_rhs_r_child.parent = rhs;
            }
            rhs.children[1] = old_rhs_r_child;


            if(old_lhs_parent != null)
            {
                if(old_lhs_parent.children[0] == rhs)
                {
                    old_lhs_parent.children[0] = lhs;

                }
                else
                {
                    old_lhs_parent.children[1] = lhs;
                }
                lhs.parent = old_lhs_parent;
            }
            else
            {
                lhs.parent = null;
            }

            if(old_lhs_l_child != null)
            {
                old_lhs_l_child.parent = lhs;
            }
            lhs.children[0] = old_lhs_l_child;
            if(old_lhs_r_child != null)
            {
                old_lhs_r_child.parent = lhs;
            }
            lhs.children[1] = old_lhs_r_child;
        }
    }

    class MainClass
    {
        public static void Main (string[] args)
        {
            Console.WriteLine ("Hello World!");

            BSTree bTree = new BSTree();
            bTree.Insert(10);
            bTree.Insert(12);
            bTree.Insert(5);
            bTree.Insert(4);
            bTree.Insert(20);
            bTree.Insert(8);
            bTree.Insert(7);
            bTree.Insert(6);
            bTree.Insert(15);
            bTree.Insert(13);

            bTree.Remove(10);

            bTree.Insert(8);
            bTree.Remove(8);

            Node iter = bTree.Search(4);
            PrintNode(ref iter);
            iter = bTree.Search(10);
            PrintNode(ref iter);
        }

        public static void PrintNode(ref Node node)
        {
            if(node == null)
            {
                Console.WriteLine("Search success failed");
            }
            else
            {
                Console.WriteLine("Search success Success");
            }
        }
    }
}
