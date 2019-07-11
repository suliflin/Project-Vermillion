using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    public Text appleText;

    private ObjectPooler pool;
    public float rotatingSpeed = 130;
    public Barricade barricade;

    //Sultan you fix the barricade..
    private void Start()
    {
        pool = GameObject.FindGameObjectWithTag("Pool").GetComponent<ObjectPooler>();
        AppleCurrency.apples = 0;
    }

    //Update is called once per frame
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
            barricade.CanBuild();

        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            TurretPlacing();
        }

        //XINPUT
        float moveHorizontalX = Input.GetAxis("HorizontalXbox");
        float moveVerticalX = Input.GetAxis("VerticalXbox");

        //I hope this works - I dont have a controller 
        moveHorizontalX = moveHorizontalK;
        moveVerticalX = moveVerticalK;
    }

   

    public void TurretPlacing()
    {
        GameObject placed = pool.GetPooledObject("Turret");
        placed.SetActive(true);
        placed.transform.position = (transform.position + (transform.forward * 2));

        Debug.Log("Awesome");
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

}
