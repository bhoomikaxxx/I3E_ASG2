using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerDeath : MonoBehaviour
{
    public void Dead()
    {
        GetComponent<Animator>().SetTrigger("IsPlayerDead");
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(3);

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
