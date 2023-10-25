using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomBezier))]
public class CustomBezierEditor : Editor
{
    private CustomBezier ctr;
    private void OnSceneGUI()
    {
        if (ctr.poses.Length >= 4)
        {
            Draw();
        }
        
    }

    private void Awake()
    {
        ctr = target as CustomBezier;
    }

    void Draw()
    {
        //Draw a sphere
        Color originColor = Handles.color;

        Vector2 targetPos;
        targetPos = ctr.transform.position;
        for (int i = 0; i < ctr.poses.Length; i++)
        {
            var pos = ctr.poses[i];
            //Draw Circle
            Handles.color = Color.red;
            Handles.SphereHandleCap(GUIUtility.GetControlID(FocusType.Passive), targetPos + pos, Quaternion.identity, 0.2f, EventType.Repaint);
        }

        Vector2 startTangent;
        Vector2 endTangent;

        //Draw Cruve
        startTangent = ctr.poses[1] - ctr.poses[0];
        endTangent = ctr.poses[2] - ctr.poses[3];

        
        Handles.DrawBezier(targetPos +ctr.poses[0], targetPos+ctr.poses[3], targetPos+ ctr.poses[1], targetPos + ctr.poses[2], Color.red,null, 1);

        Handles.color = originColor;
    }
}