using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    Text appleText;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            Debug.Log("Instance = this");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }
    public static void AppleIncrease()
    {
        Instance.apples += 1;
        DisplayText();
    }

    public static bool AppleCheck(int amount)
    {
        if (Instance.apples < amount)
        {
            return false;
        }
        return true;
    }

    public static void AppleDecrease(int decrease)
    {
        Instance.apples -= decrease;
        DisplayText();
    }
    public static void UpdateAppleCounterUI(int newAppleCount)
    {
        Instance.appleText.text = newAppleCount.ToString();
    }*/
}
