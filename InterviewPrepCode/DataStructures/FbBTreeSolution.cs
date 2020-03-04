using System;
using NUnit.Framework;
using NUnitLite;

namespace InterviewPrepCode.DataStructures
{
    class FbBTreeSolution
    {
        [TestFixture]
        class BTreeT
        {

            [Test]
            public void WorksCorrectly()
            {
                var tree = new BTree<int>();

                tree.Add(2);
                tree.Add(1);
                tree.Add(3);

                Assert.That(tree.Root.Item, Is.EqualTo(2));
                Assert.That(tree.Root.Left.Item, Is.EqualTo(1));
                Assert.That(tree.Root.Right.Item, Is.EqualTo(3));

            }

            [Test]
            public void FindsCorrectly()
            {
                var tree = new BTree<int>();

                tree.Add(5);
                tree.Add(3);
                tree.Add(7);
                tree.Add(1);
                tree.Add(2);
                tree.Add(6);
                tree.Add(9);

                Assert.True(tree.FindPreOrder(6));
                Assert.False(tree.FindPreOrder(10));
            }

            [Test]
            public void CanDelete()
            {
                var tree = new BTree<int>();

                tree.Add(5);
                tree.Add(3);
                tree.Add(7);
                tree.Add(1);
                tree.Add(2);
                tree.Add(6);
                tree.Add(9);


                tree.Delete(5);
                Assert.False(tree.FindPreOrder(5));

                tree.Delete(9);
                Assert.False(tree.FindPreOrder(9));
            }
        }

        public class BTree<TVal> where TVal : IComparable<TVal>
        {
            public Node<TVal> Root;

            public void Add(TVal item)
            {

                var newEntry = new Node<TVal>(item);

                Root ??= newEntry;

                if (Root != newEntry)
                {
                    Root.Add(newEntry);
                }
            }

            public bool FindPreOrder(TVal item)
            {
                return Root != null && Root.FindPreOrder(item);
            }

            public void Delete(TVal item)
            {
                if (Root == null)
                {
                    return;
                }

                var tempParent = new Node<TVal>(default(TVal));
                tempParent.Left = Root;

                Root.Delete(item, tempParent);

                Root = tempParent.Left;
            }
        }

        public class Node<TVal> where TVal : IComparable<TVal>
        {
            public TVal Item;
            public Node<TVal> Left;
            public Node<TVal> Right;

            public Node(TVal item)
            {
                Item = item;
            }

            public void Add(Node<TVal> node)
            {

                switch (Item.CompareTo(node.Item))
                {
                    case 1:
                        Left ??= node;
                        if (Left != node)
                        {
                            Left.Add(node);
                        }

                        break;

                    case -1:
                        Right ??= node;
                        if (Right != node)
                        {
                            Right.Add(node);
                        }
                        break;
                }
            }

            public bool FindPreOrder(TVal item)
            {
                Console.WriteLine($"Visiting Node {Item}");

                return Item.CompareTo(item) == 0
                    || (Left != null && Left.FindPreOrder(item))
                    || (Right != null && Right.FindPreOrder(item));
            }

            public void Delete(TVal item, Node<TVal> parent)
            {
                switch (Item.CompareTo(item))
                {
                    case 0:
                        HandleDelete(item, parent);
                        break;
                    case -1:
                        Right?.Delete(item, this);
                        break;
                    case 1:
                        Left?.Delete(item, this);
                        break;
                }
            }

            private void HandleDelete(TVal item, Node<TVal> parent)
            {
                if (Left != null && Right != null)
                {
                    var smallestChildOnTheRight = Right.FindLeafWithLowestValue();

                    if (smallestChildOnTheRight != null)
                    {
                        Item = smallestChildOnTheRight.Item;
                        smallestChildOnTheRight.Item = item;
                        Right.Delete(item, this);
                    }

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

            private Node<TVal> FindLeafWithLowestValue()
            {
                return Left != null ? Left.FindLeafWithLowestValue() : this;
            }

        }
    }

}