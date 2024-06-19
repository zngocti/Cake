using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SideCircularItem : SelectableItem
{
    public GameObject m_heldObjectLarge;
    public GameObject m_heldObjectMedium;
    public GameObject m_heldObjectSmall;

    public Material m_syrupMatchMaterial;

    private void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener(delegate { SetSelected(); });
    }

    public void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

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
            ICommand command;

            switch (CameraMoveCakeLayer.Instance.m_currentLayer)
            {
                case 0:
                    command = new SideCircularItemPlaceCommand(m_heldObjectLarge, GameObject.Find("Cake_Lrg").transform, m_syrupMatchMaterial, 0);
                    CommandInvoker.AddCommand(command);
                    break;
                case 1:
                    command = new SideCircularItemPlaceCommand(m_heldObjectMedium, GameObject.Find("Cake_Med").transform, m_syrupMatchMaterial, 0);
                    CommandInvoker.AddCommand(command);
                    break;
                case 2:
                    command = new SideCircularItemPlaceCommand(m_heldObjectSmall, GameObject.Find("Cake_Sml").transform, m_syrupMatchMaterial, 0);
                    CommandInvoker.AddCommand(command);
                    break;
            }
        } else if (GameManager.Instance.m_cakeType == CAKETYPES.Two_Tier)
        {
            ICommand command;

            switch (CameraMoveCakeLayer.Instance.m_currentLayer)
            {
                case 0:
                    command = new SideCircularItemPlaceCommand(m_heldObjectLarge, GameObject.Find("Cake_Lrg").transform, m_syrupMatchMaterial, 0);
                    CommandInvoker.AddCommand(command);
                    break;
                case 1:
                    command = new SideCircularItemPlaceCommand(m_heldObjectMedium, GameObject.Find("Cake_Med").transform, m_syrupMatchMaterial, 0);
                    CommandInvoker.AddCommand(command);
                    break;
            }
        } 
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Large)
        {
            ICommand command = new SideCircularItemPlaceCommand(m_heldObjectLarge, GameObject.Find("Cake_Lrg(Clone)").transform, m_syrupMatchMaterial, 0);
            CommandInvoker.AddCommand(command);
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Medium)
        {
            ICommand command = new SideCircularItemPlaceCommand(m_heldObjectMedium, GameObject.Find("Cake_Med(Clone)").transform, m_syrupMatchMaterial, 0);
            CommandInvoker.AddCommand(command);
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Small)
        {
            ICommand command = new SideCircularItemPlaceCommand(m_heldObjectSmall, GameObject.Find("Cake_Sml(Clone)").transform, m_syrupMatchMaterial, 0);
            CommandInvoker.AddCommand(command);
        }
    }
}
