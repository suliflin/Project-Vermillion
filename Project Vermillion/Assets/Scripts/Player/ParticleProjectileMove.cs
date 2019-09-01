using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleProjectileMove : MonoBehaviour
{
    public float speed;
    public float firerate;
    public GameObject muzzlePrefab;


    // Start is called before the first frame update
    void Start()
    {
        if(muzzlePrefab != null)
        {
            GameObject muzzleVFX = Instantiate(muzzlePrefab, transform.position, transform.rotation);
            muzzleVFX.transform.forward = gameObject.transform.forward;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
