using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BackgroundMultiplierTool))]
public class BackgroundMultiplierToolEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        var script = (BackgroundMultiplierTool) target;
        if (GUILayout.Button("Populate Backgrounds")) {
            script.PopulateBackgrounds();
        } if (GUILayout.Button("Clear Backgrounds")) {
            script.ClearBackgrounds();
        }
    }
}