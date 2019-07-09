using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AppleCurrency : MonoBehaviour
{
    public static int apples;

   // public static int barricadePriceApples = 10;

    public static void AppleIncrease()
    {
        apples += 1;
    }
    /* Add Parameters and then decrease the amount of apples based on that parameter
     * Then call the function where it needs to be decreased and input the amount there
     * Currently this will only work with Barricades not teleporters
     */
    public static bool AppleDecrease(int decrease)
    {
        if(apples >= decrease)
        {

            apples -= decrease;

            return true;
            
            
        }
        else return false;

    }
}
