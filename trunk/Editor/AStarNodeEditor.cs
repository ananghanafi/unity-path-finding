using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AStarNode))]
public class AStarNodeEditor : Editor 
{
    bool selectionModeActive = false;
    private int curSel = 0;
    private GameObject node;
    private Vector2 scrollPosition;

    public override void OnInspectorGUI()
    {
        GUILayout.Label("All you need is the scene view");
    }

    void OnSceneGUI()
    {
        var self = (AStarNode)target;
        #region GUI
        Handles.BeginGUI();
        {
            GUILayout.BeginArea(new Rect(5, 5, 350, 250));
            {
                GUILayout.Box("Options", GUILayout.Width(350), GUILayout.Height(250));
                GUILayout.Space(-220);
                var cnt = self.connections.Count;
                GUILayout.BeginHorizontal();
                {
                    cnt = EditorGUILayout.IntField("Size", cnt, GUILayout.Width(200));
                    if (GUILayout.Button("Add"))
                        cnt++;
                }
                GUILayout.EndHorizontal();

                if (cnt < 0) cnt = 0;
                if (cnt > 20) cnt = 20;
                if (cnt > self.connections.Count)
                    while (self.connections.Count < cnt)
                        self.connections.Add(null);
                else
                    while (self.connections.Count > cnt)
                        self.connections.Remove(self.connections[self.connections.Count - 1]);
                scrollPosition = GUILayout.BeginScrollView(scrollPosition);
                {
                    for (var i = 0; i < self.connections.Count; i++)
                    {
                        GUILayout.BeginHorizontal(GUILayout.Width(295));
                        {
                            if (GUILayout.Button("Select"))
                            {
                                if (self.connections[i] != null)
                                    Selection.activeGameObject = self.connections[i].gameObject;
                                else
                                {
                                    selectionModeActive = true;
                                    curSel = i;
                                }
                            }
                            self.connections[i] = EditorGUILayout.ObjectField(self.connections[i], typeof(AStarNode)) as AStarNode;
                        }
                        GUILayout.EndHorizontal();
                    }
                }
                GUILayout.EndScrollView();
            }
            GUILayout.EndArea();
        }
        Handles.EndGUI();
        #endregion

        #region Selecting
        if (selectionModeActive)
        {
            var controlID = GUIUtility.GetControlID(FocusType.Passive);
            var cur = Event.current;
            switch (cur.type)
            {
                case EventType.MouseDown:
                    Ray r = HandleUtility.GUIPointToWorldRay(cur.mousePosition);
                    RaycastHit h;
                    if (Physics.Raycast(r, out h))
                    {
                        Undo.RegisterUndo(self, "Add Connection");
                        var n = h.transform.GetComponent<AStarNode>();
                        if (n)
                        {
                            self.connections[curSel] = n;
                            if (!n.connections.Contains(self))
                            {
                                n.connections.Add(self);
                            }
                            selectionModeActive = false;
                        }
                    }
                    cur.Use();
                    break;
                case EventType.layout:
                    HandleUtility.AddDefaultControl(controlID);
                    break;
            }
        }
        #endregion

        #region Pathing
        foreach(AStarNode aStarNode in FindSceneObjectsOfType(typeof(AStarNode)))
        {
            for(int i = 0; i < aStarNode.connections.Count; i++)
            {
                Handles.color = Color.red; // not a connection
                if(aStarNode.connections[i] != null)
                    Handles.DrawPolyLine(aStarNode.transform.position, aStarNode.connections[i].transform.position);
                Handles.color = Color.white;
            }
        }

        foreach(var t in self.connections)
        {
            Handles.color = Color.cyan;
            if(t != null)
            {
                Handles.DrawPolyLine(self.transform.position, t.transform.position);
            }
            Handles.color = Color.white;
        }
        #endregion

        if(GUI.changed)
            EditorUtility.SetDirty(self);
    }
}
