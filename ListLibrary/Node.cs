using System;

namespace ListLibrary
{
    public class Node<T>
    {
        public T value;
        public Node<T> next;
        public Node<T> prev;

        public Node()
        {
            value = default(T);
            next = null;
            prev = null;
        }
    }
}
