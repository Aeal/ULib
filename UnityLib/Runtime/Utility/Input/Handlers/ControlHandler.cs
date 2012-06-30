using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

//Mayh have to do the stupid component hack
//Also need to do component
[Serializable]
public abstract class ControlHandler : MonoBehaviour
{
    protected UlibArgs args;

#if UNITY_EDITOR || RELEASE || DEBUG
    [HideInInspector]
    public bool isVisible = true;
#endif
    public string controlName;
    public event OnKeyActivatedHandle OnControlActivated;
    public event OnKeyDeactivatedHandle OnControlDeactivated;

    public ControlHandler()
    {
    }
    public virtual void Update() { } 

    public void SubscribeToActivated(OnKeyActivatedHandle handler)
    {
        if(OnControlActivated != handler)
        {
            OnControlActivated += handler;
        }
    }

    public void UnsubscribeToActivated(OnKeyActivatedHandle handler)
    {
        if (OnControlActivated == handler)
        {
            OnControlActivated -= handler;
        }
    }

    public void SubscribeToDeactivated(OnKeyDeactivatedHandle handler)
    {
        if (OnControlDeactivated != handler)
        {
            OnControlDeactivated += handler;
        }
    }

    public void UnsubscribeToDeactivated(OnKeyDeactivatedHandle handler)
    {
        if (OnControlDeactivated == handler)
        {
            OnControlDeactivated -= handler;
        }
    }

    protected void Activated()
    {
        if (OnControlActivated != null)
        {
            OnControlActivated(this, args);
        }
    }

    protected void Deactivated()
    {
        if (OnControlDeactivated != null)
        {
            OnControlDeactivated(this, args);
        }
    }

#if UNITY_EDITOR
    public virtual void OnEditorGUI()
    {
        hideFlags = isVisible ? 0 : HideFlags.HideInInspector;
        gameObject.active = false;
        gameObject.active = true;

    }
#endif

    public void OnDestroy()
    {
        OnControlActivated = null;
        OnControlDeactivated = null;
    }
}

