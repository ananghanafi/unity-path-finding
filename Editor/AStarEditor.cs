using UnityEngine;
// Import editor package
using UnityEditor;
using System.Collections;

// Editor window
public class AStarEditor : EditorWindow
{
    // String for console
    private string console;
    private Vector2 scrollTextArea;
    // Adds the Minerva Item to the Unity tool bar
    [MenuItem("Minerva/AStar Editor")]
    // Shows window
    static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof (AStarEditor));
    }

    // "Draw" method of the gui
    void OnGUI()
    {
        // The actual window goes here
        GUILayout.Label("A* Implementation");   // Text
        bool addNodeClick = GUILayout.Button("Add node");         // Button
        if(addNodeClick)
        {
		    AddToConsole("Node added");
        }
        // Draw console
        //scrollTextArea = EditorGUILayout.BeginScrollView(scrollTextArea);
        EditorGUILayout.TextArea(console, GUILayout.Height(100));
        //EditorGUILayout.EndScrollView();
    }

    void AddToConsole(string str)
    {
        console += str + "\n";
    }
}