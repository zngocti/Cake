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

    private void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener(delegate { SetSelected(); });
    }

    public void Update()
    {
        if (!m_selected)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = CameraManager.Instance.RaycastCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (!hit.collider.tag.Contains("Cake"))
                return;

            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                if (m_selected)
                {
                    SetSideModel();
                    CameraManager.Instance.ZoomOut();
                    GetComponent<Toggle>().isOn = false;
                }
            }
        }
    }

    public void SetSideModel()
    {
        if (ThemeItemsCounter.Instance.IsCurrent(m_themeItem))
        {
            return;
        }

        // Check if we are in a tiered cake or not
        if (GameManager.Instance.m_cakeType == CAKETYPES.Three_Tier)
        {
            ThemeItemPlaceCommand themeCommand = new ThemeItemPlaceCommand(m_themeItem, GameObject.Find("Cake_Sml").transform, m_material, m_matIndex);
            GameObject oldItem = ThemeItemsCounter.Instance.AddNewItemToList(m_themeItem);
            themeCommand.SetItemOverriden(oldItem);
            ICommand command = themeCommand;
            CommandInvoker.UndoItem(oldItem, false);
            CommandInvoker.AddCommand(command);
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Two_Tier)
        {
            ThemeItemPlaceCommand themeCommand = new ThemeItemPlaceCommand(m_themeItem, GameObject.Find("Cake_Med").transform, m_material, m_matIndex);
            GameObject oldItem = ThemeItemsCounter.Instance.AddNewItemToList(m_themeItem);
            themeCommand.SetItemOverriden(oldItem);
            ICommand command = themeCommand;
            CommandInvoker.UndoItem(oldItem, false);
            CommandInvoker.AddCommand(command);
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Large)
        {
            ThemeItemPlaceCommand themeCommand = new ThemeItemPlaceCommand(m_themeItem, GameObject.Find("Cake_Lrg(Clone)").transform, m_material, m_matIndex);
            GameObject oldItem = ThemeItemsCounter.Instance.AddNewItemToList(m_themeItem);
            themeCommand.SetItemOverriden(oldItem);
            ICommand command = themeCommand;
            CommandInvoker.UndoItem(oldItem,false);
            CommandInvoker.AddCommand(command);
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Medium)
        {
            ThemeItemPlaceCommand themeCommand = new ThemeItemPlaceCommand(m_themeItem, GameObject.Find("Cake_Med(Clone)").transform, m_material, m_matIndex);
            GameObject oldItem = ThemeItemsCounter.Instance.AddNewItemToList(m_themeItem);
            themeCommand.SetItemOverriden(oldItem);
            ICommand command = themeCommand;
            CommandInvoker.UndoItem(oldItem, false);
            CommandInvoker.AddCommand(command);
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Small)
        {
            ThemeItemPlaceCommand themeCommand = new ThemeItemPlaceCommand(m_themeItem, GameObject.Find("Cake_Sml(Clone)").transform, m_material, m_matIndex);
            GameObject oldItem = ThemeItemsCounter.Instance.AddNewItemToList(m_themeItem);
            themeCommand.SetItemOverriden(oldItem);
            ICommand command = themeCommand;
            CommandInvoker.UndoItem(oldItem, false);
            CommandInvoker.AddCommand(command);
        }
    }
}
