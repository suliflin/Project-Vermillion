using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public enum GameAct
    {
        MainMenu,
        Narration,
        One,
        Two,
        Three
    }

    public enum GameState
    {
        Start,
        Play,
        End
    }

    public GameState gState = GameState.Start;
    public GameAct Act = GameAct.Narration;

    public bool UseController;

    private GameManager gm;

    #region Singleton
    public static SceneLoader SharedInstance;

    private void Awake()
    {
        if (SharedInstance != null && SharedInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            SharedInstance = this;
            DontDestroyOnLoad(this);
        }
    }
    #endregion

    void Update()
    {
        if(gState == GameState.Start)
        {
            gm = GameManager.SharedInstance;
        }

        if (gState == GameState.End)
        {
            if (Act == GameAct.MainMenu)
            {
                SceneManager.LoadScene("Narration");
                gState = GameState.Start;
            }

            if (Act == GameAct.Narration)
            {
                SceneManager.LoadScene("Level 1");
                gState = GameState.Start;
            }

            if (Act == GameAct.One)
            {
                SceneManager.LoadScene("Level 2");
                gState = GameState.Start;
            }

            if (Act == GameAct.Two)
            {
                SceneManager.LoadScene("Level 3");
                gState = GameState.Start;
            }

            if (Act == GameAct.Three)
            {
                //Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }
}
