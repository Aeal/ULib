using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ClickedEventArgs : EventArgs
{
    public GameObject TargetObject;

    public ClickedEventArgs(GameObject targetObject)
    {
        TargetObject = targetObject;
    }
}
