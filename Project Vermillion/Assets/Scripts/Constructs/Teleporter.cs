using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public List<ParticleSystem> teleportEffect;

    public Transform destination;

    public PlayerController player;

    public int health;
    public int upgrade = 0;
    public int upgradeCost = 1;
    public int teleportCost;

    public float teleporterTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        player.GetComponent<PlayerController>().teleporterTimer -= Time.deltaTime;

        if (player.teleporterTimer <= 0)
        {
            for (int i = 0; i < teleportEffect.Count; i++)
            {
                teleportEffect[i].Play();
                destination.parent.GetComponent<Teleporter>().teleportEffect[i].Play();
            }
        }

        if (health <= 0)
        {
            transform.position = GameManager.SharedInstance.transform.position;
            gameObject.SetActive(false);
        }

        if (upgrade == 1)
        {
            //Change texture
            upgradeCost = 2;
            teleporterTimer = 8;
        }
        else if (upgrade == 2)
        {
            //Change texture
            teleportCost = 0;
            teleporterTimer = 4;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && player.teleporterTimer <= 0)
        {
            if (player.AppleCheck(teleportCost))
            {
                other.gameObject.transform.position = destination.position;
                player.teleporterTimer = teleporterTimer;
                player.AppleDecrease(1);

                for (int i = 0; i < teleportEffect.Count; i++)
                {
                    teleportEffect[i].Stop();
                    destination.parent.GetComponent<Teleporter>().teleportEffect[i].Stop();
                }
            }
            else
            {
                Debug.Log("Not enough Apples");
            }
        }

        if (other.gameObject.CompareTag("Smash"))
        {
            GameManager.SharedInstance.SetDamage(1, this.gameObject);
        }
    }
}