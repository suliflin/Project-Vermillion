using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public  GameObject barricade;
    public  GameObject player;
    public static bool barricadeWall;



    private void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if(barricadeWall == true)
        {
            //Instantiate(barricade, player.transform.position + (player.transform.forward * 2), player.transform.rotation);
           
        }
        
    }

    public static void Barricaded()
    {

       
        
    }
}
