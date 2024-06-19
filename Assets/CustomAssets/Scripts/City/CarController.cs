using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform[] m_wayPoints;

    private float m_originalMovementSpeed;
    public float m_movementSpeed = 30.0f;
    public float m_turningSpeed = 5.0f;
    public LayerMask m_layerMask;
    private int m_currentIndex = 0;

    private float m_accelerationTimer = 2.0f;
    private void Start()
    {
        m_originalMovementSpeed = m_movementSpeed;
    }

    private void Update()
    {
        Collider[] frontColliders = Physics.OverlapBox(transform.position + (transform.forward * 5.0f) + (transform.up * 1.5f), Vector3.one / 2.0f, Quaternion.identity, m_layerMask);

        if (frontColliders.Length > 0)
        {
            m_accelerationTimer = 2.0f;
            m_movementSpeed = 0;
        }
        else
        {
            if (m_accelerationTimer > 0)
            {
                m_accelerationTimer -= Time.deltaTime;
            }
            else
            {
                m_movementSpeed = Mathf.Lerp(m_movementSpeed, m_originalMovementSpeed, Time.deltaTime);
            }
        }

        if (m_currentIndex < m_wayPoints.Length)
        {
            Vector3 target = m_wayPoints[m_currentIndex].position;
            Vector3 lookPos = target - transform.position;
            lookPos.y = 0;

            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * m_turningSpeed);
            transform.position += transform.forward * Time.deltaTime * m_movementSpeed;

            if (Vector3.Distance(transform.position, target) < 6.0f)
            {
                m_currentIndex++;
            }
        }
        else
        {
            Vector3 target = m_wayPoints[0].position;
            Vector3 lookPos = target - transform.position;
            lookPos.y = 0;

            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * m_turningSpeed);
            transform.position += transform.forward * Time.deltaTime * m_movementSpeed;

            if (Vector3.Distance(transform.position, target) < 6.5f)
            {
                m_currentIndex = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < m_wayPoints.Length; i++)
        {
            Gizmos.DrawCube(m_wayPoints[i].position, Vector3.one);
            if (i + 1 < m_wayPoints.Length)
            {
                Gizmos.DrawLine(m_wayPoints[i].position, m_wayPoints[i + 1].position);
            }
            else
            {
                Gizmos.DrawLine(m_wayPoints[i].position, m_wayPoints[0].position);
            }
        }
    }
}
