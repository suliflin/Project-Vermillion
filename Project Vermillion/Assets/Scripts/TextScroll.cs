using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScroll : MonoBehaviour
{
    public AudioSource narration;

    public float scrollSpeed = 20;

    void Update()
    {
        Vector3 localVectorUp = transform.TransformDirection(0, 1, 0);

        transform.position += localVectorUp * scrollSpeed * Time.deltaTime;

        if (!narration.isPlaying)
        {
            SceneLoader.SharedInstance.gState = SceneLoader.GameState.End;
        }
    }
}
