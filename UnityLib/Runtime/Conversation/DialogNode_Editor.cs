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

//This is the part of the class that contains the editor portions
[Serializable]
public partial class DialogNode
{
#if UNITY_EDITOR || RELEASE || DEBUG
  
    ActionConnector<DialogNode> connector = new ActionConnector<DialogNode>();
    public void InitializeGUI()
    {
        
    }

    public void BuildForSerialize()
    {
       
        Debug.Log("Checking for audio clip");
        if (spoken != null)
        {
            Debug.Log("Audio clip found fetching resource path");
            AudioClipRef = Utility.GetResourcesPath(spoken);

            Debug.Log("Fetch results: " + AudioClipRef);
        }
    }
   
    public void DrawWindowGUI(int winID)
    {
        EditorGUIUtility.LookLikeControls();
        //EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
        skippable = EditorGUILayout.Toggle("Skipabble: ", skippable, "Radio");
        speaker = (Speakers)EditorGUILayout.EnumPopup("Speaker: ", speaker,"Popup");

        spoken = (AudioClip)EditorGUILayout.ObjectField("Audio Clip: ",spoken, typeof(AudioClip), false);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.PrefixLabel("Subtitle");
        EditorStyles.textField.wordWrap = true;
        subtitle = EditorGUILayout.TextArea(subtitle);
        EditorGUILayout.EndHorizontal();

        
        wait = (Waits)EditorGUILayout.EnumPopup("Wait Time: " , wait);
        if (wait == Waits.Time)
        {
            waitTime = EditorGUILayout.FloatField("Wait time: ", waitTime);
        }

        connector.DrawLayout();
        //EditorGUILayout.EndVertical();

         // Make the windows be draggable.
    }

    public void DrawNodeConnections()
    {
        connector.DrawConnections();
    }
#endif


    #region base node code
   
    #endregion
}
