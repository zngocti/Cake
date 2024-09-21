using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TextVector3
{
    public string _name;
    public Vector3 _vector;
}

public class CakeManager : MonoBehaviour
{
    static CakeManager _instance;
    public static CakeManager Instance { get => _instance; }

    [SerializeField]
    string _decorationTag = "Decoration";

    [SerializeField]
    float _icingMaskDistance = 5;

    [SerializeField]
    Color _outlineColor = Color.red;

    [SerializeField]
    float _outlineWidth = 2;

    [SerializeField]
    QuickOutline.Mode _outlineMode = QuickOutline.Mode.OutlineVisible;

    [SerializeField]
    TextVector3[] _icingScale = new TextVector3[0];

    public string DecorationTag { get => _decorationTag; }

    public Color OutlineColor { get => _outlineColor; }
    public float OutlineWidth { get => _outlineWidth; }
    public QuickOutline.Mode OutlineMode { get => _outlineMode; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            return;
        }

        if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public Vector3 GetIcingScale(string cakeType)
    {
        if (_icingScale.Length < 1)
        {
            return Vector3.one;
        }

        Vector3 scale;

        for (int i = 0; i < _icingScale.Length; i++)
        {
            if (_icingScale[i]._name.Contains(cakeType))
            {
                return _icingScale[i]._vector;
            }
        }

        return Vector3.one;
    }

    public void NextCake()
    {
        GameManager.Instance.NextCake();
    }

    public float GetIcingMaskDistance()
    {
        return _icingMaskDistance;
    }
}
