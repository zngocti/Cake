using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableItem : MonoBehaviour
{
    public bool m_selected;

    public GameObject m_indicator = null;

    public void SetSelectedIcing()
    {
        foreach (SelectableItem item in GameObject.FindObjectsOfType<SelectableItem>())
        {
            item.m_selected = false;
        }

        m_selected = true;
    }

    public void SetSelected()
    {
        if (TopSyrupController._instanceTool)
        {
            TopSyrupController._instanceTool.GetComponent<Animator>().SetTrigger("Return");
            Destroy(TopSyrupController._instanceTool, 1);
        }

        foreach (SelectableItem item in GameObject.FindObjectsOfType<SelectableItem>())
        {
            item.m_selected = false;
        }

        m_selected = true;
    }
}
