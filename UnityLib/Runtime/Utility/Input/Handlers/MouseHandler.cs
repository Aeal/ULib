using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum MouseButton
{
    LeftMouse,
    RightMouse,
    MiddleMouse,
}

public class MouseHandler : ControlHandler
{
    public MouseButton mouseButton;
    //TODO make a mouse inputHandler
#if UNITY_EDITOR
    public override void OnEditorGUI()
    {
        EditorGUILayout.BeginVertical();
        GUILayout.Label(controlName + " Type: " + GetType(), "IN TitleText");
        if (!isVisible) return;
        mouseButton =(MouseButton) EditorGUILayout.EnumPopup("Mouse Button: ", mouseButton);
        EditorGUILayout.EndVertical();
        base.OnEditorGUI();
    }
#endif

    public override void Update()
    {
        if (Input.GetMouseButtonDown((int)mouseButton))
        {
            Activated();
        }
        else if(Input.GetMouseButtonUp((int)mouseButton))
        {
            Deactivated();
        }
    }
}
	
