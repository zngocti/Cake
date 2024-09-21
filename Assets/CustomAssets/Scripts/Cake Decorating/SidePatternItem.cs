using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SidePatternItem : SelectableItem
{
    public Material m_heldPattern;

    private void Start()
    {
        _toggle?.onValueChanged.AddListener(delegate { SetSelected(); });
    }

    private void Update()
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

        if ((int)GameManager.Instance.m_cakeType < 3)
        {
            RaycastHit hit;
            Ray ray = CameraManager.Instance.RaycastCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (!hit.collider.tag.Contains("Cake"))
                    return;

                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                if (m_selected)
                {
                    ICommand command = new SidePatternPlaceCommand(GameManager.Instance.m_selectedCake.transform, m_heldPattern);
                    CommandInvoker.AddCommand(command);

                    GetComponent<Toggle>().isOn = false;
                }
            }
        }
        else
        {
            RaycastHit hit;
            Ray ray = CameraManager.Instance.RaycastCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (!hit.collider.tag.Contains("Cake"))
                    return;

                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                if (m_selected)
                {
                    if (CameraManager.Instance.UsingZoomOut())
                    {
                        if (hit.collider.tag.Equals("CakeL"))
                        {
                            ICommand command = new SidePatternPlaceCommand(GameManager.Instance.m_selectedCake.transform.GetChild(0), m_heldPattern);
                            CommandInvoker.AddCommand(command);
                        }
                        else if (hit.collider.tag.Equals("CakeM"))
                        {
                            ICommand command = new SidePatternPlaceCommand(GameManager.Instance.m_selectedCake.transform.GetChild(1), m_heldPattern);
                            CommandInvoker.AddCommand(command);
                        }
                        else if (hit.collider.tag.Equals("CakeS"))
                        {
                            ICommand command = new SidePatternPlaceCommand(GameManager.Instance.m_selectedCake.transform.GetChild(2), m_heldPattern);
                            CommandInvoker.AddCommand(command);
                        }
                    }
                    else
                    {
                        ICommand command = new SidePatternPlaceCommand(GameManager.Instance.m_selectedCake.transform.GetChild(CameraMoveCakeLayer.Instance.m_currentLayer), m_heldPattern);
                        CommandInvoker.AddCommand(command);
                    }

                    if (_deselectAfterUse)
                    {
                        GetComponent<Toggle>().isOn = false;
                    }
                }
            }
        }
    }
}
