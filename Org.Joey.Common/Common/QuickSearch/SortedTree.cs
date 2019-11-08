using System;
using System.Collections.Generic;
using System.Text;

namespace Org.Joey.Common
{
    /// <summary>
    /// 排序二叉树，实现二叉树查找，插入，该类可以用于关键字最长匹配
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SortedTree<T>
        where T : ISortedTreeNodeKey
    {
        public SortedTreeNode<T> Node { get; set; }
        public void Add(T item)
        {

        }
        public void Remove(T item)
        {

        }
        public IEnumerable<T> Query(string text)
        {
            return new T[] { };
        }
        public IEnumerable<T> ToList()
        {
            if (this.Node == null) yield break;
            yield return this.Node.Data;
            foreach (var item in Read(this.Node.Left))
            {
                yield return item;
            }
            foreach (var item in Read(this.Node.Right))
            {
                yield return item;
            }
        }
        private IEnumerable<T> Read(SortedTreeNode<T> node)
        {
            if (node == null) yield break;
            yield return node.Data;
            foreach (var left in Read(node.Left))
            {
                yield return left;
            }
            foreach (var right in Read(node.Right))
            {
                yield return right;
            }
        }

    }
}
