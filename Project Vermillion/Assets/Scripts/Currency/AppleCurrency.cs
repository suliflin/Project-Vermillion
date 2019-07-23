using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppleCurrency : MonoBehaviour
{
    public static int apples;
    public static Text appleText;

    public static void AppleIncrease()
    {
        apples += 1;
        DisplayText();
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
        DisplayText();
    }

    public static void DisplayText()
    {
        appleText.text = apples.ToString();
    }
}
