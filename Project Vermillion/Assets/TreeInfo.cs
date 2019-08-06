using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInfo : MonoBehaviour
{
    public int health;

    private void Update()
    {
        if (health <= 0)
        {
            //Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword") || other.gameObject.CompareTag("Shield"))
        {
            health -= 1;
        }
    }
}
