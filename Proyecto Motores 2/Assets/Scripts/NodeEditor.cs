using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodeEditor : EditorWindow
{
    ///> <
    private List<Nodo> windows = new List<Nodo>();
    private Nodo SelectedNode;
    private bool makeTransitionMode = false;
    private Vector2 mousePos;

    [MenuItem("Shot Editor/Editor de Nodos")]
    static void showEditor()
    {
        NodeEditor editor = EditorWindow.GetWindow<NodeEditor>();
    }
    void OnGUI()
    {
        Event e = Event.current;
        mousePos = e.mousePosition;
        if (e.button == 1 && !makeTransitionMode)
        {
            if (e.type == EventType.MouseDown)
            {
                bool clickedOnWindow = false;
                int selectedIndex = -1;
                for (int i = 0; i < windows.Count; i++)
                {
                    if (windows[i].windowRect.Contains(mousePos))
                    {
                        selectedIndex = i;
                        clickedOnWindow = true;
                        break;
                    }
                }
                if (!clickedOnWindow)
                {
                    GenericMenu menu = new GenericMenu();
                    menu.AddItem(new GUIContent("agregar Nodo"), false, contextCallback, "Nodo");
                    menu.ShowAsContext();
                    e.Use();
                }
                else
                {
                    GenericMenu menu = new GenericMenu();
                    menu.AddItem(new GUIContent("Likear Nodo"), false, contextCallback, "makeTransition");
                    menu.AddSeparator("");
                    menu.AddItem(new GUIContent("Eliminar Nodo"), false, contextCallback, "deleteNode");
                    menu.ShowAsContext();
                    e.Use();
                }
            }
        }
        else if (e.button == 0 && e.type == EventType.MouseDown && makeTransitionMode)
        {
            bool clickedOnWindow = false;
            int selectedIndex = -1;
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].windowRect.Contains(mousePos))
                {
                    selectedIndex = i;
                    clickedOnWindow = true;
                    break;
                }
            }
            if (clickedOnWindow && !windows[selectedIndex].Equals(SelectedNode))
            {
                windows[selectedIndex].setInput((Nodo)SelectedNode, mousePos);
                makeTransitionMode = false;
                SelectedNode = null;
            }
            if (!clickedOnWindow)
            {
                makeTransitionMode = false;
                SelectedNode = null;
            }
            e.Use();
        }
        else if (e.button == 0 && e.type == EventType.MouseDown && !makeTransitionMode)
        {
            bool clickedOnWindow = false;
            int selextIndex = -1;
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].windowRect.Contains(mousePos))
                {
                    selextIndex = i;
                    clickedOnWindow = true;
                    break;
                }
            }
            if (clickedOnWindow)
            {
                Nodo nodeToChange = windows[selextIndex].clickedOnInput(mousePos);
                if (nodeToChange != null)
                {
                    SelectedNode = nodeToChange;
                    makeTransitionMode = true;
                }
;
            }
        }
        if (makeTransitionMode && SelectedNode != null)
        {
            Rect mouserect = new Rect(e.mousePosition.x, e.mousePosition.y, 10, 10);
            DrawNodeCurve(SelectedNode.windowRect, mouserect);
            Repaint();
        }
        foreach (Nodo nodo in windows)
        {
            nodo.drawCurves();
        }
        BeginWindows();
        for (int i = 0; i < windows.Count; i++)
        {
            windows[i].windowRect = GUI.Window(i, windows[i].windowRect, drawNodeWindow, windows[i].windowTitle);
        }
        EndWindows();

    }
    void contextCallback(object objeto)
    {
        string callbk = objeto.ToString();
        if (callbk.Equals("Nodo"))
        {
            Nodo Inodo = new Nodo();
            Inodo.windowRect = new Rect(mousePos.x, mousePos.y, 200, 150);
            windows.Add(Inodo);
        }       
       
        else if (callbk.Equals("makeTransition"))
        {
            bool clickedOnWindow = false;
            int selextIndex = -1;
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].windowRect.Contains(mousePos))
                {
                    selextIndex = i;
                    clickedOnWindow = true;
                    break;
                }
            }
            if (clickedOnWindow)
            {
                SelectedNode = windows[selextIndex];
                makeTransitionMode = true;
            }
        }
        else if (callbk.Equals("deleteNode"))
        {
            bool clickedOnWindow = false;
            int selextIndex = -1;
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].windowRect.Contains(mousePos))
                {
                    selextIndex = i;
                    clickedOnWindow = true;
                    break;
                }
            }
            if (clickedOnWindow)
            {
                Nodo selNode = windows[selextIndex];
                windows.RemoveAt(selextIndex);
                foreach (Nodo nodo in windows)
                {
                    if (SelectedNode != null)
                    {
                        nodo.deletNode(SelectedNode);
                    }
                }
            }
        }


    }
    void drawNodeWindow(int id)
    {
        windows[id].DrawWindow();
        GUI.DragWindow();
    }
    public static void DrawNodeCurve(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width / 2, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x + end.width / 2, end.y + end.height / 2, 0);
        Vector3 starTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;

        for (int i = 0; i < 3; i++)
        {
            Handles.DrawBezier(startPos, endPos, starTan, endTan, Color.black, null, (i + 1) * 5);
        }
        Handles.DrawBezier(startPos, endPos, starTan, endTan, Color.black, null, 1);
    }

}

