using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RayCastScript : MonoBehaviour
{
    //Collectible count
    int count = 0;
    bool openDoorOnce = false;
    //Raycast
    void CheckColliders()
    {
        Ray rays = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(rays, out RaycastHit hit, 3))
        {
            

            if (hit.collider.tag == "Teleporter")
            {
                Debug.Log("teleport");

                SceneManager.LoadScene(2);
                hit.collider.GetComponent<AudioSource>().Play();
            }
            if (hit.collider.tag == "Teleporter2")
            {
                Debug.Log("teleport");
                SceneManager.LoadScene(1);
                hit.collider.GetComponent<AudioSource>().Play();
            }
            if (hit.collider.tag == "Fakegun")
            {
                FindObjectOfType<PlayerScript>().ActivateGun();
                Destroy(GameObject.FindWithTag("Fakegun"));
                hit.collider.GetComponent<AudioSource>().Play();
            }
            if (hit.collider.tag == "SpecialCollectible")
            {
                SceneManager.LoadScene(3);
                hit.collider.GetComponent<AudioSource>().Play();
            }
            if (hit.collider.tag == "Collectibles")
            {
                Debug.Log(count);
                count += 1;
                hit.collider.GetComponent<AudioSource>().Play();
                hit.collider.enabled = false;
                Destroy(hit.collider.gameObject, 1f);

                MainDoorScript mainDoor = FindObjectOfType<MainDoorScript>();

                if (mainDoor != null)
                {
                    mainDoor.GetComponent<MainDoorScript>().Collected();

                }

                if (count >= 2)
                {
                    openDoorOnce = true;
                    
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

        if (openDoorOnce == true)
        {
            DoorRightScript doorRight = FindObjectOfType<DoorRightScript>();
            if (doorRight != null)
            {
                doorRight.GetComponent<DoorRightScript>().Collected();
            }

            DoorLeftScript doorLeft = FindObjectOfType<DoorLeftScript>();
            if (doorLeft != null)
            {
                doorLeft.GetComponent<DoorLeftScript>().Collected();
                openDoorOnce = false;
            }
            
        }
    }
}
