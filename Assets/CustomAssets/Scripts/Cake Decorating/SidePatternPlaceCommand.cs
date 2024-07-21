using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePatternPlaceCommand : ICommand
{
    private Transform m_placeAt;
    private Material m_material;

    public SidePatternPlaceCommand(Transform placeAt, Material material)
    {
        this.m_placeAt = placeAt;
        this.m_material = material;
    }

    public void Execute()
    {
        SidePatternPlacer.PlaceSidePattern(m_placeAt, m_material);
    }

    public bool IsPlaced()
    {
        return SidePatternPlacer.IsPlaced(m_placeAt, SidePatternPlacer.m_materials[SidePatternPlacer.m_materials.Count - 1]);
    }

    public GameObject ItemToExecute()
    {
        return null;
    }

    public void Undo(bool executeOld = true)
    {
        SidePatternPlacer.RemoveSidePattern(m_placeAt, SidePatternPlacer.m_materials[SidePatternPlacer.m_materials.Count - 1]);
    }
}
