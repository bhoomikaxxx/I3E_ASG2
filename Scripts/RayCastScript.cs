using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RayCastScript : MonoBehaviour
{
    //Collectible count
    int count = 0;

    //Raycast
    void CheckColliders()
    {
        Ray rays = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(rays, out RaycastHit hit))
        {

            if (hit.collider.tag == "Teleporter")
            {
                SceneManager.LoadScene(2);
                GetComponent<AudioSource>().Play();
            }
            if (hit.collider.tag == "Teleporter2")
            {
                SceneManager.LoadScene(1);
                GetComponent<AudioSource>().Play();
            }
            if (hit.collider.tag == "SpecialCollectible")
            {
                SceneManager.LoadScene(3);
                GetComponent<AudioSource>().Play();
            }
            if (hit.collider.tag == "Collectibles")
            {
                count += 1;
                GetComponent<AudioSource>().Play();
                
                if (count >= 3)
                {
                    Destroy(GameObject.FindWithTag("Collectibles"));
                    FindObjectOfType<DoorRightScript>().GetComponent<DoorRightScript>().Collected();
                    FindObjectOfType<DoorLeftScript>().GetComponent<DoorLeftScript>().Collected();
                }
            }
            /* if (hit.collider.tag == "Door")
            {
                if (count <= 3)
                {
                    GetComponent<Animator>().SetTrigger("isCollectedRDoor");
                    GetComponent<Animator>().SetTrigger("isCollectedLDoor");
                }
            }*/
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
