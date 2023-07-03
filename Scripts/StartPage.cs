using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;


public class StartPage : MonoBehaviour
{
    


    //Texts in UI
    public TextMeshProUGUI playGame;
    public TextMeshProUGUI confirmGame;
    public TextMeshProUGUI optionsGame;
    public TextMeshProUGUI yesGame;
    public TextMeshProUGUI noGame;
    public TextMeshProUGUI audioGame;
    public TextMeshProUGUI changeAudioGame;
    public TextMeshProUGUI backGameO;
    public TextMeshProUGUI creditsText;
    public TextMeshProUGUI backGameC;
    public TextMeshProUGUI exitText;
    public TextMeshProUGUI yesExit;
    public TextMeshProUGUI noExit;
    public TextMeshProUGUI yesConfirmExitMessage;
    public TextMeshProUGUI backGameE;


    private void OnView()
    {
        playGame.text = "Play";
        confirmGame.text = "Are you sure you want to play?";
        optionsGame.text = "Options";
        noGame.text = "No";
        yesGame.text = "Yes";
        audioGame.text = "Adjust your audio here";
        changeAudioGame.text = "Change audio";
        backGameO.text = "Back";
        creditsText.text = "Made by Bhoomika manot 2023 Sound credits Sample Audio Pack";
        backGameC.text = "Back";
        exitText.text = "Are you sure you want to exit?";
        yesExit.text = "Yes";
        noExit.text = "No";
        yesConfirmExitMessage.text = "You have exited the game. Press Back to return to game menu.";
        backGameE.text = "Back";

    }


    //To use to mute volume
    public void MuteToggle(bool muted)
    {
        if (muted)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }

    //To create class names for the mixer and slider and exit game
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;

    //Checks if player has player before, if has, adjusts sound to what was left before. If player has not, Volume is turned to 1
    private void Start()
    {

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }

    }

    //To adjust the volume on slider
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    

    //To retrieve data on the sound the player adjusted to playing the previous time
    private void Load()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume();
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

}
