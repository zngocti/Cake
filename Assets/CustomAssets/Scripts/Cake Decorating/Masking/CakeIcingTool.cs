using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CakeIcingTool : MonoBehaviour
{
    private Vector3 m_offset;
    private float m_zCoord;

    private Transform m_nozzlePoint;

    public Material m_material;

    public GameObject m_maskPrefab;
    private GameObject m_maskObj;
    public GameObject m_largeDrops;
    public GameObject m_medDrops;
    public GameObject m_smallDrops;

    private void Start()
    {
        m_nozzlePoint = transform.GetChild(0); 
    }

    static GameObject PrefabUsedSDrops;
    static GameObject CakeSDrops;

    private void OnMouseDown()
    {
        m_zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        m_offset = gameObject.transform.position - GetMouseWorldPos();

        // Raycast down to cake surface
        RaycastHit hit;
        Ray ray = new Ray(m_nozzlePoint.position, Vector3.down);

        if (Physics.Raycast(ray, out hit))
        {
            if (GameManager.Instance.m_cakeType == CAKETYPES.Three_Tier)
            {
                switch (CameraMoveCakeLayer.Instance.m_currentLayer)
                {
                    case 0:
                        if (hit.collider.tag != "CakeL")
                        {
                        }
                        break;
                    case 1:
                        if (hit.collider.tag != "CakeM")
                        {
                        }
                        break;
                    case 2:
                        if (hit.collider.tag == "CakeS")
                        {
                            if (CakeSDrops && PrefabUsedSDrops)
                            {
                                if (PrefabUsedSDrops == m_smallDrops && CakeSDrops.GetComponent<MeshRenderer>().material.color == m_material.color)
                                {
                                    return;
                                }

                                Destroy(CakeSDrops);
                            }

                            GameObject drops = Instantiate(m_smallDrops, GameObject.Find("Cake_Sml").transform);
                            drops.GetComponent<MeshRenderer>().material = m_material;
                            drops.transform.localPosition   = new Vector3(0, 0, 0);
                            drops.transform.localScale      = new Vector3(1/2.54f, 1 / 2.54f, 1 / 2.54f);
                            drops.transform.localRotation   = Quaternion.Euler(new Vector3(0, 0, 0));

                            CakeSDrops = drops;
                            PrefabUsedSDrops = m_smallDrops;

                            GameObject m_maskObj = Instantiate(m_maskPrefab, GameObject.Find("Cake_Sml").transform);
                            m_maskObj.GetComponent<MaskObject>().m_maskObj[0] = drops;
                            m_maskObj.transform.position = hit.point;
                            m_maskObj.transform.localPosition += Vector3.forward * 2;
                            m_maskObj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        }
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
                        }
                        break;
                    case 1:
                        if (hit.collider.tag != "CakeM")
                        {
                        }
                        break;
                }
            }
            else if (GameManager.Instance.m_cakeType == CAKETYPES.Large)
            {
                if (hit.collider.tag != "CakeL")
                {
                }
            }
            else if (GameManager.Instance.m_cakeType == CAKETYPES.Medium)
            {
                if (hit.collider.tag != "CakeM")
                {
                }
            }
            else if (GameManager.Instance.m_cakeType == CAKETYPES.Small)
            {
                if (hit.collider.tag != "CakeS") 
                {                 
                }
            }
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = m_zCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + m_offset;
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag != "IcingTool")
                return;

            if (Input.GetMouseButton(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }


            }
        }
    }

    public void SetVariables(GameObject largeDrops, GameObject medDrops, GameObject smallDrops)
    {
        m_largeDrops = largeDrops;
        m_medDrops = medDrops;
        m_smallDrops = smallDrops;
    }
}
