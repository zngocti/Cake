using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Tab button manager.
/// </summary>
[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public bool m_interactable = true;
    // Reference to tab group this tab belongs to
    public TabGroup m_tabGroup;
    // Image attached to this tab
    public Image m_tabImage;

    [SerializeField]
    Image _childTabImage;

    public Sprite m_deselectedGlass;
    public Sprite m_deselectedIcon;

    public Sprite m_selectedGlass;
    public Sprite m_selectedIcon;

    public Image ChildTabImage { get => _childTabImage; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_tabGroup.m_selectedTab == this)
        {
            m_tabGroup.m_selectedTab = null;
            m_tabGroup.OnTabExit(this);

            for (int i = 0; i < m_tabGroup.m_objectsToSwap.Count; i++)
            {
                m_tabGroup.m_objectsToSwap[i].SetActive(false);
            }
            return;
        }

        if (m_interactable)
        {
            m_tabGroup.OnTabSelected(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (m_interactable)
            m_tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (m_interactable)
            m_tabGroup.OnTabExit(this);
    }

    private void Start()
    {
        m_tabImage = GetComponent<Image>();
        m_tabGroup.Subscribe(this);
    }
}
