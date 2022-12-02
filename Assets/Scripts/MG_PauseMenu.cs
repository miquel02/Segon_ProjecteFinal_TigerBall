using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MG_PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;

    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    { 
        pausePanel.SetActive(!pausePanel.activeInHierarchy);

        if(isPaused == false)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
        }   
    }

}
