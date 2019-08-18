using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakerUI : MonoBehaviour
{
    public Image portrait;

    public Text fullName;
    public Text dialogue;

    private Character speaker;

    public Character Speaker
    {
        get
        {
            return speaker;
        }

        set
        {
            speaker = value;
            portrait.sprite = speaker.portrait;
            fullName.text = speaker.fullName;
        }
    }
    
    public string Dialogue
    {
        set
        {
            dialogue.text = value;
        }
    }

    public bool SpeakerIs(Character character)
    {
        return speaker == character;
    }
}
