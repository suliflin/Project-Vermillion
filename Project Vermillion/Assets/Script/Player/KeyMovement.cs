using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMovement : MonoBehaviour
{
    public float rotatingSpeed = 130;
    // Update is called once per frame
    void Update()
    {
        //Keyboard Controls
        float moveHorizontalK = Input.GetAxis("HorizontalKeyboard");
        float moveVerticalK = Input.GetAxis("VerticalKeyboard");

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
