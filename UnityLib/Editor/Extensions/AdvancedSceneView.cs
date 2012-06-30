using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
#if UNITY_EDITOR || RELEASE || DEBUG
using UnityEditor;
#endif
using UnityEngine;

public abstract class AdvancedSceneView : SceneView
{

    public void OnGUI()
    {
        SceneView sceneView = this as SceneView;
        MethodInfo dynMethod = sceneView.GetType().GetMethod("OnGUI", BindingFlags.NonPublic | BindingFlags.Instance);
        dynMethod.Invoke(this, new object[0]);
    }
}

