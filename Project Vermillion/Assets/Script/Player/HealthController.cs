using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public static HealthController instance;

    [SerializeField] GameObject[] hearts = new GameObject[4];
    public int currentHeart;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentHeart = hearts.Length - 1;
    }

    // Update is called once per frame
    public void LoseHeart()
    {
        if (currentHeart >= 0)
        {
            hearts[currentHeart].SetActive(false);
            currentHeart--;
        }

    }
}