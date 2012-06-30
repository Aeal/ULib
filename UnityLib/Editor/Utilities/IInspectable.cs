using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//denotes if the class implements its own inspector
public interface IInspectable
{

#if UNITY_EDITOR || DEBUG || RELEASE
    void OnInspectorGUI();
#endif
}

