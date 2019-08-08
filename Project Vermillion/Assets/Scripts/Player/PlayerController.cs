using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public ObjectPooler pooler;

    public GameObject tpA;
    public GameObject tpB;
    public GameObject teleporter;

    public Camera mainCamera;

    public CrossbowController crossbow;

    public LayerMask layerMask;

    public Vector3 buildDistance;

    public int health;
    public int barricadeCost;
    public int teleporterCost;
    public int turretCost;

    public float moveSpeed;
    public float teleporterTimer = 0;
    public float smooth = 0.3f;

    public bool built = false;
    public bool lookAtPlayer = false;
    public bool useController;

    private Rigidbody rb;

    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Vector3 cameraOffset;
    private Vector3 velocity = Vector3.zero;

    public Text realAppleText;

    [SerializeField]
    int apples;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pooler = ObjectPooler.SharedInstance;

        cameraOffset = mainCamera.transform.position - transform.position;
    }

    void Update()
    {
        //  realAppleText.text = "x" + AppleCurrency.apples.ToString();

        if (health <= 0)
        {
            //Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }

        buildDistance = (transform.position + (transform.forward * 2));

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

            if (Input.GetButtonDown("Square") && AppleCheck(barricadeCost))
            {
                AppleDecrease(barricadeCost);
                pooler.SpawnFromPool("Barricade", buildDistance, transform.rotation);
            }

            if (Input.GetButtonDown("Triangle") && AppleCheck(turretCost))
            {
                AppleDecrease(turretCost);
                pooler.SpawnFromPool("Turret", buildDistance, transform.rotation);
            }

            if (Input.GetButtonDown("Circle") && AppleCheck(teleporterCost))
            {
                BuildTeleporter();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveVelocity, ForceMode.Force);
    }

    private void LateUpdate()
    {
        Vector3 newPos = transform.position + cameraOffset;

        mainCamera.transform.position = Vector3.Slerp(mainCamera.transform.position, newPos, smooth);

        if (lookAtPlayer)
        {
            mainCamera.transform.LookAt(transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Apples"))
        {
            //UIManager.UpdateAppleCounterUI(++apples);
            apples++;
            other.transform.position = GameManager.SharedInstance.transform.position;
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

    public bool CanBuild(int cost)
    {
        if (!AppleCheck(cost))
        {
            return false;
        }

        RaycastHit hit;

        if (Physics.SphereCast(transform.position, 3, transform.forward, out hit, 5, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            return false;
        }

        return true;
    }

    public void BuildTeleporter()
    {
        if (tpA != null && tpB != null)
        {
            return;
        }

        if (tpA == null)
        {
            tpA = Instantiate(teleporter, buildDistance, transform.rotation);
            AppleDecrease(teleporterCost);
            return;
        }
        else
        {
            tpB = Instantiate(teleporter, buildDistance, transform.rotation);
            AppleDecrease(teleporterCost);
            TeleporterLink();
        }

    }

    public void TeleporterLink()
    {
        tpA.GetComponent<BoxCollider>().enabled = true;
        tpB.GetComponent<BoxCollider>().enabled = true;
        tpA.GetComponent<Teleporter>().destination = tpB.transform.GetChild(0).transform;
        tpB.GetComponent<Teleporter>().destination = tpA.transform.GetChild(0).transform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position, buildDistance);
        Gizmos.DrawWireSphere(buildDistance, 3);
    }
}