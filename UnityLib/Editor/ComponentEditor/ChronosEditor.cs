using System;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof (ChronosManager))]
public class ChronosEditor : Editor
{
     public override void OnInspectorGUI()
     {
        Debug.Log("On Inspector GUI");
        ChronosManager manager = target as ChronosManager;
        EditorGUILayout.BeginHorizontal("box"); 

        GUILayout.Label("Real Time units");
        manager.RealTimeUnit = (TimeUnit)EditorGUILayout.EnumPopup((Enum)manager.RealTimeUnit);
        manager.RealTime = EditorGUILayout.IntField(manager.RealTime);

        GUILayout.Label("Game units");
        manager.GameUnit = (TimeUnit)EditorGUILayout.EnumPopup((Enum)manager.GameUnit);
        manager.GameTime = EditorGUILayout.IntField(manager.GameTime);

     

        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal(); 
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Game seconds to real time seconds: ", manager.GameSecondsToRealSeconds.ToString());
        EditorGUILayout.EndVertical();
     }
}