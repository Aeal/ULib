using System;
using System.Xml.Serialization;
#if UNITY_EDITOR || RELEASE || DEBUG
using UnityEditor;
#endif
using UnityEngine;

[Serializable]
public abstract class ActionNode : Node
{

    //ARGS element which we will allow the user to modify



    //and an action that the user selects. actions can be done in a  
    // GAMOBJECTNAME:actionName

    protected ActionNode()
    {
    }

    /// <summary>
    /// Ovverride this method and perform the nodes logic in here
    /// </summary>
    public virtual void DoAction()
    {
    }

    /// <summary>
    /// Put your nodes initilization logic in here, be sure to call it in the runtime
    /// </summary>
    public virtual void Initilize()
    {
    }

    /// <summary>
    /// Do any logic in here that you may need to prepare your node for serialization
    /// </summary>
    public virtual void BuildForSerialize()
    {
    }

    public virtual void Update()
    {
    }

    /// <summary>
    /// Do anything here that needs to happen when the node is removed or destroyed
    /// </summary>
    public virtual void OnDestroy()
    {
    }

}
