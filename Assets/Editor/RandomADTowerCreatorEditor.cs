using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandomADTowerCreator))]
public class RandomADTowerCreatorEditor : Editor
{

    void GuiLine(int i_height = 1)

    {

        Rect rect = EditorGUILayout.GetControlRect(false, i_height);

        rect.height = i_height;

        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));

    }

    override public void OnInspectorGUI()
    {
        RandomADTowerCreator randomBuildingCreator = (RandomADTowerCreator)target;
        if (GUILayout.Button("Create Building"))
        {
            randomBuildingCreator.CreateBuilding();
        }
        DrawDefaultInspector();
    }
}
