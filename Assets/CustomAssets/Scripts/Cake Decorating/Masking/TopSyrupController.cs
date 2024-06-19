using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopSyrupController : MonoBehaviour
{
    public GameObject m_toolPrefab;
    private GameObject m_tool;

    public void ToggleTool()
    {
        if (!m_tool)
        {
            m_tool = Instantiate(m_toolPrefab);
        }
        else
        {
            m_tool.GetComponent<Animator>().SetTrigger("Return");
            Destroy(m_tool, 1);
        }
    }
}
