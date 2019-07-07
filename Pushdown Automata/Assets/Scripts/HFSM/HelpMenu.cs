using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : Screen
{
    public Screen gameMenu;
    public Screen mainMenu;

    // Update is called once per frame
    public override void Execute(ScreenManager sm)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sm.SetState(mainMenu);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sm.SetState(gameMenu);
        }
        base.Execute(sm);
    }
}
