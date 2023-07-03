using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainScript : MonoBehaviour
{
    

    /// <summary>
    /// The prefab of the player used for spawning.
    /// </summary>
    public GameObject playerPrefab;

    /// <summary>
    /// Store the active player in the game.
    /// </summary>
    private DontDestroyOnLoadScript activePlayer;

    private PickUp gun;

    /// <summary>
    /// Store the active GameManager.
    /// </summary>
    public static MainScript instance;

    private bool gamePaused;

    private void Awake()
    {
        // Check whether there is an instance
        // Check whether the instance is me
        if (instance != null && instance != this)
        {
            // If true, I'm not needed and can be destroyed.
            Destroy(gameObject);
        }
        // If not, set myself as the instance
        else
        {
            //Set the GameManager to not be destroyed when scenes are loaded.
            DontDestroyOnLoad(gameObject);

            // Subscribe the spawning function to the activeSceneChanged event.
            SceneManager.activeSceneChanged += SpawnPlayerOnLoad;

            // Set myself as the instance
            instance = this;
        }
    }

    /// <summary>
    /// Spawn the player when the scene changes
    /// </summary>
    /// <param name="currentScene"></param>
    /// <param name="nextScene"></param>
    void SpawnPlayerOnLoad(Scene currentScene, Scene nextScene)
    {
        if (nextScene.buildIndex == 0 || nextScene.buildIndex == 3 || nextScene.buildIndex == 4)
        {
            if (activePlayer != null)
            {
                Destroy(activePlayer.gameObject);
                activePlayer = null;
            }
            return;
        }


        PlayerSpawnSpot playerSpot = FindObjectOfType<PlayerSpawnSpot>();
        // Checking if there is any active player in the game.
        if (activePlayer == null)
        {
            // If there is no player, I should spawn one.
            GameObject newPlayer = Instantiate(playerPrefab, playerSpot.transform.position, Quaternion.identity);

            // Store the active player.
            activePlayer = newPlayer.GetComponent<DontDestroyOnLoadScript>();

            Debug.Log("active player + " + activePlayer != null);
            //activePlayer.SetUp();
        }
        // If there is already a player, position the player at the right spot.
        else
        {
            // Find the spawn spot

            // Position and rotate the player
            activePlayer.GetComponentInChildren<PlayerScript>().transform.position = playerSpot.transform.position;
            //activePlayer.transform = playerSpot.transform;
        }


        if (nextScene.buildIndex == 3)
        {
            //activePlayer.GetComponentInChildren<PlayerScript>().ActivateGun();
        }
    }

    public bool GamePaused()
    {
        return gamePaused;
    }

    public void TogglePause()
    {
        if (!gamePaused)
        {
            Time.timeScale = 0f;
            gamePaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            gamePaused = false;
        }
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}




