using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform bullets;

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MouseShoot();
        }
    }

    public void MouseShoot()
    {
        Ray ray = new Ray(bullets.position, bullets.forward);
        RaycastHit hit;

        float shotDistance = 20;

        if (Physics.Raycast(ray, out hit, shotDistance))
        {
            shotDistance = hit.distance;
        }

        Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1);

    }



}
