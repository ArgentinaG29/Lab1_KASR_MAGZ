using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ListLibrary
{
    public class GenericList<T>: IEnumerable<T>
    {
        protected Node<T> start;
        protected Node<T> end;
        protected int count;
        private int getCount;

        public GenericList()
        {
            start = null;
            end = null;
            count = 0;
        }

        public virtual void InsertAtStart(T value) { }
        
        public virtual void InsertAtEnd(T value) { }

        public virtual void InsertAtPosition(T value, int position) { }

        public virtual void ExtractAtStart() { }

        public virtual void ExtractAtEnd() { }

        public virtual void ExtractAtPosition(int position) { }

        public virtual int GetCount { get => getCount; set => getCount = value; }

        public IEnumerator<T> GetEnumerator()
        {
            var node = start;
            while(node!=null)
            {
                yield return node.value;
                node = node.next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
