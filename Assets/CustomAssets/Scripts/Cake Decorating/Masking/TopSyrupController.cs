using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopSyrupController : SelectableItem
{
    public static GameObject _instanceTool;

    public GameObject m_toolPrefab;
    private GameObject m_tool;
    /*
    public void ToggleTool()
    {
        if (m_tool)
        {
            m_selected = false;

            m_tool.GetComponent<Animator>().SetTrigger("Return");
            Destroy(m_tool, 1);
        }
        else
        {
            SetSelectedIcing();

            if (_instanceTool)
            {
                m_tool = _instanceTool;
                CakeIcingTool myTool = m_toolPrefab.GetComponentInChildren<CakeIcingTool>();
                _instanceTool.GetComponentInChildren<CakeIcingTool>().SetVariables(myTool.m_largeDrops, myTool.m_medDrops, myTool.m_smallDrops);
            }
            else
            {
                m_tool = Instantiate(m_toolPrefab);
                _instanceTool = m_tool;
            }
        }
    }*/
}
