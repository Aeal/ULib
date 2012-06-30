using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable] 
public class Tag : MonoBehaviour, IInspectable
{
    public string[] Tags;
    
    public void Start()
    {
           
    }
#if UNITY_EDITOR || RELEASE || DEBUG
    public void OnInspectorGUI()
    {
        
    }
#endif
}