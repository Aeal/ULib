using System;
using System.Collections.Generic;
using System.Xml.Serialization;
#if UNITY_EDITOR || RELEASE || DEBUG
using UnityEditor;
#endif
using UnityEngine;


public class NodeSceneView : AdvancedSceneView
{
    public delegate void NodeSceneGUIHandler();

    public static event NodeSceneGUIHandler OnNodeSceneGUI;
    private static NodeSceneView instance;
    [MenuItem("Window/Node Scene View")]
    public static void ShowWindow()
    {
        if (instance == null)
            instance = CreateInstance<NodeSceneView>();
        instance.Show();

        foreach (var sceneNode in FindSceneObjectsOfType(typeof (ComponentNode)))
        {
        }

    }

    
    public void OnGUI()
    {
        //TODO
        base.OnGUI();

        GUILayout.BeginArea(new Rect(50, 50, 500, 500));

        if(OnNodeSceneGUI != null)
        {
            OnNodeSceneGUI();
        }

        GUILayout.EndArea();
    }

}

