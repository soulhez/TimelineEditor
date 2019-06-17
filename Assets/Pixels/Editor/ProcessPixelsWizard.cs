/*
	Author:			小何
	CreateDate:		2019-06-14 11:06:52
	Desc:			MonoBehaviour脚本类.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ProcessPixelsEditor
{
	public class ProcessPixelsWizard : EditorWindow
    {
        const string k_Tab = "    ";
        const string k_ShowHelpBoxesKey = "Show Pixels Editor Wizard";

        const float k_WindowWidth = 800f;
        const float k_MaxWindowHeight = 500f;
        const float k_ScreenSizeWindowBuffer = 50f;

        public bool showHelpBoxes = true;

        string myString = "Hello World";
        bool groupEnabled;
        bool myBool = true;
        float myFloat = 1.23f;

        [MenuItem("Window/Pixels Editor")]
        private static void CreateWindow()
        {
            ProcessPixelsWizard wizard = GetWindow<ProcessPixelsWizard>(true, "Pixels Editor", true);
            Vector2 position = Vector2.zero;
            SceneView sceneView = SceneView.lastActiveSceneView;
            if(sceneView != null)
            {
                position = new Vector2(sceneView.position.x, sceneView.position.y);
            }
            wizard.position = new Rect(position.x + k_ScreenSizeWindowBuffer, position.y + k_ScreenSizeWindowBuffer, k_WindowWidth, Mathf.Min(Screen.currentResolution.height - k_ScreenSizeWindowBuffer, k_MaxWindowHeight));

            wizard.showHelpBoxes = EditorPrefs.GetBool(k_ShowHelpBoxesKey);
            wizard.Show();

            Init();
        }

        private static void Init()
        {
            Debug.Log(string.Concat(Time.time, " ", Type.FilterName, " : ", "Init()"));
        }

        private void OnGUI()
        {
            GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            myString = EditorGUILayout.TextField("Text Field", myString);

            groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
            myBool = EditorGUILayout.Toggle("Toggle", myBool);
            myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
            EditorGUILayout.EndToggleGroup();
        }
    }
}