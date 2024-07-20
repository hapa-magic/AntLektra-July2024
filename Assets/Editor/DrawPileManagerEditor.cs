using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HapaMagic;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(DrawPileManager))]
public class DrawPileManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DrawPileManager drawPileManager = (DrawPileManager)target;
        if (GUILayout.Button("Draw Next Card")){
            drawPileManager = FindObjectOfType<DrawPileManager>();
            if (drawPileManager != null){
                drawPileManager.DrawCard(drawPileManager.GetHandManager());
            }
        }
    }
}
#endif
