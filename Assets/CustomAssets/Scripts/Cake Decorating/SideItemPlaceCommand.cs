using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideItemPlaceCommand : ICommand
{
    static int _currentID = 0;

    private GameObject  m_prefab;
    private Vector3     m_postion;
    private Quaternion  m_rotation;
    private Material    m_material;
    private int         m_matIndex;
    private ParticleSystem _particles;
    private Transform _itemTransform;

    private int _itemID = 0;

    static int GetNewID()
    {
        _currentID++;
        return _currentID;
    }

    public SideItemPlaceCommand(GameObject prefab, Vector3 position, Quaternion rotation, Material material, int matIndex, ParticleSystem particles)
    {
        this.m_prefab = prefab;
        this.m_postion = position;
        this.m_rotation = rotation;
        this.m_material = material;
        this.m_matIndex = matIndex;
        this._particles = particles;

        _itemID = GetNewID();
    }

    public void Execute()
    {
       _itemTransform = SideItemPlacer.PlaceSideItem(m_prefab, m_postion, m_rotation, m_material, m_matIndex, _particles, _itemID);
    }

    public bool IsPlaced()
    {
        //return SideItemPlacer.IsPlaced(_itemTransform, SideItemPlacer.m_materials[SideItemPlacer.m_materials.Count - 1], m_matIndex);
        return SideItemPlacer.IsPlaced(_itemID);
    }

    public GameObject ItemToExecute()
    {
        return m_prefab;
    }

    public void Undo(bool executeOld = true)
    {
        //SideItemPlacer.RemoveSideItem(_itemTransform, SideItemPlacer.m_materials[SideItemPlacer.m_materials.Count - 1], m_matIndex);
        SideItemPlacer.RemoveSideItem(_itemID);
    }
}
