using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YYyanFramework
{
    public class RedDotRemind
    {
        private static readonly RedDotRemind ms_instance = new RedDotRemind();

        public static RedDotRemind instance
        {
            get { return ms_instance; }
        }
        Dictionary<string, Node> m_nodeDict = new Dictionary<string, Node>();
        /// <summary>
        /// 增加节点
        /// </summary>
        /// <param name="tree"</param>
        public void AddNodeTree(string tree)
        {
            if (string.IsNullOrEmpty(tree))
            {
                return;
            }
            string[] nodes = tree.Split('/');
            int length = nodes.Length - 1;
            while (length > 0)
            {
                Node tempNode;
                if (!m_nodeDict.TryGetValue(nodes[length], out tempNode))
                {
                    tempNode = new Node();
                    m_nodeDict.Add(nodes[length], tempNode);
                }
                if (length - 1 >= 0)
                {
                    tempNode.InitName(nodes[length - 1], nodes[length]);
                }
                else
                {
                    tempNode.InitName("", nodes[length]);
                }
                --length;
            }
        }
        /// <summary>
        /// 增加节点树
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="parent">父节点</param>
        public void AddNodeTree(string tree, string parent="")
        {
            if (string.IsNullOrEmpty(tree))
            {
                return;
            }
            string[] nodes = tree.Split('/');
            int length = nodes.Length - 1;
            while (length > 0)
            {
                Node tempNode;
                if (!m_nodeDict.TryGetValue(nodes[length], out tempNode))
                {
                    tempNode = new Node();
                    m_nodeDict.Add(nodes[length], tempNode);
                }
                if (length - 1 >= 0)
                {
                    tempNode.InitName(nodes[length - 1], nodes[length]);
                }
                else
                {
                    tempNode.InitName(parent, nodes[length]);
                }
                --length;
            }
        }
        /// <summary>
        /// 增加节点对象
        /// </summary>
        /// <param name="name">节点名</param>
        /// <param name="node">节点对象</param>
        /// 节点名字可以自定义，但必须要和关联节点树时的一致
        public void AddNodeObj(string name, GameObject node)
        {
            if (!node)
            {
                return;
            }
            Node tempNode;
            if (!m_nodeDict.TryGetValue(name, out tempNode))
            {
                tempNode = new Node();
                m_nodeDict.Add(name, tempNode);
            }
            tempNode.InitNodeObj(node);
        }
        /// <summary>
        /// 计数增加
        /// </summary>
        /// <param name="name">节点名</param>
        /// <param name="influenced">受子节点影响的计数</param>
        public void Increase(string name, bool influenced = false)
        {
            Node tempNode;
            if (!m_nodeDict.TryGetValue(name, out tempNode))
            {
                tempNode = new Node();
                m_nodeDict.Add(name, tempNode);
            }
            tempNode.Increase(influenced);
        }
        /// <summary>
        /// 计数减少(注意,受子节点计数影响，不可无增而减)
        /// </summary>
        /// <param name="name">节点名</param>
        /// <param name="influenced">受子节点影响的计数</param>
        public void Reduce(string name, bool influenced = false)
        {
            Node tempNode;
            if (!m_nodeDict.TryGetValue(name, out tempNode))
            {
                tempNode = new Node();
                m_nodeDict.Add(name, tempNode);
            }
            tempNode.Reduce(influenced);
        }
        /// <summary>
        /// 唯一计数增加(无论节点增加多少次计数，只算1个，基于受子节点影响的计数增加)
        /// </summary>
        /// <param name="name">节点名</param>
        public void OnlyIncrease(string name)
        {
            Node tempNode;
            if (!m_nodeDict.TryGetValue(name, out tempNode))
            {
                tempNode = new Node();
                m_nodeDict.Add(name, tempNode);
            }
            tempNode.OnlyIncrease();
        }
        /// <summary>
        /// 唯一计数减少(无论节点增加多少次计数，只算1个，所以减少即清空，不会清掉受子节点影响的计数)
        /// </summary>
        /// <param name="name">节点名</param>
        public void OnlyReduce(string name)
        {
            Node tempNode;
            if (!m_nodeDict.TryGetValue(name, out tempNode))
            {
                tempNode = new Node();
                m_nodeDict.Add(name, tempNode);
            }
            tempNode.OnlyReduce();
        }
        /// <summary>
        /// 清除所有
        /// </summary>
        public void Clear()
        {
            ClearCount();
            m_nodeDict.Clear();
        }
        /// <summary>
        /// 清除所有计数
        /// </summary>
        public void ClearCount()
        {
            foreach (var node in m_nodeDict)
            {
                node.Value.ClearCount();
            }
        }
        /// <summary>
        /// 清除单个节点计数
        /// </summary>
        /// <param name="name">节点名</param>
        public void ClearNodeCount(string name)
        {
            Node tempNode;
            if (m_nodeDict.TryGetValue(name, out tempNode))
            {
                tempNode.ClearCount();
            }
        }
        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="name">节点名</param>
        /// <returns></returns>
        public Node FindNode(string nodeName)
        {
            Node tempNode;
            if (m_nodeDict.TryGetValue(nodeName, out tempNode))
            {
                return tempNode;              
            }
            return tempNode;
        }
    }
}