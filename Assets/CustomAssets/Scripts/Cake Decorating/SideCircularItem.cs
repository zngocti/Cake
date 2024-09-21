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
        _toggle?.onValueChanged.AddListener(delegate { SetSelected(); });
    }

    public void Update()
    {
        if (!m_selected)
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
                SetSideModel(hit);
                if (_deselectAfterUse)
                {
                    GetComponent<Toggle>().isOn = false;
                }
            }
        }
    }

    public void SetSideModel(RaycastHit hit)
    {
        // Check if we are in a tiered cake or not
        if (GameManager.Instance.m_cakeType == CAKETYPES.Three_Tier)
        {
            ICommand command;

            int myLayer = 0;

            if (CameraManager.Instance.UsingZoomOut())
            {
                if (hit.collider.tag.Equals("CakeL"))
                {
                    myLayer = 0;
                }
                else if (hit.collider.tag.Equals("CakeM"))
                {
                    myLayer = 1;
                }
                else if (hit.collider.tag.Equals("CakeS"))
                {
                    myLayer = 2;
                }
            }
            else
            {
                myLayer = CameraMoveCakeLayer.Instance.m_currentLayer;
            }

            switch (myLayer)
            {
                case 0:
                    if (CircularItemsCounter.Instance.IsOnCakeLayer("Cake_Lrg", _selectableItemID))
                    {
                        CircularItemsCounter.Instance.RePaint("Cake_Lrg", _selectableItemID, m_syrupMatchMaterial.color);
                        return;
                    }

                    command = new SideCircularItemPlaceCommand(m_heldObjectLarge, GameObject.Find("Cake_Lrg").transform, m_syrupMatchMaterial, 0, "Cake_Lrg", _selectableItemID);
                    CommandInvoker.AddCommand(command);
                    break;
                case 1:
                    if (CircularItemsCounter.Instance.IsOnCakeLayer("Cake_Med", _selectableItemID))
                    {
                        CircularItemsCounter.Instance.RePaint("Cake_Med", _selectableItemID, m_syrupMatchMaterial.color);
                        return;
                    }

                    command = new SideCircularItemPlaceCommand(m_heldObjectMedium, GameObject.Find("Cake_Med").transform, m_syrupMatchMaterial, 0, "Cake_Med", _selectableItemID);
                    CommandInvoker.AddCommand(command);
                    break;
                case 2:
                    if (CircularItemsCounter.Instance.IsOnCakeLayer("Cake_Sml", _selectableItemID))
                    {
                        CircularItemsCounter.Instance.RePaint("Cake_Sml", _selectableItemID, m_syrupMatchMaterial.color);
                        return;
                    }

                    command = new SideCircularItemPlaceCommand(m_heldObjectSmall, GameObject.Find("Cake_Sml").transform, m_syrupMatchMaterial, 0, "Cake_Sml", _selectableItemID);
                    CommandInvoker.AddCommand(command);
                    break;
            }
        } else if (GameManager.Instance.m_cakeType == CAKETYPES.Two_Tier)
        {
            ICommand command;

            switch (CameraMoveCakeLayer.Instance.m_currentLayer)
            {
                case 0:
                    if (CircularItemsCounter.Instance.IsOnCakeLayer("Cake_Lrg", _selectableItemID))
                    {
                        CircularItemsCounter.Instance.RePaint("Cake_Lrg", _selectableItemID, m_syrupMatchMaterial.color);
                        return;
                    }

                    command = new SideCircularItemPlaceCommand(m_heldObjectLarge, GameObject.Find("Cake_Lrg").transform, m_syrupMatchMaterial, 0, "Cake_Lrg", _selectableItemID);
                    CommandInvoker.AddCommand(command);
                    break;
                case 1:
                    if (CircularItemsCounter.Instance.IsOnCakeLayer("Cake_Med", _selectableItemID))
                    {
                        CircularItemsCounter.Instance.RePaint("Cake_Med", _selectableItemID, m_syrupMatchMaterial.color);
                        return;
                    }

                    command = new SideCircularItemPlaceCommand(m_heldObjectMedium, GameObject.Find("Cake_Med").transform, m_syrupMatchMaterial, 0, "Cake_Med", _selectableItemID);
                    CommandInvoker.AddCommand(command);
                    break;
            }
        } 
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Large)
        {
            if (CircularItemsCounter.Instance.IsOnCakeLayer("Cake_Lrg", _selectableItemID))
            {
                CircularItemsCounter.Instance.RePaint("Cake_Lrg", _selectableItemID, m_syrupMatchMaterial.color);
                return;
            }

            ICommand command = new SideCircularItemPlaceCommand(m_heldObjectLarge, GameObject.Find("Cake_Lrg").transform, m_syrupMatchMaterial, 0, "Cake_Lrg", _selectableItemID);
            CommandInvoker.AddCommand(command);
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Medium)
        {
            if (CircularItemsCounter.Instance.IsOnCakeLayer("Cake_Med", _selectableItemID))
            {
                CircularItemsCounter.Instance.RePaint("Cake_Med", _selectableItemID, m_syrupMatchMaterial.color);
                return;
            }

            ICommand command = new SideCircularItemPlaceCommand(m_heldObjectMedium, GameObject.Find("Cake_Med").transform, m_syrupMatchMaterial, 0, "Cake_Med", _selectableItemID);
            CommandInvoker.AddCommand(command);
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Small)
        {
            if (CircularItemsCounter.Instance.IsOnCakeLayer("Cake_Sml", _selectableItemID))
            {
                CircularItemsCounter.Instance.RePaint("Cake_Sml", _selectableItemID, m_syrupMatchMaterial.color);
                return;
            }

            ICommand command = new SideCircularItemPlaceCommand(m_heldObjectSmall, GameObject.Find("Cake_Sml").transform, m_syrupMatchMaterial, 0, "Cake_Sml", _selectableItemID);
            CommandInvoker.AddCommand(command);
        }
    }
}
