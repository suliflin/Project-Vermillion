using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject OptionsMenu;

    public Text TextChange;

    public void PlayGame()
    {
        SceneLoader.SharedInstance.gState = SceneLoader.GameState.End;
    }

    public void QuitGame()
    {
        //Application.Quit();
    }

    public void Options()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void Back()
    {
        MainMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }

    public void Change()
    {
        if(SceneLoader.SharedInstance.UseController)
        {
            SceneLoader.SharedInstance.UseController = false;
            TextChange.text = "Change to Controller";
        }
        else
        {
            SceneLoader.SharedInstance.UseController = true;
            TextChange.text = "Change to Keyboard";
        }
    }
}
