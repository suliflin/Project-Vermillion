using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public Text appleText;

    public float teleporterTimer = 0;
    public float rotatingSpeed = 130;
    public float angleRange;
    public float detectRange;

    public bool built = false;

    public GameObject teleporterA;
    public GameObject teleporterB;

    private void Start()
    {
        AppleCurrency.apples = 20;
    }

    //Update is called once per frame
    void Update()
    {
        Debug.Log(AppleCurrency.apples);
        //Keyboard Controls
        float moveHorizontal = Input.GetAxis("HorizontalLeft");
        float moveVertical = Input.GetAxis("VerticalLeft");

        GetComponent<Rigidbody>().velocity = new Vector3(3 * moveHorizontal, 0, 3 * moveVertical);

        //Turning with keyboard
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, rotatingSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -rotatingSpeed * Time.deltaTime, 0);
        }

        if (Input.GetButtonDown("Square"))
        {
            GameObject barricade = ObjectPooler.SharedInstance.GetPooledObject("Barricade");
            CanBuild(barricade, 1);
        }
        if (Input.GetButtonDown("Triangle"))
        {
            GameObject turret = ObjectPooler.SharedInstance.GetPooledObject("Turret");
            CanBuild(turret, 1);
        }
        if (Input.GetButtonDown("Circle"))
        {
            GameObject teleporter = ObjectPooler.SharedInstance.GetPooledObject("Teleporter");

            if (CanBuildTeleporter())
            {
                CanBuild(teleporter, 1);
            }
        }
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

    public void ApplesCollected()
    {
        //appleText.text = "Apples: " + AppleCurrency.apples.ToString();
    }

    public bool CanBuild(GameObject build, int cost)
    {
        int index = 0;
        int count = 0;
        build.transform.position = (transform.position + (transform.forward * 2));
        build.transform.rotation = transform.rotation;

        for (int i = 0; i < ObjectPooler.SharedInstance.pooledObjects.Count; i++)
        {
            if (ObjectPooler.SharedInstance.pooledObjects[i].activeInHierarchy && ObjectPooler.SharedInstance.pooledObjects[i].tag != "Enemy")
            {
                index++;
                if (Vector3.Distance(ObjectPooler.SharedInstance.pooledObjects[i].transform.position, build.transform.position) > detectRange)
                {
                    count++;
                }
                else
                {
                    Debug.Log("Too close");
                }
            }
            else if (!built)
            {
                if (AppleCurrency.AppleCheck(cost))
                {
                    AppleCurrency.AppleDecrease(cost);
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

        if (AppleCurrency.AppleCheck(cost) && count == index)
        {
            AppleCurrency.AppleDecrease(cost);
            build.SetActive(true);
            return true;
        }
        else
        {
            Debug.Log("Not enough apples");
            return true;
        }
    }

    public bool CanBuildTeleporter()
    {
        int count = 0;

        for (int i = 0; i < ObjectPooler.SharedInstance.pooledObjects.Count; i++)
        {
            if (ObjectPooler.SharedInstance.pooledObjects[i].activeInHierarchy && ObjectPooler.SharedInstance.pooledObjects[i].tag == "Teleporter")
            {
                count++;
            }
        }

        if (count == 2)
        {
            TeleporterLink();
            return false;
        }

        return true;
    }

    public void TeleporterLink()
    {
        for (int i = 0; i < ObjectPooler.SharedInstance.pooledObjects.Count; i++)
        {
            if (ObjectPooler.SharedInstance.pooledObjects[i].activeInHierarchy && ObjectPooler.SharedInstance.pooledObjects[i].tag == "Teleporter" && teleporterA == null)
            {
                teleporterA = ObjectPooler.SharedInstance.pooledObjects[i];
                teleporterA.GetComponent<Teleporter>().player = gameObject;
            }
            else if (ObjectPooler.SharedInstance.pooledObjects[i].activeInHierarchy && ObjectPooler.SharedInstance.pooledObjects[i].tag == "Teleporter" && teleporterA != null)
            {
                teleporterB = ObjectPooler.SharedInstance.pooledObjects[i];
                teleporterB.GetComponent<Teleporter>().player = gameObject;
            }
            else
            {
                Debug.Log("Press Circle again to link");
            }
        }
        teleporterA.GetComponent<BoxCollider>().enabled = true;
        teleporterB.GetComponent<BoxCollider>().enabled = true;
        teleporterA.GetComponent<Teleporter>().destination = teleporterB.transform.GetChild(0).transform;
        teleporterB.GetComponent<Teleporter>().destination = teleporterA.transform.GetChild(0).transform;
    }
}