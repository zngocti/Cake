using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages tabs in a group.
/// </summary>
public class TabGroup : MonoBehaviour
{
    // The list of tabs in this group
    public List<TabButton> m_tabButtons;
    // Selected tab
    public TabButton m_selectedTab;

    public List<GameObject> m_objectsToSwap;

    // When a tab subscribes to this group, add it to the list
    public void Subscribe(TabButton _tabButton)
    {
        if (m_tabButtons == null)
        {
            m_tabButtons = new List<TabButton>();
        }
        m_tabButtons.Add(_tabButton);
    }

    public void OnTabEnter(TabButton _tabButton)
    {
       ResetTabs();
        if (m_selectedTab == null || _tabButton != m_selectedTab)
        {
            _tabButton.m_tabImage.color = new Color(245.0f / 255.0f, 245.0f / 255.0f, 245.0f / 255.0f, 1.0f);
        }
    }

    public void OnTabExit(TabButton _tabButton)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton _tabButton)
    {
        m_selectedTab = _tabButton;
        ResetTabs();
        _tabButton.m_tabImage.sprite = _tabButton.m_selectedGlass;
        _tabButton.ChildTabImage.sprite = _tabButton.m_selectedIcon;

        _tabButton.m_tabImage.color = SyrupSelector.Instance.m_selected;

        int index = _tabButton.transform.GetSiblingIndex();

        for (int i = 0; i < m_objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                m_objectsToSwap[i].SetActive(true);
            }
            else
            {
                m_objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ColorSelectedTab(Color color)
    {
        if (m_selectedTab == null)
        {
            return;
        }

        m_selectedTab.m_tabImage.color = color;
    }

    public void ResetTabs()
    {
        foreach (TabButton _button in m_tabButtons)
        {
            // Skip through the selected tab
            if (m_selectedTab != null && _button == m_selectedTab)
            {
                continue;
            }
            _button.m_tabImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            _button.m_tabImage.sprite = _button.m_deselectedGlass;
            _button.ChildTabImage.sprite =_button.m_deselectedIcon;
        }
    }
}
