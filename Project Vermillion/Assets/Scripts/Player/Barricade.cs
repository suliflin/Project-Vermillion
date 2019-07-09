using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public GameObject player;
    public int detectRange;
    public float angleRange;
    ObjectPooler pool;
    // Start is called before the first frame update
    void Start()
    {
        ApplesCollected();
        pool = FindObjectOfType<ObjectPooler>().GetComponent<ObjectPooler>();
    }

    // Update is called once per frame
    void Update()
    {

      
    }

    public void CanBuild()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);

        if (Vector3.Distance(player.transform.position, this.transform.position) < detectRange && angle < angleRange)
        {
            Debug.Log("working");
            if (AppleCurrency.AppleDecrease(5))
            {
                GameObject objectToPull = pool.GetPooledObject("Barricade");
                objectToPull.SetActive(true);
                objectToPull.transform.position = (transform.position + (transform.forward * 2));

                AppleCurrency.AppleDecrease(5);

                ApplesCollected();

                if (AppleCurrency.apples >= 10)
                {



                }
            }
            else
            {
                Debug.Log("Not enough apples"); 
            }
        }
        
    }

    public void ApplesCollected()
    {
        //appleText.text = "Apples: " + AppleCurrency.apples.ToString();
    }
}

