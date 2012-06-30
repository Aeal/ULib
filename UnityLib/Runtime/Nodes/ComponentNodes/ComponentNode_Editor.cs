

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
#if UNITY_EDITOR || RELEASE || DEBUG
using UnityEditor;
#endif
using UnityEngine;
using System.Text;


public partial class ComponentNode : MonoBehaviour, INodeGUI
{

    
#if UNITY_EDITOR || RELEASE || DEBUG

    Rect screenPosition = new Rect(50, 50, 250, 500);
    
    Vector2 currentPosition, startPosition, deltaPosition;
    
    public void  DrawWindowGUI(int i)
    {
        EditorGUI.BeginChangeCheck();
        SerializedObject serializedObject = new SerializedObject(this);
        serializedObject.Update();
        SerializedProperty iterator = serializedObject.GetIterator();
        for (bool flag = true; iterator.NextVisible(flag); flag = false)
        {
            EditorGUILayout.PropertyField(iterator, true, new GUILayoutOption[0]);
        }
        serializedObject.ApplyModifiedProperties();
        EditorGUI.EndChangeCheck();

        GUI.DragWindow(new Rect(0, 0, 10000, 20));

    }

    public void DrawNode(SceneView view)
    {
       
        Handles.BeginGUI();
        //GUILayout.BeginArea(new Rect(50, 50, 500, 500));
        view.BeginWindows();
        screenPosition = GUI.Window(0, screenPosition, DrawWindowGUI, name );
        view.EndWindows();
       // GUILayout.EndArea();
        Handles.EndGUI();


    }

    public void  DrawNodeConnections()
    {

    }

#endif
}

