using UnityEngine;
using System.Collections;
using Grid;
using UnityEditor;

[CustomEditor(typeof(GridController))]
public class GridEditor : Editor
{

	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GridController myTarget = (GridController)target;
        if(GUILayout.Button("Generate grid"))
        {
            myTarget.GenerateGrid();
        }
    }


}
