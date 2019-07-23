using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;

    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        player.GetComponent<Movement>().teleporterTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && player.GetComponent<Movement>().teleporterTimer <= 0)
        {
            if (AppleCurrency.AppleCheck(1))
            {
                other.gameObject.transform.position = destination.position;
                player.GetComponent<Movement>().teleporterTimer = 10;
                AppleCurrency.AppleDecrease(1);
            }
            else
            {
                Debug.Log("Not enough Apples");
            }
        }
    }
}
//