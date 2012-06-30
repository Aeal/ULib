using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Object = UnityEngine.Object;


public class NodeEditorWindow : EditorWindow
{
    
    private const int VERSION_NUMBER = 1,
                      maxWidth = 500,
                      RemoveButtonSize = 20;

    private static NodeEditorWindow instance;

    private static List<NodeInspectorBase> Nodes;

    private Vector2 scrollPos = Vector2.zero,
                    deltaScroll = Vector2.zero;

    private GUILayoutOption[] WindowOptions = new[] { GUILayout.ExpandWidth(false),  GUILayout.MaxWidth(GUIOption.MaxNodeWidth),   GUILayout.MinWidth(GUIOption.MinNodeWidth),
                                                      GUILayout.ExpandHeight(true), GUILayout.MaxHeight(GUIOption.MaxNodeHeight), GUILayout.MinHeight(GUIOption.MinNodeHeight)};
    private string currentFile = "";

    private Type[]   nodeTypes;
    private string[] possibleNodeTypes;
    private int      nodeIndexToAdd;

    private float zoomAmount = 0,
                  zoomSpeed = .1f;

    private Vector3 scaleMatrix = Vector3.one, 
                    transformMatrix;



    [MenuItem("Window/Node Editor")]
    public static void ShowWindow()
    {
        if (instance == null)
            instance = CreateInstance<NodeEditorWindow>();
        instance.Show();
        if(Nodes == null)
            Nodes = new List<NodeInspectorBase>();

    }


    
   
    public void GenerateActionTypes()
    {
        possibleNodeTypes = Utility.GetSubClassesAsString(typeof(ActionNode));
        nodeTypes = Utility.GetSubClassesAsType(typeof(ActionNode));
    }


    private Rect ScaleRect(float amount, Rect rect)
    {
        rect.width *= amount;
        rect.height *= amount;
        return rect;
    }

    private Rect NudgeRect(float x, float y, Rect rect)
    {
        rect.x += deltaScroll.x;
        rect.y += deltaScroll.y;
        return rect;
    }

    public void OnGUI()
    {
        DrawDialogControls();
        if (Nodes == null) return;
        var prev = scrollPos;
        
        deltaScroll = scrollPos - prev;
        Matrix4x4 guiM = GUI.matrix;
        
        if(Event.current.type == EventType.ScrollWheel)
        {
            scaleMatrix.x = Mathf.Clamp(scaleMatrix.x + Event.current.delta.y * zoomSpeed, 0.1f, 2.0f);
            scaleMatrix.y = Mathf.Clamp(scaleMatrix.y + Event.current.delta.y * zoomSpeed, 0.1f, 2.0f);
            
        }
            scaleMatrix.z = 1;
        
        GUI.matrix = Matrix4x4.TRS(new Vector3(scrollPos.x, scrollPos.y,0), Quaternion.AngleAxis(0, new Vector3(0, 1, 0)), scaleMatrix);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos,true,true);
        BeginWindows();
        for (int i = 0; i < Nodes.Count; i++)
        {
            NodeInspectorBase dialog = Nodes[i];
            float orginalHeight, 
                  orginalWidth;
            
            Rect adjusted = dialog.CurrentPoistion;
            
            orginalHeight = adjusted.height;
            orginalWidth  = adjusted.width;

           adjusted = NudgeRect(scrollPos.x,scrollPos.y,adjusted);
           //adjusted = ScaleRect(zoomAmount, adjusted);

            adjusted = GUILayout.Window(i, adjusted, dialog.OnNodeGUI, dialog.NodeName, WindowOptions);

            adjusted = NudgeRect(-scrollPos.x, -scrollPos.y, adjusted);
            //adjusted.width = orginalWidth;
            //adjusted.height = orginalHeight;

            dialog.CurrentPoistion = adjusted;

        } 
        //Get the highest and 
        EndWindows();
       
        EditorGUILayout.EndScrollView();
        GUI.matrix = guiM;
               
    }

    //void DrawActionControls(ref DialogNode item)
    //{
    //    item.skippable = EditorGUILayout.Toggle("Skipabble: ", item.skippable);
    //    actionToAdd = EditorGUILayout.Popup(actionToAdd, possibleActions);
    //    EditorGUILayout.BeginHorizontal();
    //    if(GUILayout.Button("Add Focused Action"))
    //    {
    //        ActionNode action = (ActionNode)actionTypes[actionToAdd].GetConstructor(Type.EmptyTypes).Invoke(null);
    //        item.onNodeFocusedActions.Add(action);
    //    }
    //    if(GUILayout.Button("Add Leave Action"))
    //    {
    //        ActionNode action = (ActionNode)actionTypes[actionToAdd].GetConstructor(Type.EmptyTypes).Invoke(null);
    //        item.onNodeLeaveActions.Add(action);
    //    }
    //    EditorGUILayout.EndHorizontal();

    //    DrawNodeActionControls(item.onNodeFocusedActions, "On Node Entered Actions");
    //    DrawNodeActionControls(item.onNodeLeaveActions,   "On Node Exit Actions");

    //  }

    //void DrawNodeActionControls(List<ActionNode> nodeActions, string Title )
    //{
    //    EditorGUILayout.BeginVertical("Box");
    //    EditorGUILayout.LabelField(Title);
    //    for (int index = 0; index < nodeActions.Count; index++)
    //    {
    //        ActionNode VARIABLE = nodeActions[index];
    //        EditorGUILayout.BeginVertical("box");
    //        EditorGUILayout.BeginHorizontal();

    //        if (GUILayout.Button("X", ButtonOptions))
    //        {
    //            VARIABLE.OnEditorDestroy();
    //            nodeActions.Remove(VARIABLE);
    //            index--;
    //        }

    //        VARIABLE.IsShowing = EditorGUILayout.Foldout(VARIABLE.IsShowing, VARIABLE.GetType().ToString());

    //        if (VARIABLE.IsShowing)
    //        {
    //            VARIABLE.OnInspectorGUI();                
    //        }
    //        EditorGUILayout.EndHorizontal();
    //        EditorGUILayout.EndVertical();
    //    }
    //    EditorGUILayout.EndVertical();
    //}

    //void DrawConversationOptions(DialogNode dialog)
    //{
    //    EditorStyles.textField.wordWrap = true;

    //    EditorGUILayout.BeginHorizontal();
    //    EditorGUILayout.PrefixLabel("Speaker");
    //    dialog.speaker = (Speakers)EditorGUILayout.EnumPopup(dialog.speaker);
    //    EditorGUILayout.EndHorizontal();

    //    EditorGUILayout.BeginHorizontal();
    //    EditorGUILayout.PrefixLabel("Audio Clip");
    //    dialog.spoken = (AudioClip)EditorGUILayout.ObjectField(dialog.spoken, typeof(AudioClip), false);
    //    EditorGUILayout.EndHorizontal();

    //    EditorGUILayout.BeginHorizontal();
    //    EditorGUILayout.PrefixLabel("Subtitle");
    //    dialog.subtitle = EditorGUILayout.TextArea(dialog.subtitle, GUILayoutOptions);
    //    EditorGUILayout.EndHorizontal();

    //    EditorGUILayout.BeginHorizontal();
    //    EditorGUILayout.PrefixLabel("Wait type:");
    //    dialog.wait = (Waits)EditorGUILayout.EnumPopup(dialog.wait);
    //    EditorGUILayout.EndHorizontal();
    //    if (dialog.wait == Waits.Time)
    //    {
    //        dialog.waitTime = EditorGUILayout.FloatField("Wait time: ", dialog.waitTime);
    //    }
    //}

    //void DrawDialogOptionControls(DialogNode dialog, int i)
    //{
    //    if (GUILayout.Button("up"))
    //    {
    //        if (i > 0)
    //        {
    //            DialogNodes.Insert(i - 1, dialog);
    //            DialogNodes.RemoveAt(i + 1);
    //        }
    //    }
    //    if (GUILayout.Button("down"))
    //    {
    //        if (i + 1 < DialogNodes.Count)
    //        {
    //            DialogNodes.Insert(i + 2, dialog);
    //            DialogNodes.RemoveAt(i);
    //        }
    //    }
    //    if (GUILayout.Button("delete"))
    //    {
    //        DialogNodes.RemoveAt(i);
    //    }
    //}

    void DrawDialogControls()
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Current File loaded: " + currentFile);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            //SaveConvo(currentFile);
        }
        if (GUILayout.Button("Save As"))
        {
            //SaveConvo();
        }
        if (GUILayout.Button("Load"))
        {
            //LoadConvo();
        }
        if (GUILayout.Button("New"))
        {
            if (EditorUtility.DisplayDialog("Are you sure?", "You are about to loose all unsaved work. Are you sure?", "Yes", "No"))
            {
                Nodes = new List<NodeInspectorBase>();
                currentFile = "";
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add dialog element"))
        {
            if (Nodes == null)
            {
                Nodes = new List<NodeInspectorBase>();
            }
           
            Nodes.Add(new NodeInspector<DialogNode>());
        }
      
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.EndVertical();
    }

    //void DrawBranch(DialogNode dialog, int i)
    //{
    //    if (dialog.choice != null)
    //    {
    //        for (int j = 0; j < dialog.choice.Count; j++)
    //        {
    //            EditorGUILayout.BeginHorizontal();
    //            if (GUILayout.Button("X", ButtonOptions))
    //            {
    //                DialogNodes[i].choice.Remove(dialog.choice[j]);
    //                j--;
    //                continue;
    //            }
    //            dialog.choice[j] = EditorGUILayout.TextArea(dialog.choice[j], GUILayoutOptions);
    //            String[] option_names = new String[DialogNodes.Count + 1];
    //            int[] option_numbers = new int[DialogNodes.Count + 1];

    //            for (int k = 0; k < DialogNodes.Count; k++)
    //            {
    //                if (k < i)
    //                {
    //                    option_names[k] =
    //                        DialogNodes[k].subtitle.Substring(0, Mathf.Min(40, DialogNodes[k].subtitle.Length)) + "..";
    //                    option_numbers[k] = k;
    //                }
    //                else if (k > i)
    //                {
    //                    option_names[k - 1] =
    //                        DialogNodes[k].subtitle.Substring(0, Mathf.Min(40, DialogNodes[k].subtitle.Length)) + "..";
    //                    option_numbers[k - 1] = k;
    //                }
    //            }
    //            dialog.jump[j] = EditorGUILayout.IntPopup(dialog.jump[j], option_names, option_numbers);
    //            EditorGUILayout.EndHorizontal();
    //        }
    //    }
    //    if (GUILayout.Button("Add choice"))
    //    {
    //        if (dialog.choice == null)
    //        {
    //            dialog.choice = new List<String>();
    //            dialog.jump = new List<int>();
    //        }
    //        dialog.choice.Add("(enter text to display)");
    //        dialog.jump.Add(-1);
    //    }
    //}

    //void SaveConvo(string path = "")
    //{
    //    string file = path;
    //    if (file == "" || file.EndsWith("txt"))
    //        file = EditorUtility.SaveFilePanel("Save Convo", Application.dataPath + "/Resources", "NewFile", "xml");
    //    if (file == "") return;
        

    //    Debug.Log("Saving file: " + file);
    //    foreach (DialogNode DialogNode in Nodes)
    //    {
    //        DialogNode.BuildForSerialize();
    //    }
    //   using(FileStream fs = new FileStream(file,FileMode.Create))
    //   {
    //       XmlSerializer serializer = new XmlSerializer(typeof(ConversationPackage), actionTypes.ToArray());
    //       serializer.Serialize(fs,new ConversationPackage(Nodes,VERSION_NUMBER ));
    //   }
    //}

    //void LoadConvo(string file = "") 
    //{
    //    if(file == "")
    //    {
    //        file = EditorUtility.OpenFilePanel("Load Convo", Application.dataPath + "/Resources", "xml");
    //    }
    //    if(file == "")return;
    //    currentFile = file;
    //    Debug.Log("Loading File: " + currentFile);

    //    int versionNumber = 0; 
        
    //        try
    //        {
    //            using (FileStream fs = new FileStream(file, FileMode.Open))
    //            {
    //                XmlSerializer serializer = new XmlSerializer(typeof (ConversationPackage), actionTypes.ToArray());
    //                ConversationPackage package = serializer.Deserialize(fs) as ConversationPackage;
    //                Nodes = package.items;
    //                versionNumber = package.versionNumber;
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            try
    //            {
    //                using (FileStream fs = new FileStream(file, FileMode.Open))
    //                {
    //                    Debug.LogError("Error loading package, Trying legacy loader");
    //                    XmlSerializer serializer = new XmlSerializer(typeof (List<DialogNode>), actionTypes.ToArray());
    //                    Nodes = serializer.Deserialize(fs) as List<DialogNode>;
    //                    if (Nodes == null)
    //                    {
    //                        Debug.LogError("Error in the legacy loader. Check for valid XML");
    //                    }
    //                }
    //            }
    //            catch (Exception e)
    //            {
    //                Debug.LogError("Error in the legacy loader. " + e.Message);
    //            }
                

    //        }
            
    //    foreach (DialogNode DialogNode in Nodes)
    //    {
    //        DialogNode.Initialize();
    //    }
    //    //Do specific version loading here

        
       
    //}

    //void OnEnable()
    //{
    //    GenerateActionTypes();
        
    //}

    
}

