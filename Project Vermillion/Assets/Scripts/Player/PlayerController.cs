using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public GameObject teleporterA;
    public GameObject teleporterB;

    public Camera mainCamera;

    public CrossbowController crossbow;

    public float moveSpeed;
    public float teleporterTimer = 0;
    public float rotatingSpeed = 130;

    public float smooth = 0.3f;
    public float detectRange;
    public float cameraHeight;
    public float cameraDistance;

    public bool built = false;
    public bool useController;

    private Rigidbody rb;
    private Vector3 moveInput;


    private Vector3 moveVelocity;
    private Vector3 velocity = Vector3.zero;

    public Text realAppleText;

    [SerializeField]
    int apples;

    void Start()
    {
        apples = 0;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //  realAppleText.text = "x" + AppleCurrency.apples.ToString();

        Vector3 pos = new Vector3();
        pos.x = transform.position.x;
        pos.z = transform.position.z + cameraDistance;
        pos.y = transform.position.y + cameraHeight;
        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, pos, ref velocity, smooth);

        moveInput = new Vector3(Input.GetAxisRaw("HorizontalLeft"), 0, Input.GetAxisRaw("VerticalLeft"));
        moveVelocity = moveInput * moveSpeed;

        if (!useController)
        {
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            RaycastHit hit;

            if (Physics.Raycast(cameraRay, out hit))
            {
                Vector3 pointToLook = hit.point;
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.black);
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }

            if (Input.GetMouseButtonDown(0))
            {
                crossbow.isFiring = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                crossbow.isFiring = false;
            }
        }
        else
        {
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("HorizontalRight") + Vector3.forward * -Input.GetAxisRaw("VerticalRight");

            if (playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                crossbow.isFiring = true;
            }
            else
            {
                crossbow.isFiring = false;
            }

            if (Input.GetButtonDown("Square"))
            {
                GameObject barricade = ObjectPooler.SharedInstance.GetPooledObject("Barricade");
                Build(barricade, 1);
            }
            if (Input.GetButtonDown("Triangle"))
            {
                GameObject turret = ObjectPooler.SharedInstance.GetPooledObject("Turret");
                Build(turret, 1);
            }
            if (Input.GetButtonDown("Circle"))
            {
                GameObject teleporter = ObjectPooler.SharedInstance.GetPooledObject("Teleporter");

                if (CanBuildTeleporter())
                {
                    Build(teleporter, 1);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveVelocity, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Apples"))
        {
            UIManager.UpdateAppleCounterUI(++apples);
            other.gameObject.SetActive(false);
        }
    }

    public bool AppleCheck(int v)
    {
        return v <= apples;
    }


    public void AppleDecrease(int v)
    {
        apples -= v;
    }

    public bool Build(GameObject build, int cost)
    {
        if (cost > apples)
        {
            Debug.Log("Not enough apples");
            return false;
        }

        build.transform.position = (transform.position + (transform.forward * 2));
        build.transform.rotation = transform.rotation;

        apples -= cost;
        build.SetActive(true);
        return true;


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
            }
            else if (ObjectPooler.SharedInstance.pooledObjects[i].activeInHierarchy && ObjectPooler.SharedInstance.pooledObjects[i].tag == "Teleporter" && teleporterA != null)
            {
                teleporterB = ObjectPooler.SharedInstance.pooledObjects[i];
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