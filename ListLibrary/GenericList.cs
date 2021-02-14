using System;
using System.Collections.Generic;
using System.Text;

namespace ListLibrary
{
    public class GenericList<T>
    {
        protected Node<T> start;
        protected Node<T> end;
        protected int count;

        public GenericList()
        {
            start = null;
            end = null;
            count = 0;
        }

        public virtual void InsertAtEnd(T value) { }
                            
        public virtual void ExtractAtStart() { }

        public virtual void ExtractAtEnd() { }

        public virtual void ExtractAtPosition(int position) { }
        
    }
}
