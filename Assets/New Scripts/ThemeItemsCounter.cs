using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeItemsCounter : MonoBehaviour
{
    static ThemeItemsCounter _instance;
    public static ThemeItemsCounter Instance { get => _instance; }

    GameObject[] _currentItems = new GameObject[3];

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

    public bool IsCurrent(GameObject item)
    {
        for (int i = 0; i < _currentItems.Length; i++)
        {
            if (_currentItems[i] == item)
            {
                return true;
            }
        }

        return false;
    }

    public GameObject AddNewItemToList(GameObject item)
    {
        GameObject temp = _currentItems[0];

        _currentItems[0] = _currentItems[1];
        _currentItems[1] = _currentItems[2];
        _currentItems[2] = item;

        return temp;
    }

    public void RestoreItemToList(GameObject item)
    {
        _currentItems[2] = _currentItems[1];
        _currentItems[1] = _currentItems[0];
        _currentItems[0] = item;
    }
}
