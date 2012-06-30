/********************************************************
* Trigger: Provides base functionality for all trigger types
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
using System.Collections.Generic;
using System;


public abstract class Trigger : MonoBehaviour
{
    public delegate void OnTriggerActivatedHandler(object sender, EventArgs e);
    public delegate void OnTriggerDeactivatedHandler(object sender, EventArgs e);
    
    public event OnTriggerActivatedHandler OnTriggerActivated;
    public event OnTriggerDeactivatedHandler OnTriggerDeactivated;

    public Trigger()
    {

    }

    protected void TriggerActivated()
    {
        if (OnTriggerActivated != null) OnTriggerActivated(this, EventArgs.Empty);
    }

    protected void TriggerDeactivated()
    {
        if (OnTriggerDeactivated != null) OnTriggerDeactivated(this, EventArgs.Empty);
    }

#if UNITY_EDITOR || RELEASE || DEBUG
    //Called on the editor GUI. Handle all Exposure here
    public virtual void OnEditorGUI()
    {
       

    }
#endif
  


}
