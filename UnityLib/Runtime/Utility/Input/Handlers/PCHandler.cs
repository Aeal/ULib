using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class PCControlHandler : ControlHandler
{
    public KeyCode keyHandleValue;
    public PCControlHandler()
    {
    }
#if UNITY_EDITOR
    public override void OnEditorGUI()
    {
        EditorGUILayout.BeginVertical();
        GUILayout.Label(controlName + " Type: " + GetType(), "IN TitleText");
        if (!isVisible) return;
        KeyCode prev = keyHandleValue;
        keyHandleValue = (KeyCode)EditorGUILayout.EnumPopup("Key Code:", keyHandleValue);
        if(prev != keyHandleValue)
        {
            EditorUtility.SetDirty(this);
        }
        EditorGUILayout.EndVertical();
        base.OnEditorGUI();
    }
#endif

    public override void Update()
    {
        if (Input.GetKeyDown(keyHandleValue))
        {
            Debug.Log("Activated");
            Activated();
        }
        else if (Input.GetKeyUp(keyHandleValue))
        {
            Debug.Log("Deactivated");
            Deactivated();
        }
    }
}

