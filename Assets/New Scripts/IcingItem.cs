using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IcingItem : SelectableItem
{
    //the game objects array is [0] the prefab and [1] the instance
    static Dictionary<string, GameObject[]> _icingUsed;
    static bool _isIcing = false;

    public Material m_material;

    public GameObject m_maskPrefab;
    public GameObject m_largeDrops;
    public GameObject m_medDrops;
    public GameObject m_smallDrops;

    static public void ClearIcingUsed()
    {
        _isIcing = false;

        if (_icingUsed == null)
        {
            StartIcingUsed();
            return;
        }

        _icingUsed.Clear();
    }

    static void StartIcingUsed()
    {
        _icingUsed = new Dictionary<string, GameObject[]>();
    }

    static public void StopIcing()
    {
        _isIcing = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _toggle?.onValueChanged.AddListener(delegate { SetSelected(); });
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_selected)
        {
            return;
        }

        if (_isIcing)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = CameraManager.Instance.RaycastCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (CameraManager.Instance.UsingZoomOut())
            {
                if (GameManager.Instance.m_cakeType == CAKETYPES.Three_Tier)
                {
                    if (hit.collider.tag == "CakeS" || hit.collider.tag == "CakeM" || hit.collider.tag == "CakeL")
                    {
                        MouseControls(hit, hit.collider.name);
                    }
                }
                else if (GameManager.Instance.m_cakeType == CAKETYPES.Two_Tier)
                {
                    if (hit.collider.tag == "CakeM" || hit.collider.tag == "CakeL")
                    {
                        MouseControls(hit, hit.collider.name);
                    }
                }
                else if (GameManager.Instance.m_cakeType == CAKETYPES.Large && hit.collider.tag == "CakeL")
                {
                    MouseControls(hit, hit.collider.name);
                }
                else if (GameManager.Instance.m_cakeType == CAKETYPES.Medium && hit.collider.tag == "CakeM")
                {
                    MouseControls(hit, hit.collider.name);
                }
                else if (GameManager.Instance.m_cakeType == CAKETYPES.Small && hit.collider.tag == "CakeS")
                {
                    MouseControls(hit, hit.collider.name);
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
                        MouseControls(hit, hit.collider.name);
                        break;
                    case 1:
                        if (hit.collider.tag != "CakeM")
                        {
                            return;
                        }
                        MouseControls(hit, hit.collider.name);
                        break;
                    case 2:
                        if (hit.collider.tag != "CakeS")
                        {
                            return;
                        }
                        MouseControls(hit, hit.collider.name);
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
                        MouseControls(hit, hit.collider.name);
                        break;
                    case 1:
                        if (hit.collider.tag != "CakeM")
                        {
                            return;
                        }
                        MouseControls(hit, hit.collider.name);
                        break;
                }
            }
            else if (GameManager.Instance.m_cakeType == CAKETYPES.Large)
            {
                if (hit.collider.tag != "CakeL")
                {
                    return;
                }
                MouseControls(hit, hit.collider.name);
            }
            else if (GameManager.Instance.m_cakeType == CAKETYPES.Medium)
            {
                if (hit.collider.tag != "CakeM")
                {
                    return;
                }
                MouseControls(hit, hit.collider.name);
            }
            else if (GameManager.Instance.m_cakeType == CAKETYPES.Small)
            {
                if (hit.collider.tag != "CakeS")
                {
                    return;
                }
                MouseControls(hit, hit.collider.name);
            }
        }
    }

    public void MouseControls(RaycastHit hit, string cakeName)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.normal.y < 0.9f)
            {
                return;
            }

            if (m_selected)
            {
                StartIcing(hit, cakeName);

                if (_deselectAfterUse)
                {
                    GetComponent<Toggle>().isOn = false;
                }
            }
        }
    }

    void StartIcing(RaycastHit hit, string cakeName)
    {
        if (_icingUsed == null)
        {
            StartIcingUsed();
        }

        if (_isIcing)
        {
            return;
        }

        GameObject[] icingObjcts;

        GameObject currentDrops = m_smallDrops;

        switch (cakeName)
        {
            case "Cake_Sml":
                currentDrops = m_smallDrops;
                break;
            case "Cake_Med":
                currentDrops = m_medDrops;
                break;
            case "Cake_Lrg":
                currentDrops = m_largeDrops;
                break;
            default:
                break;
        }

        if (_icingUsed.TryGetValue(cakeName, out icingObjcts))
        {
            //the game objects array is [0] the prefab and [1] the instance
            if (icingObjcts[0] == currentDrops && icingObjcts[1].GetComponent<MeshRenderer>().material.color == m_material.color)
            {
                return;
            }

            Destroy(icingObjcts[1]);
            _icingUsed.Remove(cakeName);
        }

        _isIcing = true;

        GameObject drops = Instantiate(currentDrops, GameObject.Find(cakeName).transform);
        drops.GetComponent<MeshRenderer>().material = m_material;
        drops.transform.localPosition = new Vector3(0, 0, 0);
        //drops.transform.localScale = new Vector3(1 / 2.54f, 1 / 2.54f, 1 / 2.54f);
        drops.transform.localScale = CakeManager.Instance.GetIcingScale(cakeName);
        drops.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        //the game objects array is [0] the prefab and [1] the instance
        icingObjcts = new GameObject[2];
        icingObjcts[0] = currentDrops;
        icingObjcts[1] = drops;

        _icingUsed.Add(cakeName, icingObjcts);

        GameObject m_maskObj = Instantiate(m_maskPrefab, GameObject.Find(cakeName).transform);
        m_maskObj.GetComponent<MaskObject>().m_maskObj[0] = drops;
        m_maskObj.transform.position = hit.point;
        m_maskObj.transform.localPosition += Vector3.forward * CakeManager.Instance.GetIcingMaskDistance();
        m_maskObj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
