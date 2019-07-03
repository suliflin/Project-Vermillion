using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AppleCurrency : MonoBehaviour
{
    public static int apples;
    public static int barricadePriceApples = 10;

    public static void AppleIncrease()
    {
        apples += 1;
    }
    public static void AppleDecrease()
    {

        apples = apples - barricadePriceApples;

    }




}
