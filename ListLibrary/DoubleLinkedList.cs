﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ListLibrary
{
    public class DoubleLinkedList<T>: GenericList<T>
    {
        public DoubleLinkedList()
        {
            start = null;
            end = null;
            count = 0;
        }

        public override void InsertAtEnd(T value)
        {
            Node<T> new_node = new Node<T>();
            new_node.value = value;

            if (isEmpty())
            {
                start = new_node;
                end = new_node;
            }
            else
            {
                end.next = new_node;
                new_node.prev = end;
                end = new_node;
            }
            count++;
        }

        public override void ExtractAtStart()
        {
            
            start = start.next;

            if (count == 1)
            {
                end = start;
            }
            else
            {
                start.prev = null;
            }
            count--;
           
        }

        public override void ExtractAtEnd()
        {
            Node<T> temp;
            if (count == 1)
            {
                end = end.next;
                start = end;
            }
            else
            {
                Node<T> pretemp = start;
                temp = pretemp.next;
                while (temp != end)
                {
                    pretemp = temp;
                    temp = pretemp.next;
                }
                pretemp.next = temp.next;
                end = pretemp;
            }
            count--;            
        }

        public override void ExtractAtPosition(int position)
        {
            Node<T> temp;
            if (count == 1 || position == 0)
            {
                ExtractAtStart();
            }
            else
            {
                if (position >= count-1)
                {
                    ExtractAtEnd();
                }
                else
                {
                    Node<T> pretemp = start;
                    temp = pretemp.next;
                    int index = 1;
                    while ((temp != null) && (index < position))
                    {
                        pretemp = temp;
                        temp = pretemp.next;
                        index++;
                    }
                    pretemp.next = temp.next;
                    temp.next.prev = pretemp;

                    count--;
                }
            }            
        }

        bool isEmpty()
        {
            bool empty = true;
            if (count != 0)
            {
                empty = false;
            }
            return empty;
        }        
    }
}
