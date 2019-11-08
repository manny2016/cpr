using System;
using System.Collections.Generic;
using System.Text;

namespace Org.Joey.Common
{
    /// <summary>
    /// 排序二叉树工厂类
    /// </summary>
    public class SortedTreeFactory
    {
        public static SortedTree<T> Create<T>(T[] items)
            where T : ISortedTreeNodeKey
        {
            return new SortedTree<T>();
        }
    }
}
