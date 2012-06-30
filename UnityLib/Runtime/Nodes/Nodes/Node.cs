using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Node : INodeGUI
{

#if UNITY_EDITOR || RELEASE || DEBUG
    public void DrawWindowGUI(int i)
    {
        throw new NotImplementedException();
    }

    public void DrawNodeConnections()
    {
        throw new NotImplementedException();
    }
#endif
}

