/********************************************************
* Action: Provides base functionality for all actions
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
using System;
using UnityEngine;

public abstract class Action : MonoBehaviour 
{

#if UNITY_EDITOR || RELEASE || DEBUG
    [HideInInspector]
    public bool isVisible = true;
#endif

    public Action()
    {

    }

    //Virtual functions for activated and deactivated events
    //Override in derived classes
    public virtual void OnTriggerActivated(object sender, EventArgs e){}

    public virtual void OnTriggerDeactivated(object sender, EventArgs e){}

#if UNITY_EDITOR || RELEASE || DEBUG
    //Called on the editor GUI. Handle all Exposure here and override in base classes
    //
    public virtual void OnEditorGUI()
    {
        hideFlags = isVisible ? 0 : HideFlags.HideInInspector;
        gameObject.active = false;
        gameObject.active = true;
        
    }
#endif


}
