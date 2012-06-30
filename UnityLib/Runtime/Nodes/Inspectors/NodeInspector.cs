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

public class NodeInspector<T> : NodeInspectorBase where T : INodeGUI, new()
{
   
    private T nodeTarget;
    //EditorVars


    public NodeInspector()   
    {
        nodeTarget = new T();
    }

    #region Interface Functions

    protected void DrawNodeGUI(int index)
    {
        nodeTarget.DrawWindowGUI(index);
        
    }
   
    public virtual void OnFocusedGUI()
    {
        throw new NotImplementedException();
    }
    #endregion

}

public class ComponentNodeInspector<T> : NodeInspectorBase where T : MonoBehaviour, INodeGUI
{
    private GameObject nodeTargetObject = null;
    private T nodeTarget;
    protected void DrawNodeGUI(int index)
    {
        if (nodeTargetObject == null) return;
            
        if (nodeTarget == null)
        {
            nodeTarget = nodeTargetObject.GetComponent<T>();
        }
      
        nodeTarget.DrawWindowGUI(index);

    }
   

}

