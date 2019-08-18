using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCheck : MonoBehaviour
{
    public PlayerController player;

    public List<GameObject> detectedObjects;

    private void Start()
    {
        player = gameObject.GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        if (detectedObjects.Count == 0)
        {
            player.blocked = false;
        }
        else
        {
            player.blocked = true;
        }

        float shortestDistance = Mathf.Infinity;
        GameObject closestDetectable = null;

        for (int i = 0; i < detectedObjects.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, detectedObjects[i].transform.position);

            if (distance <= shortestDistance)
            {
                shortestDistance = distance;
                closestDetectable = detectedObjects[i];
            }
        }

        player.selectedObj = closestDetectable;
    }

    private void OnTriggerEnter(Collider other)
    {
        detectedObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        detectedObjects.Remove(other.gameObject);
    }
}
