using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteTool : SelectableItem
{
    static List<QuickOutline> _outlineItems;

    static public void ClearOutlines()
    {
        if (_outlineItems == null)
        {
            StartOutlineItems();
            return;
        }

        _outlineItems.Clear();
    }

    static void StartOutlineItems()
    {
        if (_outlineItems == null)
        {
            _outlineItems = new List<QuickOutline>();
        }
    }

    static public void AddOutineItem(QuickOutline outline)
    {
        if (!_outlineItems.Contains(outline))
        {
            _outlineItems.Add(outline);
        }
    }

    private void Start()
    {
        _toggle?.onValueChanged.AddListener(delegate { SetSelected(); UseOutline(); });
    }

    public void Update()
    {
        if (!m_selected)
        {
            return;
        }

        if (!Input.GetMouseButtonUp(0))
        {
            return;
        }

        RaycastHit hit;
        Ray ray = CameraManager.Instance.RaycastCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            DeleteObject(hit);
        }
    }

    void UseOutline()
    {
        if (_outlineItems == null)
        {
            return;
        }

        for (int i = _outlineItems.Count - 1; i >= 0; i--)
        {
            if (_outlineItems[i] == null)
            {
                _outlineItems.RemoveAt(i);
            }
            else
            {
                _outlineItems[i].enabled = _toggle.isOn;
            }
        }
    }

    public void DeleteObject(RaycastHit hit)
    {
        if (!hit.collider.CompareTag(CakeManager.Instance.DecorationTag))
        {
            return;
        }

        Destroy(hit.collider.gameObject);

        if (_deselectAfterUse)
        {
            GetComponent<Toggle>().isOn = false;
        }
    }
}
