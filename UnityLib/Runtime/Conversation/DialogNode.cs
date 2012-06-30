using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
#if UNITY_EDITOR || RELEASE || DEBUG
using UnityEditor;
#endif
using UnityEngine;
using System.Text;


//3 different kinds of nodes
//Action
//Dialog
//Selection



public partial class DialogNode : INodeGUI
{
   

    public bool skippable = false;
    public Speakers speaker = Speakers.NPC;
    public string AudioClipRef;
    public string subtitle = "(enter subtitle text)";
    public string AnimationToPlay;
    [XmlIgnore]
    public AudioClip spoken = null;

    public Waits wait = Waits.Audio;
    public float waitTime = 0;

    public DialogNode()
    {
#if UNITY_EDITOR || RELEASE || DEBUG
        InitializeGUI();
#endif
    }

    public void Initialize()
    {
        Debug.Log("Initilizing Dialog Item: " + subtitle);
        if (AudioClipRef != String.Empty)
        {
            spoken = (AudioClip)Resources.Load(AudioClipRef);
            if (spoken == null)
                Debug.Log("There was an error loading " + AudioClipRef);
        }

        DialogNode self = this;
    }

    public void Update()
    {
        
    }

   
   
}
