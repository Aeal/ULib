using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public interface INodeGUI
{
#if UNITY_EDITOR || RELEASE || DEBUG
    void DrawWindowGUI(int i);
    void DrawNodeConnections();
#endif
} 