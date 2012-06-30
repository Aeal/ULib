using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;




public class InputBase : MonoBehaviour, INodeGUI
{
    //May have to save out some settings before play and load back in after play
    private const int LayerMask = 1<<8;
    private const float raycastDistance = 4.0f;

    
    private void Refresh()
    {
         controls = null;
        controls = new Dictionary<string, ControlHandler>();
        foreach(ControlHandler handler in gameObject.GetComponents<ControlHandler>())
        {
            AddControl(handler);
        }
    }
    public void Start()
    {
        Refresh();
    }
    protected virtual void Update()
    {
      
    }


    private static Dictionary<string, ControlHandler> controls = new Dictionary<string,ControlHandler>();

    

    public static bool AddControl(ControlHandler handlerToAdd) 
    {
        if(controls == null)
        {
            controls = new Dictionary<string, ControlHandler>();
        }
        if(controls.ContainsKey(handlerToAdd.controlName))
        {
            Debug.LogWarning("Cannot add controls of the same name");
            return false;
        }
        else
        {
            controls.Add(handlerToAdd.controlName, handlerToAdd);
            return true;
        }
    }
    public static bool RemoveControl(ControlHandler handlerToRemove)
    {
        if (controls.ContainsKey(handlerToRemove.controlName))
        {
            controls.Remove(handlerToRemove.controlName);
            return true;
        }
        else
        {
            return false;
        }

    }

    public static void SubscribeToControl(string controlName, OnKeyActivatedHandle onControlActivate, OnKeyDeactivatedHandle onControlDeactivate)
    {
        
        if(controls.ContainsKey(controlName))
        {
            Debug.Log("Subscribing to control: " + controlName);
            controls[controlName].SubscribeToActivated(onControlActivate);
            controls[controlName].SubscribeToDeactivated(onControlDeactivate);
        }
    }

    public static void UnsubscribeToControl(string controlName, OnKeyActivatedHandle onControlActivate, OnKeyDeactivatedHandle onControlDeactivate)
    {
        Debug.Log("Unsubscribing to control: " + controlName);

        if(controls.ContainsKey(controlName))
        {
            controls[controlName].UnsubscribeToActivated(onControlActivate);
            controls[controlName].UnsubscribeToDeactivated(onControlDeactivate);
        }

    }

    public static string[] Controls
    {
        get { return controls.Keys.ToArray(); }
    }

    public static GameObject GetClickedObject()
    {
        Debug.Log("Checking for objects");
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity))
        {
            Debug.Log("Hit " + hitInfo.collider.gameObject.name);
            return hitInfo.collider.gameObject;
        }
        
        return null;
    }
#if UNITY_EDITOR

    private string   controlToAdd     = "";
    private string[] handleNames      = Utility.GetSubClassesAsString(typeof(ControlHandler));
    private Type[]   handleTypes      = Utility.GetSubClassesAsType(typeof(ControlHandler));
    private int      handleSelection  = 0;

    public void OnEditorGUI()
    {
        if(controls == null)
        {
            Refresh();
        }

        controlToAdd = EditorGUILayout.TextField("Control Name", controlToAdd);
        handleSelection = EditorGUILayout.Popup("Handle Type: ", handleSelection, handleNames);
        if(GUILayout.Button("Add"))
        {
            Debug.Log("Adding type of: " + handleTypes[handleSelection]);
            ControlHandler handle = gameObject.AddComponent(handleTypes[handleSelection]) as ControlHandler;
            handle.controlName = controlToAdd;
                //handleTypes[handleSelection].GetConstructor(Type.EmptyTypes).Invoke(new[]{controlToAdd}) as ControlHandler;
            if(!AddControl(handle))
            {
                DestroyImmediate(handle);
                
            }
            EditorUtility.SetDirty(this);
            PrefabUtility.SetPropertyModifications(PrefabUtility.GetPrefabObject(gameObject),PrefabUtility.GetPropertyModifications(gameObject));
            
        }

        EditorGUILayout.Separator();
        if(GUILayout.Button("Refresh"))
        {
            Refresh();
        }
        EditorGUILayout.Separator();

        for (int index = 0; index < controls.Values.Count; index++)
        {
            ControlHandler control = controls.Values.ElementAt<ControlHandler>(index);
            EditorGUILayout.BeginHorizontal("box");
            int selection = DrawDefaultControlControls(control);
            switch (selection)
            {
                case 0:
                    control.OnEditorGUI();
                    break;
                case 1:
                    RemoveControl(control);
                    DestroyImmediate(control);
                    index--;
                    break;
                case 2:
                    //do nothing is minimized
                    break;
                default:
                    control.OnEditorGUI();
                    break;
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private int DrawDefaultControlControls(ControlHandler controlTarget)
    {
        //Title bar here
        int selection = 0;
        if (GUILayout.Button("X", "WinBtnCloseWin", GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false)))
        {
            selection = 1;
            return selection;

        }
        controlTarget.isVisible = EditorGUILayout.Toggle(controlTarget.isVisible,
                                                         controlTarget.isVisible ? "WinBtnMinWin" : "WinBtnMaxWin",
                                                         GUILayout.MaxWidth(15), GUILayout.MaxHeight(15));

        return selection;
    }

    public void  DrawWindowGUI(int i)
    {
 	    throw new NotImplementedException();
    }

    public void  DrawNodeConnections()
    {
 	    throw new NotImplementedException();
    }
#endif
}


