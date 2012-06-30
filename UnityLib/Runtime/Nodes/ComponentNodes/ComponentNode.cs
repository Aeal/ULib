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


[ExecuteInEditMode]
public partial class ComponentNode : MonoBehaviour, INodeGUI
{
    public GameObject Target;
    
    void Start()
    {
#if UNITY_EDITOR || RELEASE || DEBUG
        
        //if (NodeSceneView.OnNodeSceneGUI != DrawSceneGUI)
        //{
        //    NodeSceneView.OnNodeSceneGUI += DrawSceneGUI;
        //}
#endif
    }

}

