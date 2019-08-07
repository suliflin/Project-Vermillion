using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;

    public PlayerController player;

    public int health;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        player.GetComponent<PlayerController>().teleporterTimer -= Time.deltaTime;

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && player.teleporterTimer <= 0)
        {
            if (player.AppleCheck(1))
            {
                other.gameObject.transform.position = destination.position;
                player.teleporterTimer = 10;
                player.AppleDecrease(1);
            }
            else
            {
                Debug.Log("Not enough Apples");
            }
        }

        if (other.gameObject.CompareTag("Sword") || other.gameObject.CompareTag("Shield"))
        {
            health -= 1;
        }
    }
}