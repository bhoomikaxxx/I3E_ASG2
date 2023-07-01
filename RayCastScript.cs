using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RayCastScript : MonoBehaviour
{
    //Raycast
    void CheckColliders()
    {
        Ray rays = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(rays, out RaycastHit hit))
        {

            if (hit.collider.tag == "Teleporter")
            {
                SceneManager.LoadScene(3);
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckColliders();
    }
}
