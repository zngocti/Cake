using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IcingItem : SelectableItem
{
    static GameObject PrefabUsedSDrops;
    static GameObject CakeSDrops;

    public Material m_material;

    public GameObject m_maskPrefab;
    public GameObject m_largeDrops;
    public GameObject m_medDrops;
    public GameObject m_smallDrops;

    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener(delegate { SetSelected(); });
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_selected)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = CameraManager.Instance.RaycastCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (CameraManager.Instance.UsingZoomOut())
            {
                if (GameManager.Instance.m_cakeType == CAKETYPES.Three_Tier && hit.collider.tag == "CakeS")
                {
                    MouseControls(hit);
                }
                else if (GameManager.Instance.m_cakeType == CAKETYPES.Two_Tier && hit.collider.tag == "CakeM")
                {
                    MouseControls(hit);
                }
                else if (GameManager.Instance.m_cakeType == CAKETYPES.Large && hit.collider.tag == "CakeL")
                {
                    MouseControls(hit);
                }
                else if (GameManager.Instance.m_cakeType == CAKETYPES.Medium && hit.collider.tag == "CakeM")
                {
                    MouseControls(hit);
                }
                else if (GameManager.Instance.m_cakeType == CAKETYPES.Small && hit.collider.tag == "CakeS")
                {
                    MouseControls(hit);
                }
                else
                {
                    return;
                }
            }
            else if (GameManager.Instance.m_cakeType == CAKETYPES.Three_Tier)
            {
                switch (CameraMoveCakeLayer.Instance.m_currentLayer)
                {
                    case 0:
                        if (hit.collider.tag != "CakeL")
                        {
                            return;
                        }
                        //MouseControls(hit);
                        return;
                        break;
                    case 1:
                        if (hit.collider.tag != "CakeM")
                        {
                            return;
                        }
                        //MouseControls(hit);
                        return;
                        break;
                    case 2:
                        if (hit.collider.tag != "CakeS")
                        {
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
                            return;
                        }
                        //MouseControls(hit);
                        return;
                        break;
                    case 1:
                        if (hit.collider.tag != "CakeM")
                        {
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
                    return;
                }
                MouseControls(hit);
            }
            else if (GameManager.Instance.m_cakeType == CAKETYPES.Medium)
            {
                if (hit.collider.tag != "CakeM")
                {
                    return;
                }
                MouseControls(hit);
            }
            else if (GameManager.Instance.m_cakeType == CAKETYPES.Small)
            {
                if (hit.collider.tag != "CakeS")
                {
                    return;
                }
                MouseControls(hit);
            }
        }
    }

    public void MouseControls(RaycastHit hit)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.normal.y < 0.9f)
            {
                return;
            }

            if (m_selected)
            {
                StartIcing(hit);
                GetComponent<Toggle>().isOn = false;
            }
        }
    }

    void StartIcing(RaycastHit hit)
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
        drops.transform.localPosition = new Vector3(0, 0, 0);
        drops.transform.localScale = new Vector3(1 / 2.54f, 1 / 2.54f, 1 / 2.54f);
        drops.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        CakeSDrops = drops;
        PrefabUsedSDrops = m_smallDrops;

        GameObject m_maskObj = Instantiate(m_maskPrefab, GameObject.Find("Cake_Sml").transform);
        m_maskObj.GetComponent<MaskObject>().m_maskObj[0] = drops;
        m_maskObj.transform.position = hit.point;
        m_maskObj.transform.localPosition += Vector3.forward * 2;
        m_maskObj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
