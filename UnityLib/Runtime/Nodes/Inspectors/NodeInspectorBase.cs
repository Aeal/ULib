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
 
public class NodeInspectorBase
{
    public bool show = true;
    private static Rect defaultPosition = new Rect(0, 0, 300, 250);
    private Rect currentPosition = defaultPosition;
    private Rect windowHandle;
    private Vector3 ClickedPos;
    private string nodeName;
    private bool isResizing;
    private INodeGUI target; 


    public NodeInspectorBase() { }

    public virtual void OnNodeGUI(int i)
    {
        GUI.Button(new Rect(currentPosition.width + GUIOption.xPadding - (GUIOption.MinControlWidth), 0 + GUIOption.yPadding, GUIOption.CloseButtonSize, GUIOption.CloseButtonSize), GUIContent.none, "WinBtnCloseWin");
        show = GUI.Toggle(new Rect(currentPosition.width + (GUIOption.xPadding * 3) - (GUIOption.MinControlWidth), 0 + GUIOption.yPadding, GUIOption.CloseButtonSize, GUIOption.CloseButtonSize), Showing, GUIContent.none, "WinBtnMinWin");
        if (show)
        {
            //EditorGUIUtility.LookLikeControls();
           target.DrawWindowGUI(i);

        }
        EditorGUILayout.Separator();
        GUI.DragWindow();
        //Set name and other things here
    }
    

    #region Auto properties Members
    public Rect CurrentPoistion
    {
        get
        {
            return currentPosition;
        }
        set
        {
            currentPosition = value;
        }
    }

    public bool Showing
    {
        get
        {
            return show;
        }
        set
        {
            show = value;
        }
    }

    public string NodeName
    {
        get
        {
            return nodeName;
        }
        set
        {
            nodeName = value;
        }
    }
    #endregion
}