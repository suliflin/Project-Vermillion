using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SA.BehaviorEditor
{
    public class BehaviorEditor : EditorWindow
    {
        #region Variables
        static List<BaseNode> windows = new List<BaseNode>();

        Vector3 mousePos;

        bool makeTransition;
        bool clickedOnWindow;

        BaseNode selectedNode;

        public enum UserActions
        {
            ADDSTATE,
            ADDTRANSTITIONNODE,
            DELETENODE,
            COMMENTNODE
        }
        #endregion

        #region Init
        [MenuItem("Behavior Editor/Editor")]
        static void ShowEditor()
        {
            BehaviorEditor editor = EditorWindow.GetWindow<BehaviorEditor>();
            editor.minSize = new Vector2(800, 600);
        }
        #endregion

        #region GUI Methods
        private void OnGUI()
        {
            Event e = Event.current;
            mousePos = e.mousePosition;
            UserInput(e);
            DrawWindows();
        }

        private void OnEnable()
        {
            
        }

        void DrawWindows()
        {
            BeginWindows();
            foreach (BaseNode n in windows)
            {
                n.DrawCurve();
            }

            for (int i = 0; i < windows.Count; i++)
            {
                windows[i].windowRect = GUI.Window(i, windows[i].windowRect, DrawNodeWindow, windows[i].windowTitle);
            }
            EndWindows();
        }

        void DrawNodeWindow(int id)
        {
            windows[id].DrawWindow();
            GUI.DragWindow();
        }

        void UserInput(Event e)
        {
            if (e.button == 1 && !makeTransition)
            {
                if (e.type == EventType.MouseDown)
                {
                    RightClick(e);
                }
            }

            if (e.button == 0 && !makeTransition)
            {
                if (e.type == EventType.MouseDown)
                {

                }
            }
        }
        
        void RightClick(Event e)
        {
            selectedNode = null;
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].windowRect.Contains(e.mousePosition))
                {
                    clickedOnWindow = true;
                    selectedNode = windows[i];
                    break;
                }
            }

            if (!clickedOnWindow)
            {
                AddNewNode(e);
            }
            else
            {
                ModifyNode(e);
            }
        }

        void AddNewNode(Event e)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Add State"), false, ContextCallback, UserActions.ADDSTATE);
            menu.AddItem(new GUIContent("Add Comment"), false, ContextCallback, UserActions.COMMENTNODE);
            menu.ShowAsContext();
            e.Use();
        }

        void ModifyNode(Event e)
        {
            GenericMenu menu = new GenericMenu();

            if (selectedNode is StateNode)
            {
                StateNode stateNode = (StateNode)selectedNode;
                if(stateNode.currentState != null)
                {
                    menu.AddSeparator("");
                    menu.AddItem(new GUIContent("Add Transition"), false, ContextCallback, UserActions.ADDTRANSTITIONNODE);
                }
                else
                {
                    menu.AddSeparator("");
                    menu.AddDisabledItem(new GUIContent("Add Transition"));
                }
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.DELETENODE);
            }

            if (selectedNode is CommentNode)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.DELETENODE);
            }

            menu.ShowAsContext();
            e.Use();
        }

        void ContextCallback(object o)
        {
            UserActions a = (UserActions)o;
            switch (a)
            {
                case UserActions.ADDSTATE:

                    StateNode stateNode = new StateNode();
                    stateNode.windowRect = new Rect(mousePos.x, mousePos.y, 200, 300);
                    stateNode.windowTitle = "State";
                    windows.Add(stateNode);

                    break;
                case UserActions.ADDTRANSTITIONNODE:

                    if(selectedNode is StateNode)
                    {
                        StateNode from = (StateNode)selectedNode;
                        Transition transition = from.AddTransition();
                        AddTransitionNode(from.currentState.transitions.Count, transition, from);
                    }

                    break;
                case UserActions.DELETENODE:

                    if (selectedNode != null)
                    {
                        windows.Remove(selectedNode);
                    }

                    break;
                case UserActions.COMMENTNODE:

                    CommentNode commentNode = new CommentNode();
                    commentNode.windowRect = new Rect(mousePos.x, mousePos.y, 200, 100);
                    commentNode.windowTitle = "Comment";
                    windows.Add(commentNode);

                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Helper Methods
        public static TransitionNode AddTransitionNode(int index, Transition transition, StateNode from)
        {
            Rect fromRect = from.windowRect;
            fromRect.x += 50;
            float targetY = fromRect.y - fromRect.height;

            if (from.currentState != null)
            {
                targetY += (index * 100);
            }
            fromRect.y = targetY;

            TransitionNode transNode = CreateInstance<TransitionNode>();
            transNode.Init(from, transition);
            transNode.windowRect = new Rect(fromRect.x + 300, fromRect.y + (fromRect.height * 0.7f), 200, 80);
            transNode.windowTitle = "Condition Check";
            windows.Add(transNode);

            return transNode;
        }

        public static void DrawNodeCurve(Rect start, Rect end, bool left, Color curveColor)
        {
            Vector3 startPos = new Vector3((left) ? start.x + start.width : start.x, start.y + (start.height * 0.5f), 0);
            Vector3 endPos = new Vector3(end.x + (end.width * 0.5f), end.y + (end.height * 0.5f), 0);
            Vector3 startTan = startPos + Vector3.right * 50;
            Vector3 EndTan = endPos + Vector3.left * 50;

            Color shadow = new Color(0, 0, 0, 0.05f);
            for (int i = 0; i < 3; i++)
            {
                Handles.DrawBezier(startPos, endPos, startTan, EndTan, shadow, null, (i + 1) * 0.5f);
            }

            Handles.DrawBezier(startPos, endPos, startTan, EndTan, curveColor, null, 1);
        }
        #endregion
    }
}

