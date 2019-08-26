using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public ObjectPooler pooler;

    [SerializeField]
    public Collider smashColliderBoss;
    public GameObject tpA;
    public GameObject tpB;
    public GameObject teleporter;
    public GameObject radialMenu;
    public GameObject minimap;
    public GameObject selectedObj;

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

    public bool blocked;
    public bool lookAtPlayer = false;
    public bool useController;
    public bool radial;

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

            if (Input.GetMouseButtonDown(0) && !radial)
            {
                crossbow.isFiring = true;
            }

            if (Input.GetMouseButtonUp(0) && !radial)
            {
                crossbow.isFiring = false;
            }
        }
        else
        {
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("HorizontalRight") + Vector3.forward * -Input.GetAxisRaw("VerticalRight");

            playerDirection.y = 0;

            if (playerDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerDirection), 0.1f);
            }

            if (playerDirection.sqrMagnitude > 0.0f && !radial)
            {
                crossbow.isFiring = true;
            }
            else
            {
                crossbow.isFiring = false;
            }
        }

        if (Input.GetButton("R1") && Input.GetButton("L1"))
        {
            radial = true;
            radialMenu.SetActive(true);

            if (Input.GetButtonDown("Square") && CanBuild(barricadeCost))
            {
                AppleDecrease(barricadeCost);
                pooler.SpawnFromPool("Barricade", buildDistance, transform.rotation);
            }

            if (Input.GetButtonDown("Triangle") && CanBuild(turretCost))
            {
                AppleDecrease(turretCost);
                pooler.SpawnFromPool("Turret", buildDistance, transform.rotation);
            }

            if (Input.GetButtonDown("Circle") && CanBuild(teleporterCost))
            {
                BuildTeleporter();
            }

            if (Input.GetButtonDown("X"))
            {                
                if (CanUpgrade(selectedObj))
                {
                    Upgrade(selectedObj);
                }
            }
        }
        else
        {
            radial = false;
            //radialMenu.SetActive(false);

            if (Input.GetButtonDown("Square"))
            {
                //Interact
            }

            if (Input.GetButtonDown("Triangle"))
            {
                if (GameManager.SharedInstance.state == GameManager.WaveState.Countdown)
                {
                    apples += (int)GameManager.SharedInstance.waveCountdown / 10;

                    GameManager.SharedInstance.waveCountdown = 0;
                }
            }

            if (Input.GetButtonDown("Circle"))
            {
                transform.position = GameManager.SharedInstance.recallPoint.transform.position;
            }

            if (Input.GetButton("X"))
            {
                minimap.SetActive(true);
            }
            else
            {
                //minimap.SetActive(false);
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

        if (other.gameObject.CompareTag("Smash"))
        {
            health -= 5;
            smashColliderBoss.enabled = false;
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
        if (!AppleCheck(cost) || blocked)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool CanUpgrade(GameObject obj)
    {
        if (obj.name == "Turret(Clone)" && obj.GetComponent<Turret>().upgrade < 2)
        {
            if (!AppleCheck(obj.GetComponent<Turret>().upgradeCost))
            {
                return false;
            }

            return true;
        }
        else if (obj.name == "Teleporter(Clone)" && obj.GetComponent<Teleporter>().upgrade < 2)
        {
            if (!AppleCheck(obj.GetComponent<Teleporter>().upgradeCost))
            {
                return false;
            }

            return true;
        }
        else if (obj.name == "Barricade(Clone)" && obj.GetComponent<Barricade>().upgrade < 2)
        {
            if (!AppleCheck(obj.GetComponent<Barricade>().upgradeCost))
            {
                return false;
            }
            return true;
        }

        return false;
    }

    public void Upgrade(GameObject obj)
    {
        if (obj.name == "Turret(Clone)")
        {
            AppleDecrease(obj.GetComponent<Turret>().upgradeCost);
            obj.GetComponent<Turret>().upgrade += 1;
        }
        else if (obj.name == "Teleporter(Clone)")
        {
            AppleDecrease(obj.GetComponent<Teleporter>().upgradeCost);
            tpA.GetComponent<Teleporter>().upgrade += 1;
            tpB.GetComponent<Teleporter>().upgrade += 1;
        }
        else if (obj.name == "Barricade(Clone)")
        {
            AppleDecrease(obj.GetComponent<Barricade>().upgradeCost);
            obj.GetComponent<Barricade>().upgrade += 1;
            obj.GetComponent<Barricade>().UpgradeBarricade();
        }
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
}