using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public Screen current; // Current State

    // Start is called before the first frame update
    void Start()
    {
        current.gameObject.SetActive(true);
    }

    private void Update()
    {
        current.Execute(this);
    }

    public void SetState(Screen newScreen)
    {
        current.gameObject.SetActive(false);
        current = newScreen;
        current.gameObject.SetActive(true);
    }
}