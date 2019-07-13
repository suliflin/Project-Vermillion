using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BehaviorTreeEditor : EditorWindow
{
    #region Variables
    static List<BaseNode> windows = new List<BaseNode>();

    BaseNode root;
    BaseNode current;
    BaseNode selectedNode;

    Vector3 mousePos;

    bool makeArrow;
    bool clickedOnWindow;

    int selectedIndex;

    public enum UserActions
    {
        ADDSELECTORNODE,
        ADDSEQUENCENODE,
        ADDACTIONNODE,
        ADDTRANSITION,
        DELETENODE
    }
    #endregion

    #region Initialize
    [MenuItem("Behavior Tree Editor/Editor")]
    static void ShowEditor()
    {
        BehaviorTreeEditor editor = EditorWindow.GetWindow<BehaviorTreeEditor>();
        editor.minSize = new Vector2(800, 600);
    }

    private void OnEnable()
    {

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

    void DrawWindows()
    {
        BeginWindows();

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
        if (e.button == 1)
        {
            if (e.type == EventType.MouseDown)
            {
                RightClick(e);
            }
        }

        if (e.button == 0 )
        {
            if (e.type == EventType.MouseDown)
            {

            }
        }
    }

    void RightClick(Event e)
    {
        selectedIndex = -1;
        clickedOnWindow = false;
        for (int i = 0; i < windows.Count; i++)
        {
            if (windows[i].windowRect.Contains(e.mousePosition))
            {
                clickedOnWindow = true;
                selectedNode = windows[i];
                selectedIndex = i;
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

        menu.AddItem(new GUIContent("Add Selector"), false, ContextCallback, UserActions.ADDSELECTORNODE);
        menu.AddItem(new GUIContent("Add Sequence"), false, ContextCallback, UserActions.ADDSEQUENCENODE);

        menu.ShowAsContext();
        e.Use();
    }

    void ModifyNode(Event e)
    {

    }

    void ContextCallback(object o)
    {
        UserActions a = (UserActions)o;

        switch (a)
        {
            case UserActions.ADDSELECTORNODE:

                AddSelectorNode(mousePos);

                break;

            case UserActions.ADDTRANSITION:

                break;

            case UserActions.DELETENODE:

                break;

            default:
                break;
        }
    }
    #endregion

    #region Helper Methods
    public static Selector AddSelectorNode(Vector2 pos)
    {
        Selector selector = CreateInstance<Selector>();
        selector.windowRect = new Rect(pos.x, pos.y, 200, 300);
        selector.windowTitle = "Selector";
        windows.Add(selector);

        return selector;
    }
    #endregion
}