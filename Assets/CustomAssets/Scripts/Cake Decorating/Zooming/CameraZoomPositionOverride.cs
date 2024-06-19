    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomPositionOverride : MonoBehaviour
{
    public Transform m_newCameraPosition;
    private Vector3 m_originalCameraPosition;
    public bool m_isActive;

    private Transform m_cameraStart;
    private bool m_previousBool;
    private float t = 0;

    private void Start()
    {
        m_originalCameraPosition = m_newCameraPosition.position;
    }

    private void Update()
    {
        if (!m_isActive)
            return;

        Camera.main.transform.position = Vector3.Lerp(m_cameraStart.position, m_newCameraPosition.position, t);
        Camera.main.transform.rotation = Quaternion.Lerp(m_cameraStart.rotation, m_newCameraPosition.rotation, t);
        t += 5.0f * Time.deltaTime;
    }

    public void OverrideZoom(ZoomInButton zoomInButton)
    {
        zoomInButton.m_override = m_isActive;
        m_cameraStart = Camera.main.transform;
        t = 0;
    }

    public void OverrideZoom(ZoomOutButton zoomOutButton)
    {
        zoomOutButton.m_override = m_isActive;
        m_cameraStart = Camera.main.transform;
        t = 0;
    }

    public void SetBool(bool set)
    {
        m_previousBool = m_isActive;
        m_isActive = set;
    }

    public void ResetBool()
    {
        if (m_isActive == m_previousBool)
        {
            m_isActive = false;
            return;
        }
    }

    public void MoveCameraUp()
    {
        m_newCameraPosition.position = m_newCameraPosition.position + Vector3.up * 80.0f;
    }

    public void MoveCameraDown()
    {
        m_newCameraPosition.position = m_newCameraPosition.position + Vector3.down * 80.0f;
    }

    public void ResetPosition()
    {
        m_newCameraPosition.position = m_originalCameraPosition;
    }
}
