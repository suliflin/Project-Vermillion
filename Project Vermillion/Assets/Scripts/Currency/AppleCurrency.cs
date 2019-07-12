using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AppleCurrency : MonoBehaviour
{
    public static int apples;

    public static void AppleIncrease()
    {
        apples += 1;
    }

    public static bool AppleCheck(int amount)
    {
        if (apples < amount)
        {
            return false;
        }
        return true;
    }

    public static void AppleDecrease(int decrease)
    {
        apples -= decrease;
    }
}
