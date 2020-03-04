using System;
using NUnit.Framework;
using NUnitLite;

namespace InterviewPrepCode.Sorting
{
    class FbBinaryTree
    {

        [TestFixture]
        class MyBinaryTreeTest
        {
            [Test]
            public void CanIndexNodesCorrectly()
            {
                var node1 = new TreeNode<int>(1);
                var node2 = new TreeNode<int>(2);
                var node3 = new TreeNode<int>(3);
                var node4 = new TreeNode<int>(4);

                node2.Index(node1);
                Assert.That(node2.left, Is.EqualTo(node1));

                node2.Index(node3);
                Assert.That(node2.right, Is.EqualTo(node3));

                node2.Index(node4);
                Assert.That(node3.right, Is.EqualTo(node4));
            }

            [Test]
            public void DoDepthFirstSearch()
            {
                var tree = new MyBinaryTree<int>();

                tree.Add(5);
                tree.Add(3);
                tree.Add(8);
                tree.Add(4);
                tree.Add(2);
                tree.Add(6);
                tree.Add(9);


                Assert.That(tree.FindDFS(8), Is.True);

            }

            [Test]
            public void CanDelete()
            {
                var tree = new MyBinaryTree<int>();
                tree.Add(2);
                tree.Add(1);
                tree.Add(3);

                tree.Delete(2);

                Assert.That(tree.root.Value, Is.EqualTo(3));
                Assert.That(tree.root.right, Is.Null);
                Assert.That(tree.root.left.Value, Is.EqualTo(1));

                tree = new MyBinaryTree<int>();
                tree.Add(2);
                tree.Add(1);

                tree.Delete(2);

                Assert.That(tree.root.Value, Is.EqualTo(1));
                Assert.That(tree.root.right, Is.Null);
                Assert.That(tree.root.left, Is.Null);


                tree = new MyBinaryTree<int>();

                tree.Add(2);
                tree.Add(1);
                tree.Add(4);
                tree.Add(3);

                tree.Delete(2);

                Assert.That(tree.root.Value, Is.EqualTo(3));


                tree = new MyBinaryTree<int>();

                tree.Add(2);
                tree.Delete(2);

                Assert.That(tree.root, Is.Null);

            }
        }

        class MyBinaryTree<TVal> where TVal : IComparable<TVal>
        {
            public TreeNode<TVal> root;

            public void Add(TVal newValue)
            {
                var node = new TreeNode<TVal>(newValue);

                root ??= node;

                if (root != node)
                {
                    root.Index(node);
                }
            }

            public bool FindDFS(TVal valueToFind)
            {
                return root != null && root.DepthFirstSearch(valueToFind);
            }

            public void Delete(TVal valueToDelete)
            {

                var rootPlaceHolder = new TreeNode<TVal>(default(TVal));

                rootPlaceHolder.left = root;

                root.Delete(valueToDelete, rootPlaceHolder);

                root = rootPlaceHolder.left;
            }

        }

        class TreeNode<TVal> where TVal : IComparable<TVal>
        {
            public TVal Value;
            public TreeNode<TVal> left;
            public TreeNode<TVal> right;

            public TreeNode(TVal nodeValue)
            {
                Value = nodeValue;
            }

            public bool DepthFirstSearch(TVal valueToFind)
            {

                return (left?.DepthFirstSearch(valueToFind) ?? false)
                     || (right?.DepthFirstSearch(valueToFind) ?? false)
                     || CompareValue(valueToFind);
            }

            private bool CompareValue(TVal valueToFind)
            {
                Console.WriteLine($"Visiting {Value}");

                return Value.CompareTo(valueToFind) == 0;
            }

            public void Index(TreeNode<TVal> nodeToAdd)
            {
                switch (Value.CompareTo(nodeToAdd.Value))
                {

                    case -1:

                        right ??= nodeToAdd;

                        if (right != nodeToAdd)
                        {
                            right.Index(nodeToAdd);
                        }

                        return;

                    case 1:
                        left ??= nodeToAdd;

                        if (left != nodeToAdd)
                        {
                            left.Index(nodeToAdd);
                        }

                        return;
                }
            }

            public void Delete(TVal valueToDelete, TreeNode<TVal> parent)
            {
                switch (Value.CompareTo(valueToDelete))
                {
                    case -1:
                        right.Delete(valueToDelete, this);
                        break;

                    case 1:
                        left.Delete(valueToDelete, this);
                        break;

                    case 0:
                        //if L & R then get smallest value on right

                        if (left != null && right != null)
                        {
                            SwapAndDelete(valueToDelete, parent);
                            return;
                        }

                        if (this == parent.left)
                        {
                            parent.left = left ?? right;
                            return;
                        }

                        if (this == parent.right)
                        {
                            parent.right = left ?? right;
                            return;
                        }

                        break;
                }

            }

            private void SwapAndDelete(TVal valueToDelete, TreeNode<TVal> parent)
            {
                var leftMoseLeafNodeOnRightSubTree = right.FindLeftMostLeafNode();
                Value = leftMoseLeafNodeOnRightSubTree.Value;
                right.Delete(leftMoseLeafNodeOnRightSubTree.Value, this);
            }

            private TreeNode<TVal> FindLeftMostLeafNode()
            {
                return left?.FindLeftMostLeafNode() ?? this;
            }

        }

    }
}