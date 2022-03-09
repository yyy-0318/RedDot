using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YYyanFramework;

public class TestDot : MonoBehaviour {

    bool select = false;

    public Button m_Start;
    public Button m_End;
    public Button m_Increase;
    public Button m_Reduce;

    List<GameObject> selectNode = new List<GameObject>();
    GameObject m_node;
    void Start ()
    {
        for (int i = 0, length = transform.childCount; i < length; i++)
        {
            GameObject node = transform.GetChild(i).gameObject;
            node.GetComponent<Button>().onClick.AddListener(() => { ClickAddNode(node); });       
        }
        m_Start.onClick.AddListener(Begin);
        m_End.onClick.AddListener(End);
        m_Increase.onClick.AddListener(ClickNodeIncrease);
        m_Reduce.onClick.AddListener(ClickNodeReduce);

        m_Start.gameObject.SetActive(true);
    }

    void ClickAddNode(GameObject node)
    {
        if (select)
        {
            if (!selectNode.Contains(node))
            {
                selectNode.Add(node);
                node.transform.GetChild(0).GetComponent<Text>().text="选中";
            }
        }
        else
        {
            if (m_node != null)
            {
                m_node.GetComponent<Image>().color = Color.white;
            }
            m_node = node;
            m_node.GetComponent<Image>().color = Color.red;
        }
       
    }
    public void ClickNodeIncrease()
    {
        if (m_node!=null)
        {
            RedDotRemind.instance.Increase(m_node.name);
        }
      
    }
    public void ClickNodeReduce()
    {
        if (m_node != null)
        {
            RedDotRemind.instance.Reduce(m_node.name);
        }     
    }
    public void Begin()
    {
        m_End.gameObject.SetActive(true);
        m_Start.gameObject.SetActive(false);
        select = true;
        selectNode.Clear();
    }
    public void End()
    {
        select = false;
        if (selectNode.Count<2)
        {
            selectNode.Clear();
            Debug.LogError("请选择2个以上的节点");
        }
        else
        {
            m_End.gameObject.SetActive(false);
            m_Increase.gameObject.SetActive(true);
            m_Reduce.gameObject.SetActive(true);

            string tree = "";
            for (int i = 0; i < selectNode.Count; i++)
            {
                tree += selectNode[i].name;
                tree += "/";
            }
            Color color = new Color(Random.Range(0.1f,1f), Random.Range(0f, 1f), Random.Range(0.5f, 1f), 1);
            RedDotRemind.instance.AddNodeTree(tree);
            for (int i = 0; i < selectNode.Count; i++)
            {
                Text text = selectNode[i].transform.GetChild(0).GetComponent<Text>();
                text.color = color;
                RedDotRemind.instance.AddNodeObj(selectNode[i].name, text.gameObject);
            }
        }
    }
    public void Clear()
    {
        RedDotRemind.instance.Clear();
    }
}
