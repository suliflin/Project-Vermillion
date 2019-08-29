using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltController : MonoBehaviour
{
    public float speed;

    public float duration;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        duration -= Time.deltaTime;

        if (duration <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
