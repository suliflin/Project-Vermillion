using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScroll : MonoBehaviour
{
    public AudioSource narration;

    public float scrollSpeed = 20;

    public bool isDone;

    private void Start()
    {
        narration.Play();
    }

    void Update()
    {
        Vector3 localVectorUp = transform.TransformDirection(0, 1, 0);

        transform.position += localVectorUp * scrollSpeed * Time.deltaTime;

        StartCoroutine(waitForSound());

        if (isDone)
        {
            SceneLoader.SharedInstance.gState = SceneLoader.GameState.End;
            isDone = false;
        }
    }

    IEnumerator waitForSound()
    {
        yield return new WaitWhile(() => narration.isPlaying);

        isDone = true;
    }

}