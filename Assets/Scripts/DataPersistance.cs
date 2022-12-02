using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistance : MonoBehaviour
{
    public int currentLevel;
    public int currentBombs;
    public int currentLives;

    private MainMenuManager mainMenuManagerScript;
    private GameManager gameManagerScript;


    public static DataPersistance PlayerStats;

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
        mainMenuManagerScript = GameObject.Find("Main Menu Manager").GetComponent<MainMenuManager>();
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();


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
