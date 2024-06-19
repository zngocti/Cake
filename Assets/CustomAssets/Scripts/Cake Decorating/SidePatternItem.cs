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
        GetComponent<Toggle>().onValueChanged.AddListener(delegate { SetSelected(); });
    }

    private void Update()
    {
        if ((int)GameManager.Instance.m_cakeType < 3)
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
                        ICommand command = new SidePatternPlaceCommand(GameManager.Instance.m_selectedCake.transform, m_heldPattern);
                        CommandInvoker.AddCommand(command);

                        GetComponent<Toggle>().isOn = false;
                    }
                }
            }
        }
        else
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
                        ICommand command = new SidePatternPlaceCommand(GameManager.Instance.m_selectedCake.transform.GetChild(CameraMoveCakeLayer.Instance.m_currentLayer), m_heldPattern);
                        CommandInvoker.AddCommand(command);

                        GetComponent<Toggle>().isOn = false;
                    }
                }
            }
        }
    }
}
