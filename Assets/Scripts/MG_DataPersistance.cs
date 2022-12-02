using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DataPersistance : MonoBehaviour
{
    public int currentLevel;
    public int currentBombs;
    public int currentLives;

    private MG_MainMenuManager mainMenuManagerScript;
    private MG_GameManager gameManagerScript;


    public static MG_DataPersistance PlayerStats;

    void Awake()
    {
        // Si la instancia no existe
        if (PlayerStats == null)
        {
            // Configuramos la instancia
            PlayerStats = this;
            // Nos aseguramos de que no sea destruida con el cambio de escena
            DontDestroyOnLoad(PlayerStats);
        }
        else
        {
            // Como ya existe una instancia, destruimos la copia
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mainMenuManagerScript = GameObject.Find("Main Menu Manager").GetComponent<MG_MainMenuManager>();
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<MG_GameManager>();


        currentLevel = PlayerPrefs.GetInt("LEVELS");
        currentBombs = PlayerPrefs.GetInt("BOMBS");
        currentLives = PlayerPrefs.GetInt("LIVES");
    }

    // Update is called once per frame
    public void SaveStats()
    {
        PlayerPrefs.SetInt("LEVELS", currentLevel);
        PlayerPrefs.SetInt("BOMBS", currentBombs);
        PlayerPrefs.SetInt("LIVES", currentLives);

        if (mainMenuManagerScript.gameStarted)
        {
            currentLevel = 1;
            currentBombs = 5;
            currentLives = 10;
        }
    }
}
