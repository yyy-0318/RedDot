using UnityEngine;

namespace YYyanFramework
{
    public class Node
    {
        int m_influencedCount;
        private int refCount;
        public int RefCount
        {
            get
            {
                return this.refCount;
            }
            set
            {
                if (value < 0)
                {
                    Debug.LogError(string.Format("{0} refcount: {1}", this.m_name, value));
                    return;
                }
                this.refCount = value;
                if (refCount < m_influencedCount)
                {
                    refCount = m_influencedCount;
                }
                nodeActive();
            }
        }
        public string m_name { get; set; }
        public string m_parent { get; set; }
        GameObject m_node;
        UnityEngine.UI.Text m_nodeText;
        bool close = false;
        public Node()
        {
            RefCount = 0;
            m_influencedCount = 0;
            close = false;
        }
        /// <summary>
        /// 初始化节点名字
        /// </summary>
        /// <param name="parent">父节点名称,最上级节点可无父节点</param>
        /// <param name="name">节点名称</param>
        public void InitName(string parent, string name)
        {
            m_parent = parent;
            m_name = name;        
            nodeActive();
        }
        /// <summary>
        /// 初始化节点对象
        /// </summary>
        /// <param name="node">节点对象</param>
        public void InitNodeObj(GameObject node)
        {
            m_node = node;
            m_nodeText = m_node.GetComponent<UnityEngine.UI.Text>();
            nodeActive();
        }
        /// <summary>
        /// 清除父节点的连接
        /// </summary>
        public void ClearParentLink()
        {
            if (RefCount > 0 && !string.IsNullOrEmpty(m_parent))
            {
                m_parent = string.Empty;
                RedDotRemind.instance.Reduce(m_parent, true);
            }
        }
        /// <summary>
        /// 增加计数
        /// </summary>
        public void Increase(bool influenced)
        {
            if (influenced)
            {
                ++m_influencedCount;
            }
            ++RefCount;
        }
        /// <summary>
        /// 减少计数
        /// </summary>
        public void Reduce(bool influenced)
        {
            if (influenced && RefCount >= 1)
            {
                --m_influencedCount;
            }
            --RefCount;
        }
        /// <summary>
        /// 无论增加多少次，只计算1
        /// </summary>
        public void OnlyIncrease()
        {
            RefCount= m_influencedCount+1;
        }
        /// <summary>
        /// 减少即清零
        /// </summary>
        public void OnlyReduce()
        {
            RefCount = m_influencedCount;
        }
        /// <summary>
        /// 清除计数
        /// </summary>
        public void ClearCount()
        {
            RefCount = 0;
            m_influencedCount = 0;
            if (m_node != null)
            {
                //m_node.SetActive(false);
                m_nodeText.color = Color.white;
                m_nodeText.text = RefCount.ToString();
            }
        }
        /// <summary>
        /// 节点显示状态
        /// </summary>
        void nodeActive()
        {
            if (m_node == null)
            {
                return;
            }
            bool active = RefCount > 0;
            //if (m_node.activeSelf!= active)
            //{
            //    m_node.SetActive(active);
            //    //父节点计数加减
            //    if (!string.IsNullOrEmpty(m_parent))
            //    {
            //        if (active)
            //        {
            //            RedDotRemind.instance.Increase(m_parent);
            //        }
            //        else
            //        {
            //            RedDotRemind.instance.Reduce(m_parent);
            //        }
            //    }          
            //}
            if (close != active)
            {
                close = active;
                if (!string.IsNullOrEmpty(m_parent))
                {
                    if (active)
                    {
                        RedDotRemind.instance.Increase(m_parent, true);
                    }
                    else
                    {
                        RedDotRemind.instance.Reduce(m_parent, true);
                    }
                }
            }
            m_nodeText.text = RefCount.ToString();

        }
    }
}