using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SA;

namespace SA.BehaviorEditor
{
    public class StateNode : BaseNode
    {
        public State currentState;
        public State previousState;

        List<BaseNode> dependecies = new List<BaseNode>();

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
                    windowRect.height = 300;
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
                previousState = currentState;
                ClearReferences();
                for (int i = 0; i < currentState.transitions.Count; i++)
                {
                    dependecies.Add(BehaviorEditor.AddTransitionNode(i, currentState.transitions[i], this));
                }
            }

            if (currentState != null)
            {

            }
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