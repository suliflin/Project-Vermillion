using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float rotatingSpeed = 130;
<<<<<<< HEAD
    // Update is called once per frame
    void Update()
    {
        //Keyboard Controls
        float moveHorizontalK = Input.GetAxis("HorizontalKeyboard");
        float moveVerticalK = Input.GetAxis("VerticalKeyboard");
=======
    public float smooth = 0.3f;
    public float detectRange;
    public float cameraHeight;

    public bool built = false;
    public bool useController;

    private Rigidbody rb;

    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Vector3 velocity = Vector3.zero;

    public Text realAppleText;

    void Start()
    {
        AppleCurrency.apples = 0;
        AppleCurrency.appleText = realAppleText;
        rb = GetComponent<Rigidbody>();
    }

    //Update is called once per frame
    void Update()
    {
//<<<<<<< HEAD
        Debug.Log(AppleCurrency.apples);
        realAppleText.text = "x" + AppleCurrency.apples.ToString();
//=======
        Vector3 pos = new Vector3();
        pos.x = transform.position.x;
        pos.z = transform.position.z;
        pos.y = transform.position.y + cameraHeight;
        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, pos, ref velocity, smooth);
//>>>>>>> e809a4457348f87087beec567b707a7f20f0e145

        moveInput = new Vector3(Input.GetAxisRaw("HorizontalLeft"), 0, Input.GetAxisRaw("VerticalLeft"));
        moveVelocity = moveInput * moveSpeed;

        if (!useController)
        {
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
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
    }
>>>>>>> parent of 22f6c88... Movement for keyboard and aiming fixed.

        GetComponent<Rigidbody>().velocity = new Vector3(moveHorizontalK, 0, moveVerticalK);

        //Turning with keyboard
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, rotatingSpeed * Time.deltaTime, 0);
            Debug.Log("turning right");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -rotatingSpeed * Time.deltaTime, 0);
            Debug.Log("turning left");
        }


        //XINPUT
        float moveHorizontalX = Input.GetAxis("HorizontalXbox");
        float moveVerticalX = Input.GetAxis("VerticalXbox");

        //I hope this works - I dont have a controller 
        moveHorizontalK = moveHorizontalX;
        moveVerticalK = moveVerticalX;



        //always use Debug.Log instead of System.Console.WriteLine(   );
        //GetComponent<Rigidbody>().velocity = new Vector3(moveHorizontalX, 0, moveHorizontalX);
    }
}
