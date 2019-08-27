using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameManager gm;

    public List<Conversation> conversations;

    public GameObject speakerLeft;
    public GameObject speakerRight;

    public int conversationIndex;

    private SpeakerUI speakerUILeft;
    private SpeakerUI speakerUIRight;

    private int activeLineIndex = 0;

    private bool inDialogue;

    void Start()
    {
        speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();
        gm = GameManager.SharedInstance;

        switch (gm.Act)
        {
            case GameManager.GameAct.One:
                conversations = gm.conversationsActOne;
                break;

            case GameManager.GameAct.Two:
                conversations = gm.conversationsActTwo;
                break;

            case GameManager.GameAct.Three:
                conversations = gm.conversationsActThree;
                break;

            default:
                break;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Square") && inDialogue)
        {
            AdvanceConversation();
        }
    }

    public void AdvanceConversation()
    {
        Time.timeScale = 0;

        if (activeLineIndex < conversations[conversationIndex].lines.Length)
        {
            DisplayLine();
            inDialogue = true;
            activeLineIndex++;
        }
        else
        {
            speakerLeft.SetActive(false);
            speakerRight.SetActive(false);
            activeLineIndex = 0;
            inDialogue = false;
            Time.timeScale = 1;
        }

        if (conversationIndex < conversations.Count)
        {
            conversationIndex++;
        }
        else
        {
            conversationIndex = 0;
        }
    }

    void DisplayLine()
    {
        Line line = conversations[conversationIndex].lines[activeLineIndex];
        Character character = line.character;

        if (character == conversations[conversationIndex].speakerRightTwo)
        {
            speakerUIRight.Speaker = conversations[conversationIndex].speakerRightTwo;
            SetDialogue(speakerUIRight, speakerUILeft, line.text);
        }

        if (speakerUILeft.Speaker == character)
        {
            speakerUILeft.Speaker = conversations[conversationIndex].speakerLeft;
            SetDialogue(speakerUILeft, speakerUIRight, line.text);
        }
        else
        {
            speakerUIRight.Speaker = conversations[conversationIndex].speakerRight;
            SetDialogue(speakerUIRight, speakerUILeft, line.text);
        }
    }

    void SetDialogue(SpeakerUI active, SpeakerUI inactive, string text)
    {
        active.Dialogue = text;
        active.gameObject.SetActive(true);
        inactive.gameObject.SetActive(false);
    }
}