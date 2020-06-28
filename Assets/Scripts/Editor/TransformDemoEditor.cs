using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Tutorial;

namespace Tutorial.Editing
{
    [CustomEditor(typeof(TransformDemo))]
    public class TransformDemoEditor : Editor
    {
        Transform transform;
        TransformDemo demoObject;


        private void OnEnable() 
        {
            demoObject = target as TransformDemo;
            transform = demoObject.transform;
        }

        private void OnSceneGUI() 
        {
            Vector3 result = Handles.PositionHandle(transform.position + demoObject.Forward, Quaternion.identity) - transform.position;
            //Handles.color = Color.green;
            //Handles.DrawWireCube(transform.position, Vector3.one);
            demoObject.Forward = result.normalized;
        }

    }
}