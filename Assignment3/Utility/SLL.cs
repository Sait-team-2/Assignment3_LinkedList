using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.Utility
{
    //SLL class to implement ILinkedListADT Interface:
    [DataContract]
    public class SLL : ILinkedListADT
    {
        [DataMember]
        private Node head;
        
        [DataMember]
        private int count;
        
        //Constructor:
        public SLL()
        {
            //head references first node
            //count keeps track of all items in list
            head = null;
            count = 0;
        }
        //IsEmpty method: Checks if linked list is empty by checking count = 0
        //returns true if list is empty
        public bool IsEmpty()
        {
            return count == 0;
        }
        //clear method: Clears the list
        //Reset head, reset count
        public void Clear()
        {
            head = null;
            count = 0;
        }
        //AddLast method: Adds a new node with the value to the end of the list
        public void AddLast(User value)
        {
            Node newNode = new Node(value);
            //Set head to newNode if list IsEmpty:
            if (IsEmpty())
            {
                head = newNode;
            }
            else
            {
                Node current = head;
                //Moving throught nodes that aren't empty to get to the end:
                while (current.Next != null)
                {
                    current = current.Next;
                }
                //Set last node to newNode:
                current.Next = newNode;
            }
            count++;
        }
        //AddFirst method: Adds a new node with the value to the beginning of the list
        public void AddFirst(User value)
        {
            Node newNode = new Node(value);
            //Set head to newNode if list IsEmpty:
            if (IsEmpty())
            {
                head = newNode;
            }
            else
            {
                //set newNodes next head to current head:
                newNode.Next = head;
                //set head to newNode value:
                head = newNode;
            }
            count++;
        }
        //Add method: Adds a new node with the value to specified position in the linked list
        public void Add(User value, int index)
        {
            //Index out of range exception:
            if (index<0 || index >count)
            {
                throw new IndexOutOfRangeException("Index is out of range!");
            }
            //If index = 0 add value to first position:
            if (index == 0)
            {
                AddFirst(value);
            }
            //If index = count (last position) add value to last position:
            else if (index == count)
            {
                AddLast(value);
            }
            //All other positions:
            else
            {
                Node newNode = new Node(value);
                Node current = head;
                //For loop from index 0 to node before target index:
                for (int i = 0; i < index - 1; i++)
                {
                    //node before target index:
                    current = current.Next;
                }
                newNode.Next = current.Next;
                //Setting current's next to newNode value:
                current.Next = newNode;
                count++;
            }
        }
        //Replace method: Replaces value at specific position in linked list
        public void Replace(User value, int index)
        {
            //Index out of range exception:
            if(index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("Index is out of range!");
            }

            Node current = head;
            //For loop from index 0 to target node:
            for (int i = 0;i < index; i++)
            {
                //Target node:
                current = current.Next;
            }
            //Setting current node to value:
            current.Data = value;
        }
        //Count method: Returns total count of items in the list
        public int Count()
        {
            return count;
        }
        //RemoveFirst method: Removes first node from linked list
        public void RemoveFirst()
        {
            //Empty List, Invalid Operation Exception:
            if (IsEmpty())
            {
                throw new InvalidOperationException("Cannot Remove! List is empty!");
            }
            //Setting head to next node:
            head = head.Next;
            count--;
        }
        //RemoveLast method: removes last node from linked list
        public void RemoveLast()
        {
            //Empty List, Invalid Operation Exception:
            if (IsEmpty())
            {
                throw new InvalidOperationException("Cannot Remove! List is empty!");
            }
            //if only 1 node in list, set head to null:
            if (count == 1)
            {
                head = null;
            }
            //More than 1 node in list:
            else
            {
                Node current = head;
                //while loop goes to second last node:
                while (current.Next.Next != null)
                {
                    //second last node:
                    current = current.Next;
                }
                //sets last to null:
                current.Next = null;
            }
            count--;
        }
        //Remove method: Removes value from specified position in linked list
        public void Remove(int index)
        {
            //Index out of range exception:
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("Index is out of range!");
            }
            if (index == 0)
            {
                RemoveFirst();
            }
            else if (index == count - 1)
            {
                RemoveLast();
            }
            else
            {
                Node current = head;
                //for loop from index 0 to 1 node before target index:
                for (int i = 0; i < index - 1; i++)
                {
                    //node before target index:
                    current = current.Next;
                }
                //Set current's next to node after target index:
                current.Next = current.Next.Next;
                count--;
            }
        }
        //GetValue method: Gets the value at the specified index
        public User GetValue(int index)
        {
            //Index out of range exception:
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("Index is out of range!");
            }
            Node current = head;
            //for loop index 0 to target index:
            for (int i = 0; i < index; i++)
            {
                //Target index:
                current = current.Next;
            }
            return current.Data;
        }
        //IndexOf method: Returns the index of the first occurance of the value
        public int IndexOf(User value)
        {
            Node current = head;
            for (int i = 0; i < count; i++)
            {
                if (current.Data.Equals(value))
                {
                    //return index:
                    return i;
                }
                current = current.Next;
            }
            //if value is not found:
            return -1;
        }
        //Contains method: Go through nodes and check if one has value.
        public bool Contains(User value)
        {
            return IndexOf(value) != -1;
        }

        public User[] ToArray()
        {
            User[] array = new User[count];
            Node current = head;
            for (int i = 0; i < count; i++)
            {
                array[i] = current.Data;
                current = current.Next;
            }
            return array;
        }

        public void Reverse()
        {
            Node prev = null;
            Node current = head;
            Node next = null;
            while (current != null)
            {
                next = current.Next; 
                current.Next = prev; 
                prev = current; 
                current = next;
            }
            head = prev; 
        }

        public void Sort()
        {
            if (head == null || head.Next == null)
            {
                return; // The list is empty or has only one element, so it's already sorted.
            }

            bool wasChanged;
            do
            {
                Node current = head;
                Node previous = null;
                Node next = head.Next;
                wasChanged = false;

                while (next != null)
                {
                    if (current.Data.Name.CompareTo(next.Data.Name) > 0)
                    {
                        wasChanged = true;

                        if (previous != null)
                        {
                            Node temp = next.Next;
                            previous.Next = next;
                            next.Next = current;
                            current.Next = temp;
                        }
                        else
                        {
                            Node temp = next.Next;
                            head = next;
                            next.Next = current;
                            current.Next = temp;
                        }

                        previous = next;
                        next = current.Next;
                    }
                    else
                    {
                        previous = current;
                        current = next;
                        next = next.Next;
                    }
                }
            } while (wasChanged);
}
/*
        public void Join(params SLL[] lists)
        {
            foreach (SLL list in lists)
            {
                Node current = list.head;
                while (current != null)
                {
                    Add(current.Data); 
                    current = current.Next;
                }
            }
        }

        public (SLL, SLL) DivideAt(int index)
        {
            if (index < 0 || index >= Count) // Assuming there's a Count property
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be within the bounds of the linked list.");
            }

            SLL firstHalf = new SLL();
            SLL secondHalf = new SLL();
            Node current = this.head;
            int currentIndex = 0;

            while (current != null)
            {
                if (currentIndex < index)
                {
                    firstHalf.Add(current.Data);
                }
                else if (currentIndex == index)
                {
                    secondHalf.head = new Node(current.Data); // Initialize the second half's head
                    Node secondCurrent = secondHalf.head;
                    current = current.Next;
                    currentIndex++;
                    while (current != null)
                    {
                        secondCurrent.Next = new Node(current.Data); // Continue appending to the second half
                        secondCurrent = secondCurrent.Next;
                        current = current.Next;
                        currentIndex++;
                    }
                    break; // Exit the loop once the second half is fully populated
                }
                current = current.Next;
                currentIndex++;
            }

            return (firstHalf, secondHalf);
        }

*/
    }
}
