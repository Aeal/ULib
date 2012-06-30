
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class DebugConsole : ComponentSingleton<DebugConsole>
{    
    private enum DebugMode
    {
        Console,
        Input,
        Watch,
    }

    private const KeyCode openKey = KeyCode.BackQuote;
    private bool isShowing;
    private Rect windowRect = new Rect(0, 0, 250, 300),
                 dragRect = new Rect(0, 0, 10000, 10000);

    private DebugMode currentMode = DebugMode.Console;
    private Dictionary<string,KeyValuePair<object,string>> watchVariables = new Dictionary<string, KeyValuePair<object,string>>();
    public bool hello = false;

    public void Start()
    {

        //Assembly cecile = Assembly.Load(Application.dataPath + "/Mono.Cecil.dll");
        //Debug.Log(cecile);
        //AssemblyDefinition unity = null;

        



    }
    private void OnGUI()
    {
        if(!isShowing)return;
        windowRect = GUI.Window(0, windowRect, DrawConsoleWindow, "Console");

    }

    private void Update()
    {
        if (Input.GetKeyDown(openKey))
        {
            Debug.Log("TEST");
            isShowing = !isShowing;
        }

    }

    private void DrawConsoleWindow(int id)
    {
        GUILayout.BeginHorizontal();
        currentMode = (DebugMode) GUILayout.SelectionGrid((int) currentMode, Enum.GetNames(typeof(DebugMode)), 3);
        GUILayout.EndHorizontal();

        switch (currentMode)
        {
            case DebugMode.Console:
                DrawConsole();
                break;
            case DebugMode.Input:
                DrawInput();
                break;
            case DebugMode.Watch:
                DrawWatches();
                break;
        }



        GUI.DragWindow(dragRect);
    }

    private void DrawConsole()
    {

    }

    private void DrawInput()
    {

    }

    private void DrawWatches()
    {
        string s = "";
        //Horizontal lables
        //One verticle scroll value
        foreach (KeyValuePair<string, KeyValuePair<object, string>> v in watchVariables)
        {
            KeyValuePair<object, string> temp = v.Value;
            Debug.Log(temp);
            FieldInfo imfo = temp.Key.GetType().GetField(temp.Value);
            Debug.Log(imfo);
            s += v.Key + "\n" +  imfo.GetValue(temp.Key) + "\n";
        }
        GUILayout.TextArea(s);
    }

    private void GetKeyName()
    {
        var values = Enum.GetValues(typeof(KeyCode));

        foreach (KeyCode value in values)
        {
            if(Input.GetKeyDown(value))Debug.Log(value);
        }
    }

    public static void AddVarToWatch(string name, object container, string varName)
    {
        Instance.watchVariables.Add(name, new KeyValuePair<object, string>(container,varName));
    }
    
    public static void HELLLO()
    {
        Instance.hello = true;
    }

}

