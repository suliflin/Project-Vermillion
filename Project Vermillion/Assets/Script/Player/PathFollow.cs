using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    public Transform lookPos;

    public float speed;

    [SerializeField]
    private Transform[] routes;

    private int routeIndex;

    private float pathParam;

    private Vector3 camPos;

    private bool coroutineAllowed;

    void Start()
    {
        routeIndex = 0;
        pathParam = 0;
        coroutineAllowed = true;
    }

    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(FollowRoute(routeIndex));
        }

        transform.LookAt(lookPos);
    }

    private IEnumerator FollowRoute(int index)
    {
        coroutineAllowed = false;

        Vector3 p0 = routes[index].GetChild(0).position;
        Vector3 p1 = routes[index].GetChild(1).position;
        Vector3 p2 = routes[index].GetChild(2).position;
        Vector3 p3 = routes[index].GetChild(3).position;

        while (pathParam < 1)
        {
            pathParam += Time.deltaTime * speed;

            camPos = Mathf.Pow(1 - pathParam, 3) *
                p0 + 3 * Mathf.Pow(1 - pathParam, 2) * pathParam *
                p1 + 3 * (1 - pathParam) * Mathf.Pow(pathParam, 2) *
                p2 + Mathf.Pow(pathParam, 3) * p3;

            transform.position = camPos;
            yield return new WaitForEndOfFrame();
        }

        pathParam = 0;

        routeIndex++;

        if (routeIndex > routes.Length - 1)
        {
            routeIndex = 0;
        }

        coroutineAllowed = true;
    }
}
