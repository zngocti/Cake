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
                    if (CommandInvoker.ContainsItem(m_themeItem))
                    {
                        return;
                    }

                    SetSideModel();
                    CameraManager.Instance.ZoomOut();
                    GetComponent<Toggle>().isOn = false;
                }
            }
        }
    }

    public void SetSideModel()
    {
        // Check if we are in a tiered cake or not
        if (GameManager.Instance.m_cakeType == CAKETYPES.Three_Tier)
        {
            ICommand command = new ThemeItemPlaceCommand(m_themeItem, GameObject.Find("Cake_Sml").transform, m_material, m_matIndex);
            CommandInvoker.AddCommand(command);
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Two_Tier)
        {
            ICommand command = new ThemeItemPlaceCommand(m_themeItem, GameObject.Find("Cake_Med").transform, m_material, m_matIndex);
            CommandInvoker.AddCommand(command);
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Large)
        {
            ICommand command = new ThemeItemPlaceCommand(m_themeItem, GameObject.Find("Cake_Lrg(Clone)").transform, m_material, m_matIndex);
            CommandInvoker.AddCommand(command);
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Medium)
        {
            ICommand command = new ThemeItemPlaceCommand(m_themeItem, GameObject.Find("Cake_Med(Clone)").transform, m_material, m_matIndex);
            CommandInvoker.AddCommand(command);
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Small)
        {
            ICommand command = new ThemeItemPlaceCommand(m_themeItem, GameObject.Find("Cake_Sml(Clone)").transform, m_material, m_matIndex);
            CommandInvoker.AddCommand(command);
        }
    }
}
