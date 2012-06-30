/********************************************************
* Sound Action: Plays a sound when a trigger is activated
* Author: Tyler Steele
* Created: 1/15/12
* Modified: 4/25/12
* Modified by: Tyler Steele
* 
* Notes:
* When intergrating with sound manager use a popup of all sounds in the project
* 
* 
* 
* Copyright 2012 Tyler Steele, All rights reserved.
**********************************************************/
using UnityEngine;
using System.Collections;
/* TODO
 * Add in stopping functionality when deactivated
 * 
 * 
 */

[RequireComponent(typeof(AudioSource))]
public class SoundAction : Action
{
    public AudioSource source = null;

    public bool hasPlayed = false;


    public AudioClip clipToPlay = null;

    public bool oneShot = false,
                loop = false;

    public ulong Delay;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        
        if(source == null)
        {
            Debug.LogError("No Audiosource found on: " + gameObject.name + ". Sound action not initilized");
        }
    }
    
    public override void OnTriggerActivated(object sender, System.EventArgs e)
    {
        if(source.isPlaying)return;

        if(oneShot)
        {

            if (hasPlayed)
            {
                return;
            }

            source.PlayOneShot(clipToPlay);
            hasPlayed = true;
            return;
        }

        source.loop = loop;
        source.clip = clipToPlay;
        source.Play(Delay);
        base.OnTriggerActivated(sender, e);
    }

#if UNITY_EDITOR || RELEASE || DEBUG
    public override void OnEditorGUI()
    {
        GUILayout.Label("Action Type: " + GetType(), "IN TitleText");
        base.OnEditorGUI();
    }
#endif
}
