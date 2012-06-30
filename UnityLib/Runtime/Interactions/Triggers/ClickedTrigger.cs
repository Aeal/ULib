/********************************************************
* Clicked Trigger: Provides funtionality a clicked trigger
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
using System.Collections.Generic;

public class ClickedTrigger : Trigger
{
    private bool isActivated;
	// Use this for initialization
	void Start ()
	{
	  
        if(Inputbase.Instance == null)
        {
            Debug.LogError("No Input base found." + gameObject.name + "'s Clicked Trigger not initilized");
            return;
        }
	    Inputbase.Instance.OnActionButtonPressedHandle  += OnActivated;
	    Inputbase.Instance.OnActionButtonReleasedHandle += OnDeactivated;
	}
	
    private void OnActivated(object sender, ClickedEventArgs e)
    {
        if (e.TargetObject != gameObject) return;

        isActivated = true;

        TriggerActivated();

    }

    private void OnDeactivated(object sender, ClickedEventArgs e)
    {
        if (!isActivated) return;

        isActivated = false; 

        TriggerDeactivated();

    }

    void OnDestroy()
    {
        if (Inputbase.Instance != null)
        {
           Inputbase.Instance.OnActionButtonPressedHandle -= OnActivated;
        }
        
        
    }
}
