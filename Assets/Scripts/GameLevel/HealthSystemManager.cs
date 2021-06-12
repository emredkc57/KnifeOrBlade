using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystemManager : MonoBehaviour
{

    public GameObject heart1, heart2, heart3, heart4;

    private static HealthSystemManager healthSystemManager;

    // Start is called before the first frame update
    void Start()
    {

        heart1 = transform.GetChild(0).GetChild(0).gameObject;
        heart2 = transform.GetChild(0).GetChild(1).gameObject;
        heart3 = transform.GetChild(0).GetChild(2).gameObject;
        heart4 = transform.GetChild(0).GetChild(3).gameObject;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Singleton Requires for This class

    public static HealthSystemManager GetInstance()
    {

        if(healthSystemManager == null)
        {

            return new HealthSystemManager();
        }

        return healthSystemManager;
   

    }
    


}
