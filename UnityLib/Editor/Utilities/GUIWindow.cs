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

public class GUIWindowNode 
{
    private static bool Resizing = false;
    public static Rect DrawResizeableWindow(Rect drawRect, int id, GUI.WindowFunction func)
    {
        Rect dragArea = new Rect(drawRect.x - 8, drawRect.y - 8, drawRect.width, drawRect.height);
        int gid = EditorGUIUtility.GetControlID(FocusType.Native);
        Event e = Event.current;

        switch (e.GetTypeForControl(gid))
        {
            case EventType.MouseDown:
                {

                    if (drawRect.Contains(e.mousePosition)) // inside this control
                    {
                        EditorGUIUtility.hotControl = gid;
                        Resizing = true;
                        e.Use();
                        Debug.Log("USED");
                    }
                }
                break;

            case EventType.MouseUp:
                {
                    Resizing = false;
                    e.Use();
                }

                break;
        }

        return drawRect;
        //func.Invoke(id);
    }

    private static void  RepaintWindow(Rect drawRect)
    {
        GUILayout.BeginArea(drawRect);
        GUILayout.Box("box");
        GUILayout.EndArea();
    }

}
