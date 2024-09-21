using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeItemsCounter : MonoBehaviour
{
    static ThemeItemsCounter _instance;
    public static ThemeItemsCounter Instance { get => _instance; }

    [Min(1)]
    [SerializeField]
    int _maxThemeItems = 1;

    (GameObject, int)[] _currentItems = new (GameObject, int)[1];

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            _currentItems = new (GameObject, int)[_maxThemeItems];
            return;
        }

        if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public bool IsCurrent(int selectableID)
    {
        for (int i = 0; i < _currentItems.Length; i++)
        {
            if (_currentItems[i].Item2 == selectableID)
            {
                return true;
            }
        }

        return false;
    }

    public (GameObject, int) AddNewItemToList((GameObject, int) item)
    {
        (GameObject, int) temp = _currentItems[0];

        for (int i = 0; i < _currentItems.Length; i++)
        {
            if (i + 1 >= _currentItems.Length)
            {
                _currentItems[i] = item;
            }
            else
            {
                _currentItems[i] = _currentItems[i + 1];
            }
        }

        return temp;
    }

    public void RestoreItemToList((GameObject, int) item)
    {
        for (int i = _currentItems.Length - 1; i >= 0; i--)
        {
            if (i == 0)
            {
                _currentItems[i] = item;
            }
            else
            {
                _currentItems[i] = _currentItems[i - 1];
            }
        }
    }
}
