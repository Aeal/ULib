using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if UNITY_EDITOR || RELEASE || DEBUG
using UnityEditor;
#endif
using UnityEngine;



public class ActionConnector<T>
{
    Rect connector;
    GUIStyle style;
    bool DrawingConnector, MousedOver = false;
    Vector2 drawLineTo = Vector2.zero;
    private static void curveFromTo(Rect wr, Rect wr2, Color color, Color shadow)
    {
        Drawing.bezierLine(
            new Vector2(wr.x + wr.width, wr.y + 3 + wr.height / 2),
            new Vector2(wr.x + wr.width + Mathf.Abs(wr2.x - (wr.x + wr.width)) / 2, wr.y + 3 + wr.height / 2),
            new Vector2(wr2.x, wr2.y + 3 + wr2.height / 2),
            new Vector2(wr2.x - Mathf.Abs(wr2.x - (wr.x + wr.width)) / 2, wr2.y + 3 + wr2.height / 2), shadow, 5, true, 20);
        Drawing.bezierLine(
            new Vector2(wr.x + wr.width, wr.y + wr.height / 2),
            new Vector2(wr.x + wr.width + Mathf.Abs(wr2.x - (wr.x + wr.width)) / 2, wr.y + wr.height / 2),
            new Vector2(wr2.x, wr2.y + wr2.height / 2),
            new Vector2(wr2.x - Mathf.Abs(wr2.x - (wr.x + wr.width)) / 2, wr2.y + wr2.height / 2), color, 2, true, 20);
    }

    public void DrawLayout(string lable = "", string guiStyle = "")
    {
        style = GUI.skin.FindStyle("Radio");
        Rect next = GUILayoutUtility.GetRect(GUIContent.none,style, GUILayout.ExpandWidth(false));
        Draw(next, lable, guiStyle);
    }


    //Returns the Rect that the button draws in. How do you handle connections
    public void Draw(Rect DrawPos, string lable = "", string guiStyle = "")
    {

        connector = DrawPos;
        style = GUI.skin.FindStyle("Radio");
        int id = EditorGUIUtility.GetControlID(FocusType.Native);
        Event e = Event.current;
        DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
        switch (e.GetTypeForControl(id))
        {

            case EventType.MouseDown:
                {

                    if (connector.Contains(e.mousePosition)) // inside this control
                    {
                        EditorGUIUtility.hotControl = id;
                        Debug.Log("USED");
                        e.Use();
                        DrawingConnector = true;
                        //Handles.DrawLine(new Vector3(DrawPos.x,DrawPos.y), new Vector3(e.mousePosition.x,e.mousePosition.y));
                    }
                }
                break;
            case EventType.MouseUp:
                {
                    DrawingConnector = false;
                    MousedOver = false;
                    if (EditorGUIUtility.hotControl == id)
                        EditorGUIUtility.hotControl = 0;
                }

                break;
            case EventType.MouseDrag:
                {
                    if (DrawingConnector)
                    {
                        Debug.Log("Dragging" + id);
                        DragAndDrop.PrepareStartDrag();
                        DragAndDrop.visualMode = DragAndDropVisualMode.Link;
                        DragAndDrop.StartDrag("Connect!");

                        Event.current.Use();
                        DrawingConnector = true;
                    }


                }
                break;
            case EventType.DragUpdated:
            case EventType.DragPerform:
                {

                    DragAndDrop.visualMode = DragAndDropVisualMode.Move;
               
                    if (e.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();
                        Debug.Log("AcceptDrag" + id);
                    }


                }
                break;
            case EventType.repaint:
                {
                    style.Draw(connector, GUIContent.none, id);
                    connector = GUILayoutUtility.GetLastRect();
                    drawLineTo = e.mousePosition;

                }

                break;
        }
    }

    public void DrawConnections()
    {
         if(DrawingConnector)
             Drawing.bezierLine(GUILayoutUtility.GetLastRect().center, Vector2.zero, drawLineTo, Vector2.zero, Color.black, .5f, true, 20);
    }
}




    
