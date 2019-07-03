using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public Text appleText;
    public GameObject barricade;
    public float rotatingSpeed = 130;
    ObjectPooler pool;
    BoxCollider buildingChecker;
    public int detectRange;
    public float angleRange;

    private void Start()
    {
        AppleCurrency.apples = 0;
        ApplesCollected();
        pool = FindObjectOfType<ObjectPooler>().GetComponent<ObjectPooler>();
        buildingChecker = GetComponent<BoxCollider>();
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


        //XINPUT
        float moveHorizontalX = Input.GetAxis("HorizontalXbox");
        float moveVerticalX = Input.GetAxis("VerticalXbox");

        //I hope this works - I dont have a controller 
        moveHorizontalK = moveHorizontalX;
        moveVerticalK = moveVerticalX;

        Vector3 direction = barricade.transform.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);

        if (Vector3.Distance(barricade.transform.position, this.transform.position) < detectRange && angle < angleRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject objectToPull = pool.GetPooledObject("Barricade");
                objectToPull.SetActive(true);
                objectToPull.transform.position = (transform.position + (transform.forward * 2));

                AppleCurrency.AppleDecrease();
                ApplesCollected();
                
                if (AppleCurrency.apples >= 10)
                {


                    
                }
            }
        }
        else
        {
            Debug.Log("Cannot Build");
        }

       
    }

    void ConstructionChecker()
    {
      
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
        appleText.text = "Apples: " + AppleCurrency.apples.ToString();
    }

}
