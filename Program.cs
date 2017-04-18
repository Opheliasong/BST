#define RBT
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

    class RBNode{
        /// <summary>
        /// node's Red-Black flag
        /// </summary>
        public bool isRed = true;
        public RBNode()
        {
            //new node is red
            isRed = true;
        }

        public RBNode(int val)
        {
            //new node is red
            isRed = true;
            Value = val;
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
        bool init = false;
        public bool Init{
            get{
                return init;
            }
        }
        public int Value{
            get {
                return value;
            }
            set {
                this.value = value;
                init = true;
            }
        }
        int value;

        /// <summary>
        /// The children.
        /// childern[0] = left
        /// childeren[1] = right
        /// </summary>
        public RBNode[] children = new RBNode[2];
        public RBNode parent = null;

        public void SetBlack()
        {
            isRed = false;
        }

        public void SetRed()
        {
            isRed = true;
        }

        public void SetParent(RBNode p)
        {
            parent = p;
        }
    }

    class RBTree
    {
        public RBNode root = null;

        public void Insert(int val)
        {
            if(root == null)
            {
                root = new RBNode(val);
                //Root is always black.
                root.SetBlack();
                return;
            }

            RBNode find = root;
            int idx = 0;
            for(RBNode iter = root; iter != null; )
            {
                find = iter;

                if(find.Value > val)
                {
                    idx = 0;
                    iter= iter.children[0];
                }
                else
                {
                    idx = 1;
                    iter= iter.children[1];
                }
            }

            RBNode newNode = new RBNode(val);
            LinkNode(ref newNode, ref find, ref find.children[idx]);

            InsertColor(ref newNode);
        }

        void LinkNode(ref RBNode node, ref RBNode parent, ref RBNode link)
        {
            node.parent = parent;
            node.children[0] = null;
            node.children[1] = null;
            link = node;
        }

        void Rotate_left(ref RBNode rotationNode)
        {
            RBNode right = rotationNode.children[1];
            RBNode parent = rotationNode.parent;
            if((rotationNode.children[1] = right.children[0]) != null)
            {
                //If right nodes left child isn't null, 
                //it's will set to rotation nodes right child and that node parent is rotation node
                right.children[0].SetParent(rotationNode);
            }
            right.children[0] = rotationNode;
            right.SetParent(parent);

            if(parent != null)
            {
                if(rotationNode == parent.children[0])
                {
                    parent.children[0] = right;
                }
                else
                {
                    parent.children[1] = right;
                }
            }
            else
            {
                root = right;
            }
            rotationNode.SetParent(right);
        }

        void Rotate_Right(ref RBNode rotateNode)
        {
            RBNode left = rotateNode.children[0];
            RBNode parent = rotateNode.parent;

            if((rotateNode.children[0] = left.children[1]) != null)
            {
                left.children[1].SetParent(rotateNode);
            }
            left.children[1] = rotateNode;
            left.SetParent(parent);

            if(parent != null)
            {
                if(rotateNode == parent.children[0])
                {
                    parent.children[0] = left;
                }
                else
                {
                    parent.children[1] = left;
                }
            }
            else
            {
                root = left;
            }
            rotateNode.SetParent(left);
        }

        void InsertColor(ref RBNode node)
        {
            RBNode parent, gparent;

            while((parent = node.parent) != null &&  parent.isRed)
            {
                gparent = parent.parent;

                if(parent == gparent.children[0])
                {
                    //If parent node is gparent left child
                    {
                        RBNode uncle = gparent.children[1];
                        if(uncle != null && uncle.isRed)
                        {
                            //If uncle is red node
                            uncle.SetBlack();
                            parent.SetBlack();
                            gparent.SetRed();
                            node = gparent;
                            continue;
                        }                        
                    }

                    //If uncle is not available or uncle is black
                    if(node == parent.children[1])
                    {
                        //this node is parent's right child
                        RBNode temp = null;
                        Rotate_left(ref parent);
                        temp = parent;
                        node.parent = node;
                        node = temp;
                    }

                    parent.SetBlack();
                    gparent.SetRed();
                    Rotate_Right(ref gparent);
                }
                else
                {
                    //If parent node is gparent right child
                    {
                        RBNode uncle = gparent.children[0];
                        if(uncle != null && uncle.isRed)
                        {
                            uncle.SetBlack();
                            parent.SetBlack();
                            gparent.SetRed();
                            node = gparent;
                            continue;
                        }
                    }

                    if(parent.children[0] == node)
                    {
                        RBNode temp = null;
                        Rotate_Right(ref parent);
                        temp = parent;
                        parent = node;
                        node = temp;
                    }
                    parent.SetBlack();
                    gparent.SetRed();
                    Rotate_left(ref gparent);
                }
            }

            root.SetBlack();
        }

        public void Delete(int val)
        {
            RBNode node = Search(val);

            if(node != null)
            {
                if(node.isLeaf)
                {
                    if(node.parent.children[0] == node)
                    {
                        node.parent.children[0] = null;
                    }
                    else
                    {
                        node.children[1] = null;
                    }
                }
            }
        }

        void ReplaceNodeParent(ref RBNode node, RBNode newNode = null)
        {
            if(node.parent != null)
            {
                if(node == node.parent.children[0])
                {
                    node.parent.children[0] = newNode;
                }
                else
                {
                    node.parent.children[1] = newNode;
                }
            }

            if(newNode != null)
            {
                newNode.parent = node.parent;
            }
        }

        public RBNode Search(int val)
        {
            RBNode iter = root;

            while(iter.Value == val)
            {
                if(iter.Value < val)
                {
                    iter = iter.children[0];
                }
                else if(iter.Value > val)
                {
                    iter = iter.children[1];
                }
            }
            return iter;
        }

        public RBNode Search_Recursive(int val, RBNode node = null)
        {
            if(node == null)
            {
                node = root;
            }

            if(node.Value == val)
            {
                return node;
            }
            else if(node.Value > val)
            {
                return Search_Recursive(val, node.children[0]);
            }
            else
            {
                return Search_Recursive(val, node.children[1]);
            }
        }
    }

    class MainClass
    {
        public static void Main (string[] args)
        {
            Console.WriteLine ("Hello World!");            
#if (BST)
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
#elif (RBT)
            RBTree RBTree = new RBTree();
            RBTree.Insert(1);
            RBTree.Insert(2);
            RBTree.Insert(3);
            RBTree.Insert(4);
            RBTree.Insert(5);
            RBTree.Insert(6);
            RBTree.Insert(7);
            RBTree.Insert(8);

            var finder = RBTree.Search_Recursive(5);
            if(finder != null)
            {
                Console.WriteLine("Find it");
            }
#endif            
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
