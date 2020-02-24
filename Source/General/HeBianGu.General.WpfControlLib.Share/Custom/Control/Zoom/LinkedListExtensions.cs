using System;
using System.Collections.Generic;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// Provides extension methods for LinkedList.
    /// </summary>
    public static class LinkedListExtensions
    {
        /// <summary>
        /// Finds the next node after the given node that contains the specified value.
        /// </summary>
        /// <typeparam name="T">The type of value in the linked list.</typeparam>
        /// <param name="list">The linked list.</param>
        /// <param name="node">The node after which to search for the value in the linked list, or <c>null</c> to search from the beginning.</param>
        /// <param name="value">The value to locate in the linked list.</param>
        /// <returns>The first node after the given node that contains the specified value, if found; otherwise, <c>null</c>.</returns>
        public static LinkedListNode<T> FindNext<T>(this LinkedList<T> list, LinkedListNode<T> node, T value)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            
            if (node == null)
            {
                return list.Find(value);
            }

            if (list != node.List)
            {
                throw new ArgumentException("The list does not contain the given node.");
            }

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            // Skip the given node.
            node = node.Next;
            while (node != null)
            {
                if (value != null)
                {
                    if (comparer.Equals(node.Value, value))
                    {
                        return node;
                    }
                }
                else if (node.Value == null)
                {
                    return node;
                }
                node = node.Next;
            }

            return null;
        }

        /// <summary>
        /// Finds the previous node before the given node that contains the specified value.
        /// </summary>
        /// <typeparam name="T">The type of value in the linked list.</typeparam>
        /// <param name="list">The linked list.</param>
        /// <param name="node">The node before which to search for the value in the linked list, or <c>null</c> to search from the end.</param>
        /// <param name="value">The value to locate in the linked list.</param>
        /// <returns>The first node before the given node that contains the specified value, if found; otherwise, <c>null</c>.</returns>
        public static LinkedListNode<T> FindPrevious<T>(this LinkedList<T> list, LinkedListNode<T> node, T value)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            if (node == null)
            {
                return list.FindLast(value);
            }

            if (list != node.List)
            {
                throw new ArgumentException("The list does not contain the given node.");
            }

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            // Skip the given node.
            node = node.Previous;
            while (node != null)
            {
                if (value != null)
                {
                    if (comparer.Equals(node.Value, value))
                    {
                        return node;
                    }
                }
                else if (node.Value == null)
                {
                    return node;
                }
                node = node.Previous;
            }

            return null;
        }
    }
}