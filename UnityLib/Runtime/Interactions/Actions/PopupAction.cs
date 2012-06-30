/********************************************************
* Popup Action: shows a popup with a message in it to the player
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

public class PopupAction : Action
{
    public string Message;

    private bool isShowing = false;

    public override void OnTriggerActivated(object sender, System.EventArgs e)
    {
        isShowing = true; 
        base.OnTriggerActivated(sender, e);
    }

    void OnGUI()
    {
        if (!isShowing) return;

        GUILayout.BeginArea(new Rect(Screen.width * .4f, Screen.height * .4f, Screen.width * .2f, Screen.height * .2f),"box");
        GUILayout.TextArea(Message);

        if(GUILayout.Button("Close"))
        {
            isShowing = false;
        }

        GUILayout.EndArea();
    }

    public void Close()
    {
        isShowing = false;
    }

#if UNITY_EDITOR || RELEASE || DEBUG
    public override void OnEditorGUI()
    {
        GUILayout.Label("Action Type: " + GetType(), "IN TitleText");
        base.OnEditorGUI();
    }
#endif
}
