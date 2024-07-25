using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeItemPlaceCommand : ICommand
{
    static int _currentID = 0;

    private GameObject m_prefab;
    private Transform m_placeAt;
    private Material m_material;
    private int m_matIndex;

    private (GameObject, int) _previousItemOnList;

    private int _itemID = 0;

    static int GetNewID()
    {
        _currentID++;
        return _currentID;
    }

    public ThemeItemPlaceCommand(GameObject prefab, Transform placeAt, Material material, int matIndex)
    {
        this.m_prefab = prefab;
        this.m_placeAt = placeAt;
        this.m_material = new Material(material);
        this.m_matIndex = matIndex;

        _itemID = GetNewID();
    }

    public void Execute()
    {
        ThemeItemPlacer.PlaceThemeItem(m_prefab, m_placeAt, m_material, m_matIndex, _itemID);
    }

    public bool IsPlaced()
    {
        //return ThemeItemPlacer.IsPlaced(m_placeAt, ThemeItemPlacer.m_materials[ThemeItemPlacer.m_materials.Count - 1], m_matIndex);
        return ThemeItemPlacer.IsPlaced(_itemID);
    }

    public GameObject ItemToExecute()
    {
        return m_prefab;
    }

    public void Undo(bool undoButton = true)
    {
        //ThemeItemPlacer.RemoveThemeItem(m_placeAt, ThemeItemPlacer.m_materials[ThemeItemPlacer.m_materials.Count - 1], m_matIndex);
        ThemeItemPlacer.RemoveThemeItem(_itemID);

        if (_previousItemOnList.Item1 != null && undoButton)
        {
            ThemeItemsCounter.Instance.RestoreItemToList(_previousItemOnList);
            CommandInvoker.GetLastCommandOfItem(_previousItemOnList.Item1).Execute();
        }
        else if (_previousItemOnList.Item1 == null && undoButton)
        {
            ThemeItemsCounter.Instance.RestoreItemToList(_previousItemOnList);
        }
    }

    public void SetItemOverriden((GameObject, int) item)
    {
        _previousItemOnList = item;
    }
}
