using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

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
        }
    }

    public CAKETYPES m_cakeType;
    public GameObject[] m_cakesPrefabs;
    public GameObject m_selectedCake;

    public Transform m_spawn;

    public List<MeshRenderer> m_meshesToMatch;
    public List<Image> m_imagesToMatch;


    public Material m_selectedSidePattern;
    public Material m_highlightMaterial;

    // Start is called before the first frame update
    void Start()
    {
        m_selectedCake = Instantiate(m_cakesPrefabs[(int)m_cakeType], m_spawn.position, m_cakesPrefabs[(int)m_cakeType].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_meshesToMatch.Count; i++)
        {
            m_meshesToMatch[i].sharedMaterials[1].color = SyrupSelector.Instance.m_selected;
        }

        for (int i = 0; i < m_imagesToMatch.Count; i++)
        {
            m_imagesToMatch[i].color = SyrupSelector.Instance.m_selected;
        }
    }
}
