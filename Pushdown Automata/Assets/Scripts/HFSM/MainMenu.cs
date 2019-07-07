using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Screen
{
    public Screen gameMenu;
    public Screen opMenu;
    public Screen helpMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Execute(ScreenManager sm)
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            sm.SetState(gameMenu);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            sm.SetState(opMenu);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            sm.SetState(helpMenu);
        }
        base.Execute(sm);
    }
}
