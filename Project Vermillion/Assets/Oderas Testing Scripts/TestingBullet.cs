using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingBullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
            Destroy(gameObject);
        }
    }
}
