using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    public int health;
    public int shieldHealth;

    public bool isShielded;

    void Start()
    {

    }

    void Update()
    {
        Debug.Log(health);
        if (health <= 0)
        {
            gameObject.SetActive(false);
            health = 5;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bolt")
        {
            health -= 1;
            other.gameObject.SetActive(false);
        }
    }
}