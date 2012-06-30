using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;


//Generic abstract class for component editors
//Allows you to implement the IInspectable interface in a Monobehaviour
//To easily override the custom inspector, and keep the editor code in the class.
//See MazeGenerator.cs for an example on how to use it.
public abstract class ComponentEditor<T> : Editor where T : MonoBehaviour, IInspectable
{
    private T instance = null;
    public override void OnInspectorGUI()
    {
        if (instance == null)
        {
            instance = target as T;
            if (target == null)
            {
                Debug.LogError("There was an error setting the target");
                return;
            }
        }
        instance.OnInspectorGUI();
        base.OnInspectorGUI();
    }
}

