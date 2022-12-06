using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MG_MainMenuManager : MonoBehaviour
{
    //Script to manage the main menu UI
    [SerializeField]private AudioSource buttonClikEffect;
    private MG_ScenesManager scenesManagerScript;

    void Start()
    {
        scenesManagerScript = GameObject.Find("Main Menu Manager").GetComponent<MG_ScenesManager>();//Acces the scenes manager script
        Time.timeScale = 1;//Make sure the game is not paused
    }

    public void PlayButton()//When we use the play button
    {
        buttonClikEffect.Play();//Sound
        scenesManagerScript.StartGame();//Acces function in scenes manager
    }
}
