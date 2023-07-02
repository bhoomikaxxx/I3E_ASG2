using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRightScript : MonoBehaviour
{


    public void Collected()
    {
        GetComponent<Animator>().SetTrigger("isCollectedRDoor");

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
