﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
#if UNITY_EDITOR || RELEASE || DEBUG
using UnityEditor;
#endif
using UnityEngine;
using Object = UnityEngine.Object;
using System.IO;


public enum ResourceAction
{
    CopyToResources,
    MoveToResources,
    None,
}
public static class Utility
{
    private const string assetPath = "Assets/Resources/";
    //TODO make this better. 
    public static Type[] GetSubClassesAsType(Type baseType, bool getAbstract = false)
    {
        List<Type> ret;
        ret = new List<Type>();
        Type[] list = Assembly.GetAssembly(baseType).GetTypes();
        foreach (Type t in list)
        {
            if (t.IsSubclassOf(baseType) && (!t.IsAbstract || getAbstract))
            {
                ret.Add(t);
            }
        }

        return ret.ToArray();
    }

    public static string[] GetSubClassesAsString(Type baseType, bool getAbstract = false)
    {

        List<string> ret;
        ret = new List<string>();
        Type[] list = Assembly.GetAssembly(baseType).GetTypes();
        foreach (Type t in list)
        {
            if (t.IsSubclassOf(baseType) && (!t.IsAbstract || getAbstract))
            {
                ret.Add(t.Name);
            }
        }

        return ret.ToArray();

    }
#if UNITY_EDITOR || RELEASE || DEBUG
    public static string GetResourcesPath(Object target, ResourceAction action = ResourceAction.None, string path ="" )
    {
        if (target == null) return string.Empty;
        string resourcePath = AssetDatabase.GetAssetPath(target);

        if(resourcePath.Contains(assetPath))
        {
            resourcePath = resourcePath.Replace(assetPath, string.Empty);
            resourcePath = resourcePath.Replace(Path.GetExtension(resourcePath), string.Empty);
            return resourcePath;
        }
        Debug.LogError(target.name + " Not found in resources, please move the object into your Assets/Resources folder");
        return string.Empty;
        //string validation = "";
        //switch (action)
        //{
        //    case ResourceAction.None:
        //        Debug.LogWarning(target.name + " Does not exist in resource databse");
        //        return string.Empty;
        //    case ResourceAction.CopyToResources:
        //         validation = AssetDatabase.ValidateMoveAsset(resourcePath, assetPath + path);
        //        if (validation == string.Empty)
        //        {
        //            AssetDatabase.CopyAsset(resourcePath, assetPath + path);
        //            return path;
        //        }
        //        Debug.LogError(validation);
        //        break;
                
        //    case ResourceAction.MoveToResources:
        //        validation = AssetDatabase.ValidateMoveAsset(resourcePath, assetPath + path);
        //        if(validation == string.Empty)
        //        {
        //            AssetDatabase.CopyAsset(resourcePath, assetPath + path);
        //            return path;
        //        }
        //        Debug.LogError(validation);
        //        break;
        //}
        //return string.Empty;
    }
#endif

}
