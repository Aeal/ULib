/********************************************************
* Interaction: provides a container for the triggers and corresponding actions
* Author: Tyler Steele
* Created: 1/15/12
* Modified: 4/25/12
* Modified by: Tyler Steele
* 
* Notes:
* 
* 
* 
* 
* Copyright 2012 Tyler Steele, All rights reserved.
**********************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR || RELEASE || DEBUG
using UnityEditor;
using System;
#endif
public class Interaction : MonoBehaviour 
{

    private List<Action> Interactions = null;
    private Trigger Trigger = null;

 // Use this for initialization
    protected void Start()
    {
        
        Action[] actions =  gameObject.GetComponents<Action>() as Action[];
        Trigger = gameObject.GetComponent<Trigger>();
        if (Trigger == null)
        {
            Debug.Log("No Trigger found." + gameObject.name + " interaction not initilized!");
            return;
        }

        if (actions == null)
        {
            Debug.Log("No actions found." + gameObject.name + " interaction not initilized!");
            return;
        }
        foreach (Action action in actions)
        {
            if (Interactions == null)
            {
                Interactions = new List<Action>();
            }
            Interactions.Add(action);
            Debug.Log("Adding interaction: " + action.GetType());
            Trigger.OnTriggerActivated += action.OnTriggerActivated;
            Trigger.OnTriggerDeactivated += action.OnTriggerDeactivated;

        }
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    private void OnDestroy()
    {
        foreach (Action action in Interactions)
        {
            Trigger.OnTriggerActivated -= action.OnTriggerActivated;
            Trigger.OnTriggerDeactivated -= action.OnTriggerDeactivated;
        }
    }
    #region Editor
#if UNITY_EDITOR || RELEASE || DEBUG

    private string[] possibleActions = null,
                     possibleTriggers = null;

    private Type[]   actionTypes  = null,
                     triggerTypes = null;

    private int      actionSelected = 0,
                     triggerSelected = 0;

    //Base on EditorGUI
    public virtual void OnEditorGUI()
    {
        //Selection.activeObject = GUI.skin;
        CheckDraw();

        DrawInteractionControls();

        DrawTriggerControls();

        DrawAddActionControls();

        DrawActionGUIControls();
        
    }

    //Draw all gui controsl for the trigger here
    private void DrawTriggerControls()
    {
        int currentTriggerSelected = triggerSelected;
        triggerSelected = EditorGUILayout.Popup(triggerSelected, possibleTriggers);

        if (currentTriggerSelected != triggerSelected || Trigger == null)
        {

            DestroyImmediate(Trigger);
            Trigger = gameObject.AddComponent(triggerTypes[triggerSelected].ToString()) as Trigger;

        }
        
    }

    //Draw any controls pertaining to the interaction here
    private void DrawInteractionControls()
    {
    }

    //Draws the controls that handle adding actions
    private void DrawAddActionControls()
    {
        EditorGUILayout.BeginHorizontal("box");
        actionSelected = EditorGUILayout.Popup(actionSelected, possibleActions);
        if (GUILayout.Button("Add " + possibleActions[actionSelected], "minibutton"))
        {
            Action actionToAdd = gameObject.AddComponent(actionTypes[actionSelected].ToString()) as Action;
            if (actionToAdd != null)
            {
                Interactions.Add(actionToAdd);
            }
            else
            {
                Debug.Log("Interaction not added");
            }
        }
        EditorGUILayout.EndHorizontal();

    }

    //Draws all of the action controls
    private void DrawActionGUIControls()
    {
        for (int index = 0; index < Interactions.Count; index++)
        {
            Action action = Interactions[index];
            EditorGUILayout.BeginHorizontal("box");
            int selection = DrawDefaultActionControls(action);
            switch (selection)
            {
                case 0:
                    action.OnEditorGUI();
                    break;
                case 1:
                    Interactions.Remove(action);
                    DestroyImmediate(action);
                    index--;
                    break;
                case 2:
                    //do nothing is minimized
                    break;
                default:
                    action.OnEditorGUI();
                    break;
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    //Draws controls for hiding and removing actions
    private int DrawDefaultActionControls(Action controlTarget)
    {
        //Title bar here
        int selection = 0;
        if (GUILayout.Button("X", "WinBtnCloseWin", GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false)))
        {
            selection =  1;
            return selection;

        }
        controlTarget.isVisible = EditorGUILayout.Toggle(controlTarget.isVisible,
                                                         controlTarget.isVisible ? "WinBtnMinWin" : "WinBtnMaxWin",
                                                         GUILayout.MaxWidth(15), GUILayout.MaxHeight(15));
       
        return selection;
    }

    //Checks to make sure that there are no null references and if there is they are logged
    private void CheckDraw()
    {
        if (Interactions == null)
        {
            Interactions = new List<Action>();
            foreach (Action action in gameObject.GetComponents<Action>())
            {
                Interactions.Add(action);
            }

        }
        if (Trigger == null)
        {
            Trigger = gameObject.GetComponent<Trigger>();
            if (Trigger == null)
            {
                Debug.LogWarning("No Trigger found on " + gameObject.name + ". Please Add one");
            }
        }
        if (possibleActions == null)
        {
            possibleActions = Utility.GetSubClassesAsString(typeof(Action));
        }
        if (actionTypes == null)
        {
            actionTypes = Utility.GetSubClassesAsType(typeof(Action));
        }

        if (possibleTriggers == null)
        {
            possibleTriggers = Utility.GetSubClassesAsString(typeof(Trigger));
        }
        if (triggerTypes == null)
        {
            triggerTypes = Utility.GetSubClassesAsType(typeof(Trigger));
        }

    }
#endif   
    #endregion

}
