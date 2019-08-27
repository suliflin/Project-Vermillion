using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerController player;

    public WavesManager wm;

    public Text appleCount;
    public Text waveCount;
    public Text waveTimer;

    public Image[] hearts;

    public Sprite emptyHearts;
    public Sprite fullHeart;

    public int health;
    public int numOfHearts;
    public int currentWave;
    public int timer;

    void Start()
    {
        numOfHearts = GameManager.SharedInstance.playerHealthMax;
    }

    void Update()
    {
        currentWave = wm.nextWave + 1;
        timer = (int)GameManager.SharedInstance.waveCountdown;

        appleCount.text = player.apples.ToString();
        waveCount.text = currentWave.ToString() + "/10";
        waveTimer.text = timer.ToString();

        health = GameManager.SharedInstance.playerHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHearts;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
