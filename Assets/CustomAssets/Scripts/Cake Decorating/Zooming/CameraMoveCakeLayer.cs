using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveCakeLayer : MonoBehaviour
{
    public static CameraMoveCakeLayer Instance;

    private void Awake()
    {
        Instance = this;
    }

    public int m_currentLayer;
    public int m_maxLayers;

    public bool m_goingUp;

    [SerializeField]
    GameObject _buttonUp;

    [SerializeField]
    GameObject _buttonDown;

    private void Start()
    {
        if (GameManager.Instance.m_cakeType == CAKETYPES.Two_Tier)
        {
            m_maxLayers = 1;
        }
        else if (GameManager.Instance.m_cakeType == CAKETYPES.Three_Tier)
        {
            m_maxLayers = 2;
        }
        else
        {
            m_maxLayers = 0;
        }
    }

    /*
    private void Update()
    {
        if (m_currentLayer == m_maxLayers)
        {
            _buttonDown.SetActive(true);
            _buttonUp.SetActive(false);
        }
        else if(m_currentLayer == 0)
        {
            _buttonDown.SetActive(false);
            _buttonUp.SetActive(true);
        }
        else
        {
            _buttonDown.SetActive(true);
            _buttonUp.SetActive(true);
        }
    }*/

    public void MoveUpLayer(CameraZoomPositionOverride cameraZoomPositionOverride)
    {
        if (!m_goingUp)
            return;

        if (!cameraZoomPositionOverride.m_isActive)
        {
            return;
        }

        if (m_currentLayer < m_maxLayers)
        {
            m_currentLayer++;
            cameraZoomPositionOverride.MoveCameraUp();
        }
    }

    public void MoveDownLayer(CameraZoomPositionOverride cameraZoomPositionOverride)
    {
        if (m_goingUp)
            return;

        if (!cameraZoomPositionOverride.m_isActive)
        {
            return;
        }

        if (m_currentLayer > 0)
        {
            m_currentLayer--;
            cameraZoomPositionOverride.MoveCameraDown();
        }
    }

    public void Slide(CameraZoomPositionOverride cameraZoomPositionOverride)
    {
        if (m_maxLayers == 0)
            //return;

        if (!cameraZoomPositionOverride.m_isActive)
        {
            GetComponent<Animator>().SetTrigger("Slide");
            GetComponent<Animator>().SetBool("SlideOut", true);
            return;
        }

        GetComponent<Animator>().SetTrigger("Slide");
        GetComponent<Animator>().SetBool("SlideOut", false);
    }

    public void ResetLayer(CameraZoomPositionOverride cameraZoomPositionOverride)
    {
        m_currentLayer = 0;
        cameraZoomPositionOverride.ResetPosition();
    }
}
