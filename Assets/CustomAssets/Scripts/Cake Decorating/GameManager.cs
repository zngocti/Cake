using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    [SerializeField]
    Transform _cakePosition;

    [SerializeField]
    Transform _cakeDestroyPosition;

    [SerializeField]
    float _cakeMovementTime = 1f;

    public List<MeshRenderer> m_meshesToMatch = new List<MeshRenderer>();
    public List<Image> m_imagesToMatch = new List<Image>();

    [SerializeField]
    List<TabGroup> _tabGroupsToMatch = new List<TabGroup>();

    public Material m_selectedSidePattern;
    public Material m_highlightMaterial;

    int _cakeTypeIndex = 0;

    bool _cakeMoving = true;

    public bool CakeMoving { get => _cakeMoving; }

    [SerializeField]
    NextCakeButton _button;

    // Start is called before the first frame update
    void Start()
    {
        _cakeTypeIndex = (int)m_cakeType;

        GenerateCake();
        UpdateColor();

        IcingItem.ClearIcingUsed();
        DeleteTool.ClearOutlines();
    }

    public bool NextCake()
    {
        if (m_selectedCake == null)
        {
            return false;
        }

        _cakeMoving = true;

        m_selectedCake.transform.DOMove(_cakeDestroyPosition.position, _cakeMovementTime).
            OnComplete(() => 
            { 
                Destroy(m_selectedCake);
                IcingItem.ClearIcingUsed();
                DeleteTool.ClearOutlines();
                CircularItemsCounter.Instance.ClearItemList();
                GenerateCake();
            });

        return true;
    }

    void GenerateCake()
    {
        _cakeMoving = true;

        m_selectedCake = Instantiate(m_cakesPrefabs[_cakeTypeIndex], m_spawn.position, m_cakesPrefabs[_cakeTypeIndex].transform.rotation);
        m_cakeType = (CAKETYPES)_cakeTypeIndex;

        switch (m_cakeType)
        {
            case CAKETYPES.Small:
                m_selectedCake.name = m_cakesPrefabs[_cakeTypeIndex].name;
                break;
            case CAKETYPES.Medium:
                m_selectedCake.name = m_cakesPrefabs[_cakeTypeIndex].name;
                break;
            case CAKETYPES.Large:
                m_selectedCake.name = m_cakesPrefabs[_cakeTypeIndex].name;
                break;
            case CAKETYPES.Two_Tier:
                break;
            case CAKETYPES.Three_Tier:
                break;
            default:
                break;
        }

        _cakeTypeIndex++;

        if (_cakeTypeIndex >= m_cakesPrefabs.Length)
        {
            _cakeTypeIndex = 0;
        }

        m_selectedCake.transform.DOMove(_cakePosition.position, _cakeMovementTime).
            OnComplete(() => 
            {
                _cakeMoving = false;
                _button.PullButton();
            });
    }

    public void UpdateColor()
    {
        for (int i = 0; i < m_meshesToMatch.Count; i++)
        {
            m_meshesToMatch[i].sharedMaterials[1].color = SyrupSelector.Instance.m_selected;
        }

        for (int i = 0; i < m_imagesToMatch.Count; i++)
        {
            m_imagesToMatch[i].color = SyrupSelector.Instance.m_selected;
        }

        for (int i = 0; i < _tabGroupsToMatch.Count; i++)
        {
            _tabGroupsToMatch[i].ColorSelectedTab(SyrupSelector.Instance.m_selected);
        }

        CurrentItem.Instance.ColorItemSelected(SyrupSelector.Instance.m_selected);
    }
}
