using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Nodo : ScriptableObject {
    public Rect windowRect;
    public string windowTitle = "";
    private Nodo Ant;
    private Rect AntRect;
    private Efectos efecto;
    public enum Efectos
    {
        efecto1,
        efecto2,
        efecto3,
        
    }
    public Nodo()
    {
        windowTitle = "Nombre Nodo";       
    }


    public  void DrawWindow()
    {
        windowTitle = EditorGUILayout.TextField("Nombre", windowTitle);
        Event e = Event.current;
        GUILayout.Label("Anterior" );
        if (e.type == EventType.repaint)
        {
            AntRect = GUILayoutUtility.GetLastRect();
        }
        GUILayout.Label("Selecciona un efecto");

        efecto = (Efectos)EditorGUILayout.EnumPopup(efecto);
    }
    public  void setInput(Nodo input, Vector2 clickPos)
    {
        clickPos.x -= windowRect.x;
        clickPos.y -= windowRect.y;
        if (AntRect.Contains(clickPos))
        {
            Ant = input;
        }
       
    }
    public  void drawCurves()
    {
        if (Ant != null)
        {
            Rect rect = windowRect;
            rect.x += AntRect.x;
            rect.y += AntRect.y + AntRect.height / 2;
            rect.width = 1;
            rect.height = 1;
            NodeEditor.DrawNodeCurve(Ant.windowRect, rect);
        }
    }
    public  void getResult()
    {

        switch (efecto)
        {
            case Efectos.efecto1:
                //aplicar efecto1 a camara
                break;
            case Efectos.efecto2:
                //aplicar efecto2 a camara
                break;
            case Efectos.efecto3:
                //aplicar efecto3 a camara
                break;
        }
    }
    public  Nodo clickedOnInput(Vector2 pos)
    {
        Nodo retval = null;
        pos.x -= windowRect.x;
        pos.y -= windowRect.y;
        if (AntRect.Contains(pos))
        {
            retval = Ant;
            Ant = null;
        }       
        return retval;
    }

    public  void deletNode(Nodo node)
    {
        if (node.Equals(Ant) && Ant != null)
        {
            Ant = null;
        }
       
    }
}
