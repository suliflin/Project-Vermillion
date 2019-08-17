using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public int health;
    public int upgrade = 0;
    public int upgradeCost;

    private void Update()
    {
        if (health <= 0)
        {
            transform.position = GameManager.SharedInstance.transform.position;
            gameObject.SetActive(false);
        }
    }

    public void UpgradeBarricade()
    {
        if (upgrade == 1)
        {
            //Change texture
            upgradeCost = 2;
            health = 15;
        }
        else if (upgrade == 2)
        {
            //Change texture
            health = 20;
        }
    }
}
