using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;

    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        if(isPaused == false)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
    }
}
