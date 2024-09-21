using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOutButton : MonoBehaviour
{
    public bool m_override;
    public bool m_enabled = false;
    public bool m_isOn;

    public Camera m_3DUICamera;
    public ZoomInButton m_zoomInButton;

    public Transform m_zoomInPos;
    public Transform m_zoomOutPos;

    private float t;

    private void Update()
    {
        if (!m_override)
        {
            //ButtonUpdate();
            CameraZoomUpdate();
        }
    }

    public void UseButton()
    {
        if (!m_isOn)
        {
            if (!enabled)
                return;

            transform.Translate(Vector3.forward * -10.0f);
            m_isOn = true;

            if (m_zoomInButton.m_isOn)
            {
                m_zoomInButton.gameObject.transform.Translate(Vector3.forward * 10.0f);
                m_zoomInButton.m_isOn = false;
            }

            m_zoomInButton.m_enabled = true;
            m_enabled = false;

            t = 0;
        }
    }

    private void ButtonUpdate()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        RaycastHit hit;
        Ray ray = m_3DUICamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag != "ZoomOut")
                return;

            if (!m_isOn)
            {
                if (!enabled)
                    return;

                hit.collider.gameObject.transform.Translate(Vector3.forward * -10.0f);
                m_isOn = true;

                if (m_zoomInButton.m_isOn)
                {
                    m_zoomInButton.gameObject.transform.Translate(Vector3.forward * 10.0f);
                    m_zoomInButton.m_isOn = false;
                }

                m_zoomInButton.m_enabled = true;
                m_enabled = false;

                t = 0;
            }
        }
    }

    private void CameraZoomUpdate()
    {
        if (!m_isOn)
            return;

        Camera.main.transform.position = Vector3.Lerp(m_zoomInPos.position, m_zoomOutPos.position, t);
        Camera.main.transform.rotation = Quaternion.Lerp(m_zoomInPos.rotation, m_zoomOutPos.rotation, t);
        t += 5.0f * Time.deltaTime;
    }

    public void SetNewZoomInPosition(Transform newPos)
    {
        m_zoomInPos = newPos;
    }
}
