using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.IO;


public class Selector : BaseNode
{
    public override void DrawWindow()
    {
        EditorGUILayout.LabelField("Add state to modify:");

    }

    public override NodeStates Evaluate(BehaviorTreeEditor bt)
    {
        for (int i = 0; i < treeNodes.Count; i++)
        {
            treeNodes[i].Evaluate(bt);

            if (treeNodes[i].current == NodeStates.RUNNING)
            {
                current = NodeStates.RUNNING;
                return current;
            }

            if (treeNodes[i].current == NodeStates.SUCCESS)
            {
                current = NodeStates.SUCCESS;
                return current;
            }
        }
        current = NodeStates.FAILURE;
        return current;
    }
}
