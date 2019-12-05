using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircularLinkedList
{
    /// <summary>
    /// A linked list collection capable of basic operations such as 
    /// Add, Remove, Find and Enumerate
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedList<T> : ICollection<T>
    {
        public int Count { get; private set;}

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(T item)
        {
            AddFirst(item);
        }

        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            LinkedListNode<T> current = Head;
            while (current != null && current.Previous != Tail)
            {
                // Head -> 3 -> 5 -> 7
                // Value: 5
                if (current.Value.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            LinkedListNode<T> current = Head;
            while (current != null && current.Previous != Tail)
            {
                array[arrayIndex++] = current.Value;
                current = current.Next;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        public bool Remove(T item)
        {
            LinkedListNode<T> previous = null;
            LinkedListNode<T> current = Head;

            // 1: Empty list - do nothing
            // 2: Single node: (previous is null)
            // 3: Many nodes
            //    a: node to remove is the first node
            //    b: node to remove is the middle or last

            while (current != null && previous != Tail)
            {
                // Head -> 3 -> 5 -> 7 -> null
                // Head -> 3 ------> 7 -> null
                if (current.Value.Equals(item))
                {
                    // it's a node in the middle or end
                    if (previous != null && previous != Tail)
                    {
                        // Case 3b
                        previous.Next = current.Next;

                        // it was the end - so update Tail
                        if (current.Next == null)
                        {
                            Tail = previous;
                        }
                        else if (current.Next == Head)
                        {
                            Tail = previous;
                            previous.Next = Head;
                            Head.Previous = Tail;
                        }
                        else
                        {
                            // Before: Head -> 3 <-> 5 <-> 7 -> null
                            // After:  Head -> 3 <-------> 7 -> null

                            // previous = 3
                            // current = 5
                            // current.Next = 7
                            // So... 7.Previous = 3
                            current.Next.Previous = previous;
                        }

                        Count--;
                    }
                    else
                    {
                        // Case 2 or 3a                        
                        RemoveFirst();
                    }

                    return true;
                }

                previous = current;
                current = current.Next;
            }

            return false;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            LinkedListNode<T> current = Head;
            
            while (current != null && current.Previous != Head)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        public void RemoveLast()
        {
            if (Count != 0)
            {
                if (Count == 1)
                {
                    Head = null;
                    Tail = null;
                }
                else if (Count == 2)
                {
                    // Before: Head --> 3 --> 5 
                    //         Tail = 5
                    // After:  Head --> 3 
                    //         Tail = 3
                    // Null out 5's Next pointer
                    Tail.Previous.Next = null;
                    Tail = Tail.Previous;
                    Tail.Previous = null;
                }
                else
                {
                    // Before: Head --> 3 --> 5 --> 7
                    //         Tail = 7
                    // After:  Head --> 3 --> 5 --> null
                    //         Tail = 5
                    // Null out 5's Next pointer
                    Tail.Previous.Next = Head;
                    Tail = Tail.Previous;
                    Head.Previous = Tail;
                }

                Count--;
            }
        }

        public void RemoveFirst()
        {            
            if (Count != 0)
            {
                // Before: Head -> 3 <-> 5
                // After:  Head -------> 5

                // Head -> 3 -> null
                // Head ------> null
                Head = Head.Next;

                Count--;

                if (Count == 0)
                {
                    Tail = null;
                }
                else if (Count == 1)
                {
                    // 5.Previous was 3, now null
                    Head.Previous = null;
                    Head.Next = null;
                }
                else
                {
                    Head.Previous = Tail;
                    Tail.Next = Head;                    
                }
            }           
        }

        public void AddLast(LinkedListNode<T> node)
        {
            if (Count == 0)
            {
                Head = node;
                Tail = node;
            }
            else
            {
                Tail.Next = node;
                node.Next = Head;
                // Before: Head -> 3 <-> 5 -> null
                // After:  Head -> 3 <-> 5 <-> 7 -> null
                // 7.Previous = 5
                node.Previous = Tail;                
            }

            Tail = node;
            Head.Previous = Tail;
            Count++;
        }

        public void AddLast(T value)
        {
            AddLast(new LinkedListNode<T>(value));
        }

        public void AddFirst(LinkedListNode<T> node)
        {
            // Save off the head node so we don't lose it
            LinkedListNode<T> temp = Head;

            // Point head to the new node
            Head = node;

            // Insert the rest of the list behind the head
            Head.Next = temp;

            if (Count == 0)
            {
                // if the list was empty then Head and Tail should
                // both point to the new node.
                Tail = Head;
            }
            else
            {
                // Before: Head -------> 5 <-> 7 -> null
                // After:  Head -> 3 <-> 5 <-> 7 -> null

                // temp.Previous was null, now Head
                temp.Previous = Head;
                Head.Previous = Tail;
                Tail.Next = Head;  
            }

            Count++;
        }

        public void AddFirst(T value)
        {
            AddFirst(new LinkedListNode<T>(value));
        }

        public LinkedListNode<T> Head
        {
            get;
            private set;
        }

        /// <summary>
        /// The last node in the list or null if empty
        /// </summary>
        public LinkedListNode<T> Tail
        {
            get;
            private set;
        }
    }

    public class LinkedListNode<T>
    {
        public LinkedListNode<T> Next { get; set; }
        public T Value { get; set; }
        public LinkedListNode<T> Previous { get; set; }
        public LinkedListNode(T value)
        {
            Value = value;
        }
    }
}
