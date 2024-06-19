using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableItem : MonoBehaviour
{
    public bool m_selected;

    public GameObject m_indicator = null;

    public void SetSelected()
    {
        foreach (SelectableItem item in GameObject.FindObjectsOfType<SelectableItem>())
        {
            item.m_selected = false;
        }

        m_selected = true;
    }
}
