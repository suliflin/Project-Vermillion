using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public Text appleText;
   
    public float rotatingSpeed = 130;
    public Barricade barricade;
  
   //Sultan you fix this..

    private void Start()
    {
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

        if (Input.GetKey(KeyCode.E))
        {
            barricade.CanBuild();
            
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

}
