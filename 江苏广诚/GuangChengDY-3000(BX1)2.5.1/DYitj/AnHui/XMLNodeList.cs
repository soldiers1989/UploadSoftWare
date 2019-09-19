using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIO.AnHui
{
    public class XMLNodeList : ArrayList
    {
        public XMLNode Pop()
        {
            XMLNode item = null;

            item = (XMLNode)this[this.Count - 1];
            this.Remove(item);

            return item;
        }

        public int Push(XMLNode item)
        {
            Add(item);

            return this.Count;
        }
    }
}
