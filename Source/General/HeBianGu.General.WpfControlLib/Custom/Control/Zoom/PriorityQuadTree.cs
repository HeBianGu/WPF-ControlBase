using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// This class efficiently stores and lazily retrieves arbitrarily sized and positioned objects in a prioritized order in a quad-tree data structure.
    /// This can be used to do efficient hit detection or visibility checks on objects in a two dimensional space.
    /// The object does not need to implement any special interface because the Rect Bounds of those objects is handled as a separate argument to Insert.
    /// </summary>
    /// <remarks>
    /// Original class written by Chris Lovett.
    /// Prioritization and lazy enumeration added by Kael Rowan.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public class PriorityQuadTree<T> : IEnumerable<T>
    {
        /// <summary>
        /// Each node stored in the tree has a position, width & height.        
        /// </summary>
        private class QuadNode
        {
            private readonly Rect _bounds; // the bounds of the node
            private readonly T _node; // the actual object being stored here.
            private readonly double _priority; // the priority of the object being stored here.
            private QuadNode _next; // linked in a circular list.

            /// <summary>
            /// Construct new QuadNode to wrap the given node with given bounds.
            /// </summary>
            /// <param name="node">The node with generic type T.</param>
            /// <param name="bounds">The bounds of that node.</param>
            /// <param name="priority">The priority of that node.</param>
            public QuadNode(T node, Rect bounds, double priority)
            {
                _node = node;
                _bounds = bounds;
                _priority = priority;
            }

            /// <summary>
            /// The wrapped node.
            /// </summary>
            public T Node
            {
                get { return _node; }
            }

            /// <summary>
            /// The Rect bounds of the node.
            /// </summary>
            public Rect Bounds
            {
                get { return _bounds; }
            }

            /// <summary>
            /// The priority of the node.
            /// </summary>
            public double Priority
            {
                get { return _priority; }
            }

            /// <summary>
            /// QuadNodes form a linked list in the Quadrant.
            /// </summary>
            public QuadNode Next
            {
                get { return _next; }
                set { _next = value; }
            }

            /// <summary>
            /// Inserts this QuadNode into an existing list and returns the new tail of the list.
            /// </summary>
            /// <param name="tail">The tail of an existing circular linked list of QuadNodes, or <c>null</c> if this is the first.</param>
            /// <returns>The (possibly new) tail of the circular linked list after inserting this QuadNode into it.</returns>
            public QuadNode InsertInto(QuadNode tail)
            {
                if (tail == null)
                {
                    Next = this;
                    tail = this;
                }
                else
                {
                    // link up in circular link list.
                    if (Priority < tail.Priority)
                    {
                        Next = tail.Next;
                        tail.Next = this;
                        tail = this;
                    }
                    else
                    {
                        QuadNode x;
                        for (x = tail; x.Next != tail && Priority < x.Next.Priority; x = x.Next) ;
                        Next = x.Next;
                        x.Next = this;
                    }
                }
                return tail;
            }

            /// <summary>
            /// Walk the linked list of QuadNodes and check them against the given bounds.
            /// </summary>
            /// <param name="bounds">The bounds to test against each node.</param>
            /// <returns>A lazy list of nodes along with the priority of the next node.</returns>
            public IEnumerable<Tuple<QuadNode, double>> GetNodesIntersecting(Rect bounds)
            {
                QuadNode n = this;
                do
                {
                    n = n.Next; // first node.
                    if (bounds.Intersects(n.Bounds))
                    {
                        yield return Tuple.Create(n, n != this ? n.Next.Priority : double.NaN);
                    }
                } while (n != this);
            }

            /// <summary>
            /// Walk the linked list of QuadNodes and check them against the given bounds.
            /// </summary>
            /// <param name="bounds">The bounds to test against each node.</param>
            /// <returns>A lazy list of nodes along with the priority of the next node.</returns>
            public IEnumerable<Tuple<QuadNode, double>> GetNodesInside(Rect bounds)
            {
                QuadNode n = this;
                do
                {
                    n = n.Next; // first node.
                    if (bounds.Contains(n.Bounds))
                    {
                        yield return Tuple.Create(n, n != this ? n.Next.Priority : double.NaN);
                    }
                } while (n != this);
            }

            /// <summary>
            /// Walk the linked list and test each node against the given bounds.
            /// </summary>
            /// <param name="bounds">Bounds to test.</param>
            /// <returns>Return true if a node in the list intersects the bounds.</returns>
            public bool HasNodesIntersecting(Rect bounds)
            {
                QuadNode n = this;
                do
                {
                    n = n.Next; // first node.
                    if (bounds.Intersects(n.Bounds))
                    {
                        return true;
                    }
                } while (n != this);

                return false;
            }

            /// <summary>
            /// Walk the linked list and test each node against the given bounds.
            /// </summary>
            /// <param name="bounds">Bounds to test.</param>
            /// <returns>Return true if a node in the list is inside the bounds.</returns>
            public bool HasNodesInside(Rect bounds)
            {
                QuadNode n = this;
                do
                {
                    n = n.Next; // first node.
                    if (bounds.Contains(n.Bounds))
                    {
                        return true;
                    }
                } while (n != this);

                return false;
            }
        }

        /// <summary>
        /// The quad tree is split up into four Quadrants and objects are stored in the quadrant that contains them
        /// and each quadrant is split up into four child Quadrants recurrsively.  Objects that overlap more than
        /// one quadrant are stored in the nodes list for this Quadrant.
        /// </summary>
        private class Quadrant : IEnumerable<QuadNode>
        {
            private readonly Rect _bounds; // quadrant bounds.
            private double _potential = Double.NegativeInfinity; // the maximum priority of all nodes within this quadrant.
            private int _count;

            private QuadNode _nodes; // nodes that overlap the sub quadrant boundaries.

            // The quadrant is subdivided when nodes are inserted that are 
            // completely contained within those subdivisions.
            private Quadrant _topLeft;
            private Quadrant _topRight;
            private Quadrant _bottomLeft;
            private Quadrant _bottomRight;

            /// <summary>
            /// Construct new Quadrant with a given bounds all nodes stored inside this quadrant
            /// will fit inside this bounds.  
            /// </summary>
            /// <param name="bounds">The bounds of this quadrant</param>
            public Quadrant(Rect bounds)
            {
                _bounds = bounds;
            }

            /// <summary>
            /// Insert the given node.
            /// </summary>
            /// <param name="node">The wrapped node.</param>
            /// <param name="bounds">The bounds of that node.</param>
            /// <param name="priority">The priority of that node.</param>
            /// <param name="depth">The recursive depth of this call, to avoid stack overflows.</param>
            /// <returns>The quadrant that ultimately holds the node.</returns>            
            internal Quadrant Insert(T node, Rect bounds, double priority, int depth)
            {
                _potential = Math.Max(_potential, priority);
                _count++;

                Quadrant child = null;

                // Only drill down the tree for positive sized bounds, otherwise we could drill forever.
                // Todo: We can remove this restriction if we choose to only split quads when "full".
                if (depth <= PriorityQuadTree<T>.MaxTreeDepth && (bounds.Width > 0 || bounds.Height > 0))
                {
                    double w = _bounds.Width / 2;
                    double h = _bounds.Height / 2;

                    // assumption that the Rect struct is almost as fast as doing the operations
                    // manually since Rect is a value type.
                    Rect topLeft = new Rect(_bounds.Left, _bounds.Top, w, h);
                    Rect topRight = new Rect(_bounds.Left + w, _bounds.Top, w, h);
                    Rect bottomLeft = new Rect(_bounds.Left, _bounds.Top + h, w, h);
                    Rect bottomRight = new Rect(_bounds.Left + w, _bounds.Top + h, w, h);

                    // See if any child quadrants completely contain this node.
                    if (topLeft.Contains(bounds))
                    {
                        if (_topLeft == null)
                        {
                            _topLeft = new Quadrant(topLeft);
                        }
                        child = _topLeft;
                    }
                    else if (topRight.Contains(bounds))
                    {
                        if (_topRight == null)
                        {
                            _topRight = new Quadrant(topRight);
                        }
                        child = _topRight;
                    }
                    else if (bottomLeft.Contains(bounds))
                    {
                        if (_bottomLeft == null)
                        {
                            _bottomLeft = new Quadrant(bottomLeft);
                        }
                        child = _bottomLeft;
                    }
                    else if (bottomRight.Contains(bounds))
                    {
                        if (_bottomRight == null)
                        {
                            _bottomRight = new Quadrant(bottomRight);
                        }
                        child = _bottomRight;
                    }
                }

                if (child != null)
                {
                    return child.Insert(node, bounds, priority, depth + 1);
                }
                else
                {
                    QuadNode n = new QuadNode(node, bounds, priority);
                    _nodes = n.InsertInto(_nodes);
                    return this;
                }
            }

            /// <summary>
            /// Removes the first occurance of the given node from this quadrant or any child quadrants within the search bounds.
            /// </summary>
            /// <param name="node">The node to remove.</param>
            /// <param name="bounds">The bounds to search within.</param>
            /// <returns><c>true</c> if the node was found and removed; otherwise, <c>false</c>.</returns>
            internal bool Remove(T node, Rect bounds)
            {
                bool nodeRemoved = false;
                if (RemoveNode(node))
                {
                    nodeRemoved = true;
                }
                else
                {
                    double w = _bounds.Width / 2;
                    double h = _bounds.Height / 2;

                    // assumption that the Rect struct is almost as fast as doing the operations
                    // manually since Rect is a value type.
                    Rect topLeft = new Rect(_bounds.Left, _bounds.Top, w, h);
                    Rect topRight = new Rect(_bounds.Left + w, _bounds.Top, w, h);
                    Rect bottomLeft = new Rect(_bounds.Left, _bounds.Top + h, w, h);
                    Rect bottomRight = new Rect(_bounds.Left + w, _bounds.Top + h, w, h);

                    if (_topLeft != null && topLeft.Intersects(bounds) && _topLeft.Remove(node, bounds))
                    {
                        if (_topLeft._count == 0)
                        {
                            _topLeft = null;
                        }

                        nodeRemoved = true;
                    }
                    else if (_topRight != null && topRight.Intersects(bounds) && _topRight.Remove(node, bounds))
                    {
                        if (_topRight._count == 0)
                        {
                            _topRight = null;
                        }

                        nodeRemoved = true;
                    }
                    else if (_bottomLeft != null && bottomLeft.Intersects(bounds) && _bottomLeft.Remove(node, bounds))
                    {
                        if (_bottomLeft._count == 0)
                        {
                            _bottomLeft = null;
                        }

                        nodeRemoved = true;
                    }
                    else if (_bottomRight != null && bottomRight.Intersects(bounds) && _bottomRight.Remove(node, bounds))
                    {
                        if (_bottomRight._count == 0)
                        {
                            _bottomRight = null;
                        }

                        nodeRemoved = true;
                    }
                }

                if (nodeRemoved)
                {
                    _count--;
                    _potential = CalculatePotential();
                    return true;
                }

                return false;
            }

            /// <summary>
            /// Returns all nodes in this quadrant that intersect the given bounds.
            /// The nodes are returned in order of descending priority.
            /// </summary>
            /// <param name="bounds">The bounds that intersects the nodes you want returned.</param>
            /// <returns>A lazy list of nodes along with the new potential of this quadrant.</returns>
            internal IEnumerable<Tuple<QuadNode, double>> GetNodesIntersecting(Rect bounds)
            {
                double w = _bounds.Width / 2;
                double h = _bounds.Height / 2;

                // assumption that the Rect struct is almost as fast as doing the operations
                // manually since Rect is a value type.
                Rect topLeft = new Rect(_bounds.Left, _bounds.Top, w, h);
                Rect topRight = new Rect(_bounds.Left + w, _bounds.Top, w, h);
                Rect bottomLeft = new Rect(_bounds.Left, _bounds.Top + h, w, h);
                Rect bottomRight = new Rect(_bounds.Left + w, _bounds.Top + h, w, h);

                // Create a priority queue based on the potential of our nodes and our quads.
                var queue = new PriorityQueue<IEnumerator<Tuple<QuadNode, double>>, double>(true);

                if (_nodes != null)
                {
                    queue.Enqueue(_nodes.GetNodesIntersecting(bounds).GetEnumerator(), _nodes.Next.Priority);
                }

                if (_topLeft != null && topLeft.Intersects(bounds))
                {
                    queue.Enqueue(_topLeft.GetNodesIntersecting(bounds).GetEnumerator(), _topLeft._potential);
                }

                if (_topRight != null && topRight.Intersects(bounds))
                {
                    queue.Enqueue(_topRight.GetNodesIntersecting(bounds).GetEnumerator(), _topRight._potential);
                }

                if (_bottomLeft != null && bottomLeft.Intersects(bounds))
                {
                    queue.Enqueue(_bottomLeft.GetNodesIntersecting(bounds).GetEnumerator(), _bottomLeft._potential);
                }

                if (_bottomRight != null && bottomRight.Intersects(bounds))
                {
                    queue.Enqueue(_bottomRight.GetNodesIntersecting(bounds).GetEnumerator(), _bottomRight._potential);
                }

                // Then just loop through the queue.
                while (queue.Count > 0)
                {
                    // Grab the enumerator with the highest potential.
                    var enumerator = queue.Dequeue().Key;
                    if (enumerator.MoveNext())
                    {
                        // Get the current node and its new potential from the enumerator.
                        var current = enumerator.Current;
                        var node = current.Item1;
                        var potential = current.Item2;

                        // Determine our new potential.
                        var newPotential = queue.Count > 0 ? !potential.IsNaN() ? Math.Max(potential, queue.Peek().Value) : queue.Peek().Value : potential;

                        // It might be the case that the actual intersecting node has less priority than our remaining potential.
                        if (newPotential > node.Priority)
                        {
                            // Store it for later in a container containing only it with no further potential.
                            var store = Enumerable.Repeat(Tuple.Create(node, double.NaN), 1).GetEnumerator();

                            // Enqueue the container at the correct position.
                            queue.Enqueue(store, node.Priority);
                        }
                        else
                        {
                            // Return it to our parent along with our new potential.
                            yield return Tuple.Create(node, newPotential);
                        }

                        // If this enumerator has some more potential then re-enqueue it.
                        if (!potential.IsNaN())
                        {
                            queue.Enqueue(enumerator, potential);
                        }
                    }
                }
            }

            /// <summary>
            /// Returns all nodes in this quadrant that are fully contained within the given bounds.
            /// The nodes are returned in order of descending priority.
            /// </summary>
            /// <param name="bounds">The bounds that contains the nodes you want returned.</param>
            /// <returns>A lazy list of nodes along with the new potential of this quadrant.</returns>
            internal IEnumerable<Tuple<QuadNode, double>> GetNodesInside(Rect bounds)
            {
                double w = _bounds.Width / 2;
                double h = _bounds.Height / 2;

                // assumption that the Rect struct is almost as fast as doing the operations
                // manually since Rect is a value type.
                Rect topLeft = new Rect(_bounds.Left, _bounds.Top, w, h);
                Rect topRight = new Rect(_bounds.Left + w, _bounds.Top, w, h);
                Rect bottomLeft = new Rect(_bounds.Left, _bounds.Top + h, w, h);
                Rect bottomRight = new Rect(_bounds.Left + w, _bounds.Top + h, w, h);

                // Create a priority queue based on the potential of our nodes and our quads.
                var queue = new PriorityQueue<IEnumerator<Tuple<QuadNode, double>>, double>(true);

                if (_nodes != null)
                {
                    queue.Enqueue(_nodes.GetNodesInside(bounds).GetEnumerator(), _nodes.Next.Priority);
                }

                if (_topLeft != null && topLeft.Intersects(bounds))
                {
                    queue.Enqueue(_topLeft.GetNodesInside(bounds).GetEnumerator(), _topLeft._potential);
                }

                if (_topRight != null && topRight.Intersects(bounds))
                {
                    queue.Enqueue(_topRight.GetNodesInside(bounds).GetEnumerator(), _topRight._potential);
                }

                if (_bottomLeft != null && bottomLeft.Intersects(bounds))
                {
                    queue.Enqueue(_bottomLeft.GetNodesInside(bounds).GetEnumerator(), _bottomLeft._potential);
                }

                if (_bottomRight != null && bottomRight.Intersects(bounds))
                {
                    queue.Enqueue(_bottomRight.GetNodesInside(bounds).GetEnumerator(), _bottomRight._potential);
                }

                // Then just loop through the queue.
                while (queue.Count > 0)
                {
                    // Grab the enumerator with the highest potential.
                    var enumerator = queue.Dequeue().Key;
                    if (enumerator.MoveNext())
                    {
                        // Get the current node and its new potential from the enumerator.
                        var current = enumerator.Current;
                        var node = current.Item1;
                        var potential = current.Item2;

                        // Determine our new potential.
                        var newPotential = queue.Count > 0 ? !potential.IsNaN() ? Math.Max(potential, queue.Peek().Value) : queue.Peek().Value : potential;

                        // It might be the case that the actual intersecting node has less priority than our remaining potential.
                        if (newPotential > node.Priority)
                        {
                            // Store it for later in a container containing only it with no further potential.
                            var store = Enumerable.Repeat(Tuple.Create(node, double.NaN), 1).GetEnumerator();

                            // Enqueue the container at the correct position.
                            queue.Enqueue(store, node.Priority);
                        }
                        else
                        {
                            // Return it to our parent along with our new potential.
                            yield return Tuple.Create(node, newPotential);
                        }

                        // If this enumerator has some more potential then re-enqueue it.
                        if (!potential.IsNaN())
                        {
                            queue.Enqueue(enumerator, potential);
                        }
                    }
                }
            }

            /// <summary>
            /// Return true if there are any nodes in this Quadrant are inside the given bounds.
            /// </summary>
            /// <param name="bounds">The bounds to test</param>
            /// <returns><c>true</c> if this quadrant or its subquadrants has nodes inside the bounds; otherwise, <c>false</c>.</returns>
            internal bool HasNodesInside(Rect bounds)
            {
                double w = _bounds.Width / 2;
                double h = _bounds.Height / 2;

                // assumption that the Rect struct is almost as fast as doing the operations
                // manually since Rect is a value type.
                Rect topLeft = new Rect(_bounds.Left, _bounds.Top, w, h);
                Rect topRight = new Rect(_bounds.Left + w, _bounds.Top, w, h);
                Rect bottomLeft = new Rect(_bounds.Left, _bounds.Top + h, w, h);
                Rect bottomRight = new Rect(_bounds.Left + w, _bounds.Top + h, w, h);

                if (_nodes != null && _nodes.HasNodesInside(bounds))
                {
                    return true;
                }

                if (_topLeft != null && topLeft.Contains(bounds) && _topLeft.HasNodesInside(bounds))
                {
                    return true;
                }

                if (_topRight != null && topRight.Contains(bounds) && _topRight.HasNodesInside(bounds))
                {
                    return true;
                }

                if (_bottomLeft != null && bottomLeft.Contains(bounds) && _bottomLeft.HasNodesInside(bounds))
                {
                    return true;
                }

                if (_bottomRight != null && bottomRight.Contains(bounds) && _bottomRight.HasNodesInside(bounds))
                {
                    return true;
                }

                return false;
            }

            /// <summary>
            /// Return true if there are any nodes in this Quadrant that intersect the given bounds.
            /// </summary>
            /// <param name="bounds">The bounds to test</param>
            /// <returns><c>true</c> if this quadrant or its subquadrants has nodes intersecting the bounds; otherwise, <c>false</c>.</returns>
            internal bool HasNodesIntersecting(Rect bounds)
            {
                double w = _bounds.Width / 2;
                double h = _bounds.Height / 2;

                // assumption that the Rect struct is almost as fast as doing the operations
                // manually since Rect is a value type.
                Rect topLeft = new Rect(_bounds.Left, _bounds.Top, w, h);
                Rect topRight = new Rect(_bounds.Left + w, _bounds.Top, w, h);
                Rect bottomLeft = new Rect(_bounds.Left, _bounds.Top + h, w, h);
                Rect bottomRight = new Rect(_bounds.Left + w, _bounds.Top + h, w, h);

                if (_nodes != null && _nodes.HasNodesIntersecting(bounds))
                {
                    return true;
                }

                if (_topLeft != null && topLeft.Intersects(bounds) && _topLeft.HasNodesIntersecting(bounds))
                {
                    return true;
                }

                if (_topRight != null && topRight.Intersects(bounds) && _topRight.HasNodesIntersecting(bounds))
                {
                    return true;
                }

                if (_bottomLeft != null && bottomLeft.Intersects(bounds) && _bottomLeft.HasNodesIntersecting(bounds))
                {
                    return true;
                }

                if (_bottomRight != null && bottomRight.Intersects(bounds) && _bottomRight.HasNodesIntersecting(bounds))
                {
                    return true;
                }

                return false;
            }

            /// <summary>
            /// Remove the given node from this Quadrant.(non-recursive)
            /// </summary>
            /// <param name="node">The node to remove.</param>            
            /// <returns>Returns true if the node was found and removed.</returns>
            private bool RemoveNode(T node)
            {
                bool rc = false;
                if (_nodes != null)
                {
                    QuadNode p = _nodes;
                    while (!Object.Equals(p.Next.Node, node) && p.Next != _nodes)
                    {
                        p = p.Next;
                    }
                    if (Object.Equals(p.Next.Node, node))
                    {
                        rc = true;
                        QuadNode n = p.Next;
                        if (p == n)
                        {
                            // list goes to empty
                            _nodes = null;
                        }
                        else
                        {
                            if (_nodes == n)
                            {
                                _nodes = p;
                            }

                            p.Next = n.Next;
                        }
                    }
                }
                return rc;
            }

            /// <summary>
            /// The maximum priority for this quadrant's and all of its subquadrants' nodes.
            /// </summary>
            /// <returns>The maximum priority for this quadrant's and all of its subquadrants' nodes.</returns>
            /// <remarks>This call assumes that the potential is correctly set on the subquadrants.</remarks>
            private double CalculatePotential()
            {
                double potential = Double.NegativeInfinity;
                if (_nodes != null)
                {
                    potential = _nodes.Next.Priority;
                }

                if (_topLeft != null)
                {
                    potential = Math.Max(potential, _topLeft._potential);
                }

                if (_topRight != null)
                {
                    potential = Math.Max(potential, _topRight._potential);
                }

                if (_bottomLeft != null)
                {
                    potential = Math.Max(potential, _bottomLeft._potential);
                }

                if (_bottomRight != null)
                {
                    potential = Math.Max(potential, _bottomRight._potential);
                }

                return potential;
            }

            /// <summary>
            /// Enumerates over all nodes within this quadrant in random order.
            /// </summary>
            /// <returns>
            /// Enumerator that enumerates over all its nodes.
            /// </returns>
            public IEnumerator<QuadNode> GetEnumerator()
            {
                var queue = new Queue<Quadrant>();
                queue.Enqueue(this);

                while (queue.Count > 0)
                {
                    var quadrant = queue.Dequeue();
                    if (quadrant._nodes != null)
                    {
                        var n = quadrant._nodes;
                        do
                        {
                            n = n.Next;
                            yield return n;
                        }
                        while (n != quadrant._nodes);
                    }

                    if (quadrant._topLeft != null)
                    {
                        queue.Enqueue(quadrant._topLeft);
                    }

                    if (quadrant._topRight != null)
                    {
                        queue.Enqueue(quadrant._topRight);
                    }

                    if (quadrant._bottomLeft != null)
                    {
                        queue.Enqueue(quadrant._bottomLeft);
                    }

                    if (quadrant._bottomRight != null)
                    {
                        queue.Enqueue(quadrant._bottomRight);
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        /// <summary>
        /// The extent defines the subdivisible bounds of the quad tree index.
        /// </summary>
        private Rect _extent;

        /// <summary>
        /// The outer PriorityQuadTree class is essentially just a wrapper around a tree of Quadrants.
        /// </summary>
        private Quadrant _root;

        /// <summary>
        /// The MaxTreeDepth limit is required since recursive calls can go that deep if item bounds (height or width) are very small compared to Extent (height or width).
        /// The max depth will prevent stack overflow exception in some of the recursive calls we make.
        /// With a value of 50 the item bounds can be 2^-50 times the extent before the tree stops growing in height.
        /// </summary>
        private const int MaxTreeDepth = 50;

        /// <summary>
        /// The extent determines the overall quad-tree indexing strategy.
        /// Changing this bounds is expensive since it has to re-divide the entire thing - like a re-hash operation.
        /// </summary>
        public Rect Extent
        {
            get
            {
                return _extent;
            }
            set
            {
                if (!(value.Top >= double.MinValue &&
                      value.Top <= double.MaxValue &&
                      value.Left >= double.MinValue &&
                      value.Left <= double.MaxValue &&
                      value.Width <= double.MaxValue &&
                      value.Height <= double.MaxValue))
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                _extent = value;
                ReIndex();
            }
        }

        /// <summary>
        /// Insert an item with given bounds and priority into this QuadTree.
        /// </summary>
        /// <param name="item">The item to insert.</param>
        /// <param name="bounds">The bounds of this item.</param>
        /// <param name="priority">The priority to return this item before others in query results.</param>
        public void Insert(T item, Rect bounds, double priority)
        {
            if (bounds.Top.IsNaN() ||
                bounds.Left.IsNaN() ||
                bounds.Width.IsNaN() ||
                bounds.Height.IsNaN())
            {
                throw new ArgumentOutOfRangeException("bounds");
            }

            if (_root == null)
            {
                _root = new Quadrant(_extent);
            }

            if (Double.IsNaN(priority))
            {
                priority = Double.NegativeInfinity;
            }

            _root.Insert(item, bounds, priority, 1);
        }

        /// <summary>
        /// Gets whether any items are fully inside the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test.</param>
        /// <returns><c>true</c> if any items are inside the given bounds; otherwise, <c>false</c>.</returns>
        public bool HasItemsInside(Rect bounds)
        {
            if (bounds.Top.IsNaN() ||
                bounds.Left.IsNaN() ||
                bounds.Width.IsNaN() ||
                bounds.Height.IsNaN())
            {
                throw new ArgumentOutOfRangeException("bounds");
            }

            if (_root != null)
            {
                return _root.HasNodesInside(bounds);
            }
            return false;
        }

        /// <summary>
        /// Get a list of the items that are fully inside the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test.</param>
        /// <returns>The items that are inside the given bounds, returned in the order given by the priority assigned during Insert.</returns>
        public IEnumerable<T> GetItemsInside(Rect bounds)
        {
            if (bounds.Top.IsNaN() ||
                bounds.Left.IsNaN() ||
                bounds.Width.IsNaN() ||
                bounds.Height.IsNaN())
            {
                throw new ArgumentOutOfRangeException();
            }

            if (_root != null)
            {
                foreach (var node in _root.GetNodesInside(bounds))
                {
                    yield return node.Item1.Node;
                }
            }
        }

        /// <summary>
        /// Gets whether any items intersect the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test.</param>
        /// <returns><c>true</c> if any items intersect the given bounds; otherwise, <c>false</c>.</returns>
        public bool HasItemsIntersecting(Rect bounds)
        {
            if (bounds.Top.IsNaN() ||
                bounds.Left.IsNaN() ||
                bounds.Width.IsNaN() ||
                bounds.Height.IsNaN())
            {
                throw new ArgumentOutOfRangeException("bounds");
            }

            if (_root != null)
            {
                return _root.HasNodesIntersecting(bounds);
            }
            return false;
        }

        /// <summary>
        /// Get list of nodes that intersect the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test.</param>
        /// <returns>The items that intersect the given bounds, returned in the order given by the priority assigned during Insert.</returns>
        public IEnumerable<T> GetItemsIntersecting(Rect bounds)
        {
            if (bounds.Top.IsNaN() ||
                bounds.Left.IsNaN() ||
                bounds.Width.IsNaN() ||
                bounds.Height.IsNaN())
            {
                throw new ArgumentOutOfRangeException();
            }

            if (_root != null)
            {
                foreach (var node in _root.GetNodesIntersecting(bounds))
                {
                    yield return node.Item1.Node;
                }
            }
        }

        /// <summary>
        /// Removes the first instance of the given item from the tree (if it exists) by searching through the entire tree for the item.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns><c>true</c> if the item was found and removed; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This overload does a full search through the entire tree for the item.
        /// Clients should instead call the overload that takes a <see cref="Rect"/> if the bounds of the item are known.
        /// </remarks>
        public bool Remove(T item)
        {
            return Remove(item, new Rect(double.NegativeInfinity, double.NegativeInfinity, double.PositiveInfinity, double.PositiveInfinity));
        }

        /// <summary>
        /// Removes the first instance of the given item that intersects the given bounds from the tree (if it exists).
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <param name="bounds">The bounds within to search for the item.</param>
        /// <returns><c>true</c> if the item was found and removed; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This overload does a partial search through the tree, so if the <paramref name="bounds"/> do not intersect the node then the node will be missed.
        /// Clients should instead call the overload that does not take a <see cref="Rect"/> if the bounds of the item are not known.
        /// </remarks>
        public bool Remove(T item, Rect bounds)
        {
            if (bounds.Top.IsNaN() ||
                bounds.Left.IsNaN() ||
                bounds.Width.IsNaN() ||
                bounds.Height.IsNaN())
            {
                throw new ArgumentOutOfRangeException("bounds");
            }

            if (_root != null)
            {
                return _root.Remove(item, bounds);
            }
            return false;
        }

        /// <summary>
        /// Removes all nodes from the tree.
        /// </summary>
        public void Clear()
        {
            _root = null;
        }

        /// <summary>
        /// Rebuilds all the Quadrants according to the current QuadTree Bounds.
        /// </summary>
        private void ReIndex()
        {
            Quadrant quadrant = _root;
            _root = new Quadrant(_extent);
            if (quadrant != null)
            {
                foreach (var node in quadrant.GetNodesIntersecting(_extent))
                {
                    // Todo: It would be more efficient if we added a code path that allowed reuse of the QuadNode wrappers.
                    Insert(node.Item1.Node, node.Item1.Bounds, node.Item1.Priority);
                }
            }
        }

        /// <summary>
        /// Returns all items in the tree in unspecified order.
        /// </summary>
        /// <returns>An enumerator over all items in the tree in random order.</returns>
        /// <remarks>To get all items in the tree in prioritized-order then simply call <see cref="GetItemsInside"/> with an infinitely large rectangle.</remarks>
        public IEnumerator<T> GetEnumerator()
        {
            if (_root != null)
            {
                foreach (var node in _root)
                {
                    yield return node.Node;
                }
            }
        }

        /// <summary>
        /// Returns all items in the tree in unspecified order.
        /// </summary>
        /// <returns>An enumerator over all items in the tree in random order.</returns>
        /// <remarks>To get all items in the tree in prioritized-order then simply call <see cref="GetItemsInside"/> with an infinitely large rectangle.</remarks>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        } 
    }
}