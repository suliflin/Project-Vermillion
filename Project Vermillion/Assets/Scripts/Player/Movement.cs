using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public Text appleText;
   
    public float rotatingSpeed = 130;
    public float angleRange;
    public float detectRange;

    public bool built = false;

    private void Start()
    {
        AppleCurrency.apples = 5;
    }

    void Update()
    {
        //Keyboard Controls
        float moveHorizontalK = Input.GetAxis("HorizontalKeyboard");
        float moveVerticalK = Input.GetAxis("VerticalKeyboard");

        GetComponent<Rigidbody>().velocity = new Vector3(3 * moveHorizontalK, 0, 3 * moveVerticalK);

        //Turning with keyboard
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, rotatingSpeed * Time.deltaTime, 0);
            Debug.Log("turning rights");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -rotatingSpeed * Time.deltaTime, 0);
            Debug.Log("turning left");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject barricade = ObjectPooler.SharedInstance.GetPooledObject("Barricade");
            CanBuild(barricade, "Barricade", 1);
        }

        //XINPUT
        float moveHorizontalX = Input.GetAxis("HorizontalXbox");
        float moveVerticalX = Input.GetAxis("VerticalXbox");

        //I hope this works - I dont have a controller 
        moveHorizontalK = moveHorizontalX;
        moveVerticalK = moveVerticalX;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Apples"))
        {
            AppleCurrency.AppleIncrease();
            ApplesCollected();
            other.gameObject.SetActive(false);
        }
    }

    //The text isn't assigned so it gives a null reference once you fix it I'll put the code back
    public void ApplesCollected()
    {
        //appleText.text = "Apples: " + AppleCurrency.apples.ToString();
    }

    public bool CanBuild(GameObject build, string tag, int cost)
    {
        int index = 0;
        int count = 0;
        build.transform.position = (transform.position + (transform.forward * 2));

        for (int i = 0; i < ObjectPooler.SharedInstance.pooledObjects.Count; i++)
        {
            if (ObjectPooler.SharedInstance.pooledObjects[i].activeInHierarchy && ObjectPooler.SharedInstance.pooledObjects[i].tag == tag)
            {
                index++;
                Debug.Log(Vector3.Distance(ObjectPooler.SharedInstance.pooledObjects[i].transform.position, build.transform.position));
                if (Vector3.Distance(ObjectPooler.SharedInstance.pooledObjects[i].transform.position, build.transform.position) > detectRange)
                {
                    count++;
                    if (AppleCurrency.AppleDecrease(cost) && count == index)
                    {
                        build.SetActive(true);
                        return true;
                    }
                    else
                    {
                        Debug.Log("Not enough apples");
                        return true;
                    }
                }
                else
                {
                    Debug.Log("Too close");
                }
            }
            else if (!built)
            {
                if (AppleCurrency.AppleDecrease(cost))
                {
                    build.SetActive(true);
                    built = true;
                    return true;
                }
                else
                {
                    Debug.Log("Not enough apples");
                    return false;
                }
            }
        }
        return false;
    }
}
