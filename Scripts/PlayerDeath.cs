using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerDeath : MonoBehaviour
{
    public void Dead()
    {
        Debug.Log("player died");
        GetComponent<Animator>().SetTrigger("IsPlayerDying");
        GetComponent<AudioSource>().Play();
        
    }

    public void AfterDeath()
    {
        SceneManager.LoadScene(4);
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
