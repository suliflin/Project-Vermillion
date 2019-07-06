﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.IO;
using SA;

namespace SA.BehaviorEditor
{
    public class StateNode : BaseNode
    {
        public State currentState;
        public State previousState;

        public List<BaseNode> dependecies = new List<BaseNode>();

        SerializedObject serializedState;

        ReorderableList onStateList;
        ReorderableList onEnterList;
        ReorderableList onExitList;

        bool collapse;

        public override void DrawWindow()
        {
            if (currentState == null)
            {
                EditorGUILayout.LabelField("Add state to modify:");
            }
            else
            {
                if (!collapse)
                {
                    
                }
                else
                {
                    windowRect.height = 100;
                }
                collapse = EditorGUILayout.Toggle(" ", collapse);
            }
            currentState = (State)EditorGUILayout.ObjectField(currentState, typeof(State), false);

            if (previousState != currentState)
            {
                serializedState = null;
                previousState = currentState;
                ClearReferences();
                for (int i = 0; i < currentState.transitions.Count; i++)
                {
                    dependecies.Add(BehaviorEditor.AddTransitionNode(i, currentState.transitions[i], this));
                }
            }

            if (currentState != null)
            {
                if (serializedState == null)
                {
                    serializedState = new SerializedObject(currentState);
                    onStateList = new ReorderableList(serializedState, serializedState.FindProperty("onState"), true, true, true, true);
                    onEnterList = new ReorderableList(serializedState, serializedState.FindProperty("onEnter"), true, true, true, true);
                    onExitList = new ReorderableList(serializedState, serializedState.FindProperty("onExit"), true, true, true, true);
                }

                if (!collapse)
                {
                    serializedState.Update();
                    HandleReordableList(onStateList, "On State");
                    HandleReordableList(onEnterList, "On Enter");
                    HandleReordableList(onExitList, "On Exit");

                    EditorGUILayout.LabelField("");
                    onStateList.DoLayoutList();
                    EditorGUILayout.LabelField("");
                    onEnterList.DoLayoutList();
                    EditorGUILayout.LabelField("");
                    onExitList.DoLayoutList();

                    serializedState.ApplyModifiedProperties();

                    float standard = 300;
                    standard += (onStateList.count) * 20;
                    windowRect.height = standard;
                }
            }
        }

        void HandleReordableList(ReorderableList list, string target)
        {
            list.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, target);
            };

            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
            };
        }

        public override void DrawCurve()
        {

        }

        public Transition AddTransition()
        {
            return currentState.AddTransition();
        }

        public void ClearReferences()
        {
            BehaviorEditor.ClearWindowsFromList(dependecies);
            dependecies.Clear();
        }
    }
}