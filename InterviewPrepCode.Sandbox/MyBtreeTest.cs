using System;
using System.Collections.Generic;
using Xunit;

namespace InterviewPrepCode.Sandbox
{
    public class MyBtreeTest
    {
        [Fact]
        void CanBuildTree()
        {
            var tree = new MyBtree<int>();

            tree.Add(2);
            Assert.Equal(2, tree.Root.Key);

            tree.Add(1);
            Assert.Equal(1, tree.Root.Left.Key);

            tree.Add(4);
            Assert.Equal(4, tree.Root.Right.Key);

            tree.Add(3);
            Assert.Equal(3, tree.Root.Right.Left.Key);

            tree.Add(6);
            Assert.Equal(6, tree.Root.Right.Right.Key);

            tree.Add(5);
            Assert.Equal(5, tree.Root.Right.Right.Left.Key);


            tree.Delete(4);
            Assert.Equal(5, tree.Root.Right.Key);
            Assert.Equal(6, tree.Root.Right.Right.Key);
            Assert.Null(tree.Root.Right.Right.Left);

            var visitationList = new List<int>();
            var outcome = tree.CanFind(3, TraversalType.POSTORDER, visitationList);
            Assert.True(outcome);


            visitationList.Clear();
            outcome = tree.CanFind(4, TraversalType.PREORDER, visitationList);
            Assert.False(outcome);
        }
    }

    public class MyBtree<TVal> where TVal : IComparable<TVal>
    {
        public Node<TVal> Root;

        public void Add(TVal item)
        {
            var node = new Node<TVal>(item);

            Root ??= node;

            if (Root != node)
            {
                Root.Add(node);
            }

        }

        public void Delete(TVal item)
        {
            var tempRoot = new Node<TVal>(default(TVal));
            tempRoot.Left = Root;

            Root.Delete(item, tempRoot);

            Root = tempRoot.Left;
        }

        public bool CanFind(TVal i, TraversalType traversalType, List<TVal> visitationList)
        {
            switch (traversalType)
            {
                case TraversalType.INORDER:
                    return Root?.InOrder(i, visitationList) ?? false;
                    break;
                case TraversalType.PREORDER:
                    return Root?.PreOrder(i, visitationList) ?? false;
                    break;
                case TraversalType.POSTORDER:
                    return Root?.PostOrder(i, visitationList) ?? false;
                    break;
            }

            return false;
        }
    }

    public class Node<TVal> where TVal : IComparable<TVal>
    {
        public TVal Key;
        public Node<TVal> Left { get; set; }
        public Node<TVal> Right { get; set; }

        public Node(TVal item)
        {
            Key = item;
        }


        public void Add(Node<TVal> node)
        {
            switch (Key.CompareTo(node.Key))
            {
                case -1:
                    Right ??= node;
                    if (Right != node)
                        Right.Add(node);
                    break;

                case 0:
                    break;

                case 1:
                    Left ??= node;
                    if (Left != node)
                        Left.Add(node);
                    break;
            }
        }

        public void Delete(TVal item, Node<TVal> parent)
        {
            switch (Key.CompareTo(item))
            {
                case -1:
                    Right.Delete(item, this);
                    break;

                case 0:
                    HandleDelete(item, parent);
                    break;

                case 1:
                    Left.Delete(item, this);
                    break;
            }
        }

        private void HandleDelete(TVal item, Node<TVal> parent)
        {
            if (Left != null && Right != null)
            {
                var smallestRightChild = Right.FindSmallestChild();
                Key = smallestRightChild.Key;
                smallestRightChild.Key = item;
                Right.Delete(item, this);
                return;
            }

            if (parent.Left == this)
            {
                parent.Left = Left ?? Right;
            }

            if (parent.Right == this)
            {
                parent.Right = Left ?? Right;
            }
        }

        private Node<TVal> FindSmallestChild()
        {
            return Left?.FindSmallestChild() ?? this;
        }

        public bool InOrder(TVal findItem, List<TVal> visitationList)
        {
            return (Left?.InOrder(findItem, visitationList) ?? false)
                   || (AddToVisitationList(visitationList) && Key.CompareTo(findItem) == 0)
                   || (Right?.InOrder(findItem, visitationList) ?? false);
        }

        public bool PreOrder(TVal findItem, List<TVal> visitationList)
        {
            return (AddToVisitationList(visitationList) && Key.CompareTo(findItem) == 0)
                   || (Left?.InOrder(findItem, visitationList) ?? false)
                   || (Right?.InOrder(findItem, visitationList) ?? false);
        }

        public bool PostOrder(TVal findItem, List<TVal> visitationList)
        {
            return (Left?.InOrder(findItem, visitationList) ?? false)
                   || (Right?.InOrder(findItem, visitationList) ?? false)
                   || (AddToVisitationList(visitationList) && Key.CompareTo(findItem) == 0);
        }

        private bool AddToVisitationList(List<TVal> visitationList)
        {
            visitationList.Add(Key);
            return true;
        }
    }

    public enum TraversalType
    {
        INORDER,
        PREORDER,
        POSTORDER
    }
}