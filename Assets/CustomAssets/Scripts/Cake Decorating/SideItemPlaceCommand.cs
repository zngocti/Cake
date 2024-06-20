using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideItemPlaceCommand : ICommand
{
    private GameObject  m_prefab;
    private Vector3     m_postion;
    private Quaternion  m_rotation;
    private Material    m_material;
    private int         m_matIndex;
    private ParticleSystem _particles;

    public SideItemPlaceCommand(GameObject prefab, Vector3 position, Quaternion rotation, Material material, int matIndex, ParticleSystem particles)
    {
        this.m_prefab = prefab;
        this.m_postion = position;
        this.m_rotation = rotation;
        this.m_material = material;
        this.m_matIndex = matIndex;
        this._particles = particles;
    }

    public void Execute()
    {
       SideItemPlacer.PlaceSideItem(m_prefab, m_postion, m_rotation, m_material, m_matIndex, _particles);
    }

    public void Undo()
    {
        SideItemPlacer.RemoveLastItem();
    }
}
