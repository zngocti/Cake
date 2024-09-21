using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SideItem : SelectableItem
{
    public float m_forwardIncrease;
    public int m_matIndex;
    public Material m_itemMat;
    public GameObject m_itemPrefab;
    public Vector3 _scale = Vector3.one;
    public ParticleSystem _particles;

    float _maxNormalY = 0.03f;

    private void Start()
    {
        _toggle?.onValueChanged.AddListener(delegate { SetSelected(); });

        m_itemPrefab.transform.localScale = _scale; 
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

        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (m_indicator)
            {
                Destroy(m_indicator);
            }
            return;
        }

        /*
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (m_indicator)
            {
                Destroy(m_indicator);
            }
            return;
        }*/

        RaycastHit hit;
        Ray ray = CameraManager.Instance.RaycastCam.ScreenPointToRay(Input.mousePosition);

        /*
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = Camera.main.farClipPlane; // Change according to your needs
        Debug.Log(Camera.main.farClipPlane);
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        Vector3 origin = CameraManager.Instance.CurrentCameraPos;
        Vector3 direction = (worldPoint - origin).normalized;
        Ray ray = new Ray(origin, direction);
        */
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin, ray.direction * 3000, Color.green);

            if (CameraManager.Instance.UsingZoomOut())
            {
                MouseControls(hit);
            }
            else if (GameManager.Instance.m_cakeType == CAKETYPES.Three_Tier)
            {
                switch (CameraMoveCakeLayer.Instance.m_currentLayer)
                {
                    case 0:
                        if (hit.collider.tag != "CakeL")
                        {
                            Destroy(m_indicator);
                            return;
                        }
                        MouseControls(hit);
                        break;
                    case 1:
                        if (hit.collider.tag != "CakeM")
                        {
                            Destroy(m_indicator);
                            return;
                        }
                        MouseControls(hit);
                        break;
                    case 2:
                        if (hit.collider.tag != "CakeS")
                        {
                            Destroy(m_indicator);
                            return;
                        }
                        MouseControls(hit);
                        break;
                }
            }
            else if (GameManager.Instance.m_cakeType == CAKETYPES.Two_Tier)
            {
                switch (CameraMoveCakeLayer.Instance.m_currentLayer)
                {
                    case 0:
                        if (hit.collider.tag != "CakeL")
                        {
                            Destroy(m_indicator);
                            return;
                        }
                        MouseControls(hit);
                        break;
                    case 1:
                        if (hit.collider.tag != "CakeM")
                        {
                            Destroy(m_indicator);
                            return;
                        }
                        MouseControls(hit);
                        break;
                }
            }
            else if (GameManager.Instance.m_cakeType == CAKETYPES.Large)
            {
                if (hit.collider.tag != "CakeL")
                {
                    Destroy(m_indicator);
                    return;
                }
                MouseControls(hit);
            }
            else if (GameManager.Instance.m_cakeType == CAKETYPES.Medium)
            {
                if (hit.collider.tag != "CakeM")
                {
                    Destroy(m_indicator);
                    return;
                }
                MouseControls(hit);
            }
            else if (GameManager.Instance.m_cakeType == CAKETYPES.Small)
            {
                if (hit.collider.tag != "CakeS")
                {
                    Destroy(m_indicator);
                    return;
                }
                MouseControls(hit);
            }            
        }
        else
        {
            if (m_indicator)
            {
                Destroy(m_indicator);
            }
        }
    }

    void SetIndicator(GameObject indicator, Material material, int matIndex)
    {
        Material[] mats;
        Material match;

        if (indicator.GetComponent<MeshRenderer>())
            mats = indicator.GetComponent<MeshRenderer>().sharedMaterials;
        else
            mats = indicator.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterials;

        match = new Material(material);

        mats[matIndex] = match;

        indicator.GetComponent<MeshRenderer>().sharedMaterials = mats;
    }

    public void MouseControls(RaycastHit hit)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (m_selected)
            {
                m_indicator = Instantiate(m_itemPrefab);
                //m_indicator.GetComponent<MeshRenderer>().material = GameManager.Instance.m_highlightMaterial;
                m_indicator.GetComponent<MeshRenderer>().materials[m_matIndex] = m_itemMat;
                SetIndicator(m_indicator, m_itemMat, m_matIndex);
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (hit.normal.y > _maxNormalY)
            {
                Destroy(m_indicator);
            }
            if (m_indicator)
            {
                m_indicator.transform.position = hit.point - m_indicator.transform.up * m_forwardIncrease;
                m_indicator.transform.rotation = Quaternion.LookRotation(hit.normal) * Quaternion.Euler(new Vector3(-90, 0, 0));
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (m_indicator)
            {
                SetSideModel(m_indicator.transform.position, m_indicator.transform.rotation);

                Destroy(m_indicator);
                if (_deselectAfterUse)
                {
                    GetComponent<Toggle>().isOn = false;
                }
            }
        }
    }

    public void SetSideModel(Vector3 position, Quaternion rotation)
    {
        // Check if we are in a tiered cake or not
        if (GameManager.Instance.m_cakeType == CAKETYPES.Three_Tier)
        {
            ICommand command;

            switch (CameraMoveCakeLayer.Instance.m_currentLayer)
            {
                case 0:
                    command = new SideItemPlaceCommand(m_itemPrefab, position, rotation, m_itemMat, m_matIndex, _particles);
                    CommandInvoker.AddCommand(command);
                    break;
                case 1:
                    command = new SideItemPlaceCommand(m_itemPrefab, position, rotation, m_itemMat, m_matIndex, _particles);
                    CommandInvoker.AddCommand(command);
                    break;
                case 2:
                    command = new SideItemPlaceCommand(m_itemPrefab, position, rotation, m_itemMat, m_matIndex, _particles);
                    CommandInvoker.AddCommand(command);
                    break;
            }
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Two_Tier)
        {
            ICommand command;

            switch (CameraMoveCakeLayer.Instance.m_currentLayer)
            {
                case 0:
                    command = new SideItemPlaceCommand(m_itemPrefab, position, rotation, m_itemMat, m_matIndex, _particles);
                    CommandInvoker.AddCommand(command);
                    break;
                case 1:
                    command = new SideItemPlaceCommand(m_itemPrefab, position, rotation, m_itemMat, m_matIndex, _particles);
                    CommandInvoker.AddCommand(command);
                    break;
            }
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Large)
        {
            ICommand command = new SideItemPlaceCommand(m_itemPrefab, position, rotation, m_itemMat, m_matIndex, _particles);
            CommandInvoker.AddCommand(command);
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Medium)
        {
            ICommand command = new SideItemPlaceCommand(m_itemPrefab, position, rotation, m_itemMat, m_matIndex, _particles);
            CommandInvoker.AddCommand(command);
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Small)
        {
            ICommand command = new SideItemPlaceCommand(m_itemPrefab, position, rotation, m_itemMat, m_matIndex, _particles);
            CommandInvoker.AddCommand(command);
        }
    }
}
