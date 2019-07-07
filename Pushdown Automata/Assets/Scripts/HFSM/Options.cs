using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : Screen
{
    public Screen mainMenu;
    public Screen helpMenu;

    // Update is called once per frame
    public override void Execute(ScreenManager sm)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sm.SetState(mainMenu);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            sm.SetState(helpMenu);
        }
        base.Execute(sm);
    }
}
