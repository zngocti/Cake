using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularItemsCounter : MonoBehaviour
{
    static CircularItemsCounter _instance;
    public static CircularItemsCounter Instance { get => _instance; }

    Dictionary<string, List<(GameObject, int)>> _circularItems = new Dictionary<string, List<(GameObject, int)>>();

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

    public void ClearItemList()
    {
        foreach (var item in _circularItems)
        {
            if (item.Value.Count > 0)
            {
                item.Value.Clear();
            }
        }

        _circularItems.Clear();
    }

    public bool IsOnCakeLayer(string cakeLayer, int selectableID)
    {
        if (!_circularItems.ContainsKey(cakeLayer))
        {
            return false;
        }

        for (int i = 0; i < _circularItems[cakeLayer].Count; i++)
        {
            if (_circularItems[cakeLayer][i].Item2 == selectableID)
            {
                return true;
            }
        }

        return false;
    }

    public void RePaint(string cakeLayer, int selectableID, Color color)
    {
        for (int i = 0; i < _circularItems[cakeLayer].Count; i++)
        {
            if (_circularItems[cakeLayer][i].Item2 == selectableID)
            {
                MeshRenderer temp;

                if (_circularItems[cakeLayer][i].Item1.TryGetComponent<MeshRenderer>(out temp))
                {
                    if (temp.material.color == color)
                    {
                        return;
                    }

                    temp.material.color = color;
                }
            }
        }
    }

    public void AddToList(string cakeLayer, (GameObject, int) circularItem)
    {
        if (!_circularItems.ContainsKey(cakeLayer))
        {
            List<(GameObject, int)> list = new List<(GameObject, int)>();
            list.Add(circularItem);
            _circularItems.Add(cakeLayer, list);
            return;
        }

        for (int i = _circularItems[cakeLayer].Count - 1; i >= 0 ; i--)
        {
            if (_circularItems[cakeLayer][i].Item1 == null)
            {
                _circularItems[cakeLayer].RemoveAt(i);
                continue;
            }
        }

        _circularItems[cakeLayer].Add(circularItem);
    }
}
