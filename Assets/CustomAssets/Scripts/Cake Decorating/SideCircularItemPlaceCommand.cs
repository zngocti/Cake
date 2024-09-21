using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCircularItemPlaceCommand : ICommand
{
    static int _currentID = 0;

    private GameObject m_prefab;
    private Transform m_placeAt;
    private Material m_material;
    private int m_matIndex;

    private int _itemID = 0;

    private int _selectableID = 0;
    private string _cakeLayerName;

    static int GetNewID()
    {
        _currentID++;
        return _currentID;
    }

    public SideCircularItemPlaceCommand(GameObject prefab, Transform placeAt, Material material, int matIndex, string cakeLayerName, int selectableID)
    {
        this.m_prefab = prefab;
        this.m_placeAt = placeAt;
        this.m_material = material;
        this.m_matIndex = matIndex;

        _itemID = GetNewID();

        _cakeLayerName = cakeLayerName;
        _selectableID = selectableID;
    }

    public void Execute()
    {
        SideCircularItemPlacer.PlaceSideCircularItem(m_prefab, m_placeAt, m_material, m_matIndex, _itemID, _cakeLayerName, _selectableID);
    }

    public bool IsPlaced()
    {
        //return SideCircularItemPlacer.IsPlaced(m_placeAt, SideCircularItemPlacer.m_materials[SideCircularItemPlacer.m_materials.Count - 1], m_matIndex);
        return SideCircularItemPlacer.IsPlaced(_itemID);
    }

    public GameObject ItemToExecute()
    {
        return m_prefab;
    }

    public void Undo(bool executeOld = true)
    {
        //SideCircularItemPlacer.RemoveCircularItem(m_placeAt, SideCircularItemPlacer.m_materials[SideCircularItemPlacer.m_materials.Count - 1], m_matIndex);
        SideCircularItemPlacer.RemoveCircularItem(_itemID);
    }
}
