using System;
using System.Collections.Generic;
using System.Text;

namespace Org.Joey.Common
{
    public class SortedTreeNode<T>
        where T : ISortedTreeNodeKey
    {
        public T Data { get; set; }
        public SortedTreeNode<T> Left { get; set; }
        public SortedTreeNode<T> Right { get; set; }
    }
}
