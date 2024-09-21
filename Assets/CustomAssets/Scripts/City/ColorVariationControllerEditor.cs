using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(ColorVariationController)), CanEditMultipleObjects]
public class ColorVariationControllerEditor : Editor
{
    void GuiLine(int i_height = 1)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, i_height);

        rect.height = i_height;

        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));

    }

    override public void OnInspectorGUI()
    {
        ColorVariationController randomBuildingCreator = (ColorVariationController)target;
        if (GUILayout.Button("Create Building"))
        {
            randomBuildingCreator.CreateBuilding();
        }
        DrawDefaultInspector();
    }
}
#endif