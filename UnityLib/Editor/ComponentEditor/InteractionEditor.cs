/********************************************************
* Action: Hooks the interaction system in with the unity editor
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

using UnityEditor;

[CustomEditor(typeof(Interaction))]

public class InteractionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interaction currentTrigger = target as Interaction;

        if(currentTrigger != null)
        {
            currentTrigger.OnEditorGUI();
        }
    }
}

