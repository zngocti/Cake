using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableItem : MonoBehaviour
{
    static int _currentSelectableID = 0;

    public bool m_selected;

    public GameObject m_indicator = null;

    protected int _selectableItemID = 0;

    //this ID is for each button, all the items placed with the same button have the same selectable item ID
    //do not confuse with the item ID which is unique
    static int GetNewSelectableItemID()
    {
        _currentSelectableID++;
        return _currentSelectableID;
    }

    private void Awake()
    {
        _selectableItemID = GetNewSelectableItemID();
    }

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
