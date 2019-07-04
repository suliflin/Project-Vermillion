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
        /* All of this barricade logic should be in the barricade
         * all you need here is to take the input and then call a function
         * in barricade to check if you can build or not and in barricade 
         * you'll deduct the apple cost using the appledecrease function
         */
        Vector3 direction = barricade.transform.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        /* You encounter the problem of placing this here when you start the game
         * the if statement is always false because barricade doesn't exist
         * so you need to have the input be the outermost if condition and then
         * inside it you have another if to make it work or not
         */
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
    //What's this?
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
    //The text isn't assigned so it gives a null reference once you fix it I'll put the code back
    public void ApplesCollected()
    {
        //appleText.text = "Apples: " + AppleCurrency.apples.ToString();
    }

}
