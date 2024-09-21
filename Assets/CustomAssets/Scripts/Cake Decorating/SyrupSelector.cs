using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyrupSelector : MonoBehaviour
{
    // Singleton
    private static SyrupSelector _instance;

    public static SyrupSelector Instance { get { return _instance; } }

    [SerializeField]
    Material _startingColor;

    private void Awake()
    {
        // Remove duplicates
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            m_selected = _startingColor.color;
        }
    }

    public Color m_selected;

    public void SetColor(Color color)
    {
        m_selected = color;
        GameManager.Instance.UpdateColor();
    }

    /*
    public bool m_busy;

    public float m_timeToReachTargetPosition = 0.5f;
    public float t;

    public List<GameObject> m_syrups;
    public List<Vector3> m_orderPositions;

    public Vector3 m_mouseStart;
    public Vector3 m_mouseEnd;

    public List<Vector3> m_startPosition;
    */

    /*
    private void Start()
    {
        m_syrups = new List<GameObject>();
        m_startPosition = new List<Vector3>();
        m_orderPositions = new List<Vector3>();

        for (int i = 0; i < transform.childCount; i++)
        {
            m_syrups.Add(transform.GetChild(i).GetChild(0).gameObject);
            m_orderPositions.Add(transform.GetChild(i).transform.position);
            m_startPosition.Add(transform.GetChild(i).transform.position);
        }
    }

    private void Update()
    {
        t += Time.deltaTime / m_timeToReachTargetPosition;
        Mathf.Clamp01(t);

        // Uninteractable while lerping
        if (t >= 0 && t <= 1)
        {
        }
        else
        {
            m_busy = false;
        }

        for (int i = 0; i < m_syrups.Count; i++)
        {
            m_syrups[i].transform.parent.transform.position = Vector3.Lerp(m_startPosition[i], m_orderPositions[i], t);
        }

        m_selected = m_syrups[0].GetComponent<MeshRenderer>().sharedMaterial.color;
    }

    private void OnMouseDown()
    {
        if (m_busy)
            return;

        m_mouseStart = Input.mousePosition;
        m_busy = true;
    }

    private void OnMouseUp()
    {
        if (m_busy)
            return;

        m_mouseEnd = Input.mousePosition;

        //If dragging to the right move right
        if (m_mouseStart.x < m_mouseEnd.x)
        {
            Debug.Log("Right");

            // Remove the last item, the list shifts to the right and then place the front item
            GameObject last = m_syrups[m_syrups.Count - 1];
            last.transform.parent.transform.position += Vector3.forward * 20.0f;
            m_syrups.RemoveAt(m_syrups.Count - 1);
            t = 0;
            for (int i = 0; i < m_syrups.Count; i++)
            {
                m_startPosition[i + 1] = m_syrups[i].transform.position;
            }

            m_startPosition[0] = last.transform.position;
            m_syrups.Insert(0, last);
        }
        else if (m_mouseStart.x > m_mouseEnd.x)
        {
            Debug.Log("Left");

            // Remove the first item, the list shifts to the left and then place the final item
            GameObject front = m_syrups[0];
            front.transform.parent.transform.position += Vector3.forward * 20.0f;
            m_syrups.RemoveAt(0);
            t = 0;

            for (int i = 0; i < m_syrups.Count; i++)
            {
                m_startPosition[i] = m_syrups[i].transform.position;
            }
            m_startPosition[m_startPosition.Count - 1] = front.transform.position;
            m_syrups.Add(front);
        }
    }*/
}
