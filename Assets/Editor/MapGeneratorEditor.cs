using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshGenerator))]
public class MapGeneratorEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        MeshGenerator mapDisplay = (MeshGenerator)target;

        if (DrawDefaultInspector())
        {
            if (mapDisplay.autoUpdate)
            {
                mapDisplay.GenerateMap();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            mapDisplay.GenerateMap();
        }
    }
}
