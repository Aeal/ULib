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

public class GUIWindow 
{
    public static Rect DrawResizeableWindow(Rect drawRect, int id, GUI.WindowFunction func)
    {
        Rect dragArea = new Rect(drawRect.x - 8, drawRect.y - 8, drawRect.width, drawRect.height);
        if(Event.current.control)
        {
            if(Event.current.isMouse)
            {
               if(Event.current.type == EventType.MouseDrag)
               {
                   Debug.Log("DRAGGING");
               }
            }
        }
        GUILayout.BeginArea(drawRect);
        GUILayout.Box("box");
        GUILayout.EndArea();
        return drawRect;
        //func.Invoke(id);
    }

}
