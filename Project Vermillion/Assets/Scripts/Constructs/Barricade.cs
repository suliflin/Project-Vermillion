using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public int health;

    private void Update()
    {
        if (health <= 0)
        {
            transform.position = GameManager.SharedInstance.transform.position;
            gameObject.SetActive(false);
        }
    }
}
