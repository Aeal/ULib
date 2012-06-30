 /********************************************************
 * Volume Trigger: Provides funtionality for a basic volume trigger
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class VolumeTrigger : Trigger
{
   private BoxCollider volume;

    void Start()
    {
        Debug.Log("Starting Volume Trigger");
        gameObject.GetComponent<BoxCollider>().isTrigger = true; ;
    }

    void OnTriggerEnter(Collider other)
    {
        TriggerActivated();
    }

    void OnTriggerExit(Collider other)
    {
        TriggerDeactivated();
    }

}

