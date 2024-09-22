using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ThemeItem : SelectableItem
{
    public int m_matIndex;
    public GameObject m_themeItem;
    public Material m_material;

    [SerializeField]
    bool _instantUse = true;

    private void Start()
    {
        _deselectAfterUse = true;
        _toggle?.onValueChanged.AddListener(delegate { TrySetSelected(); });
    }

    public void Update()
    {
        if (!m_selected || _instantUse)
        {
            return;
        }

        if (GameManager.Instance.CakeMoving)
        {
            return;
        }

        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        RaycastHit hit;
        Ray ray = CameraManager.Instance.RaycastCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (!hit.collider.tag.Contains("Cake"))
                return;

            if (EventSystem.current.currentSelectedGameObject != null)
            {
                return;
            }

            /*
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }*/
            if (m_selected)
            {
                if (!ThemeItemsCounter.Instance.IsCurrent(_selectableItemID))
                {
                    SetSideModel();
                }
            }
        }
    }

    void TrySetSelected()
    {
        SetSelected();

        if (_instantUse)
        {
            if (_toggle.isOn)
            {
                if (!ThemeItemsCounter.Instance.IsCurrent(_selectableItemID))
                {
                    SetSideModel();
                }
                else
                {
                    _toggle.isOn = false;
                }
            }
        }
    }

    public void SetSideModel()
    {
        string cakeName = string.Empty;

        // Check if we are in a tiered cake or not
        if (GameManager.Instance.m_cakeType == CAKETYPES.Three_Tier)
        {
            cakeName = "Cake_Sml";
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Two_Tier)
        {
            cakeName = "Cake_Med";
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Large)
        {
            cakeName = "Cake_Lrg";
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Medium)
        {
            cakeName = "Cake_Med";
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Small)
        {
            cakeName = "Cake_Sml";
        }

        if (!string.IsNullOrEmpty(cakeName))
        {
            ThemeItemPlaceCommand themeCommand = new ThemeItemPlaceCommand(m_themeItem, GameObject.Find(cakeName).transform, m_material, m_matIndex);
            (GameObject, int) oldItem = ThemeItemsCounter.Instance.AddNewItemToList((m_themeItem, _selectableItemID));
            themeCommand.SetItemOverriden(oldItem);
            ICommand command = themeCommand;
            CommandInvoker.UndoItem(oldItem.Item1, false);
            CommandInvoker.AddCommand(command);
        }

        if (_deselectAfterUse)
        {
            _toggle.isOn = false;
        }
    }
}
