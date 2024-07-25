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
    TextVector3[] _icingScale = new TextVector3[0];

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
            if (_icingScale[i]._name.Equals(cakeType))
            {
                return _icingScale[i]._vector;
            }
        }

        return Vector3.one;
    }
}
