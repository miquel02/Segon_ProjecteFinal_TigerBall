using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MG_GameManager : MonoBehaviour
{

    public TextMeshProUGUI heartsButtonText;
    public int lives = 10;
    public bool hasUsedHeartButton;
    public bool hasUsedHeart;

    public TextMeshProUGUI bombsButtonText;
    private int bombs = 5;
    public bool hasUsedBomb;
    
    private bool gameOver;
    private bool gameWin;

    public int level;

    private MG_DragAndShoot dragAndShootScript;
    private MG_PauseMenu pauseMenuScript;
    private MG_ScenesManager scenesManagerScript;

    public GameObject ball;

    [SerializeField]private Vector2 startPos;

    public bool obstacleReset;

    void Start()
    {
        dragAndShootScript = GameObject.Find("Ball").GetComponent<MG_DragAndShoot>();
        pauseMenuScript = GameObject.Find("Pause Manager").GetComponent<MG_PauseMenu>();
        scenesManagerScript = GameObject.Find("Game Manager").GetComponent<MG_ScenesManager>();

        gameOver = false;
        hasUsedBomb = false;
        hasUsedHeartButton = false;
        level = 1;

        bombs = 5;
        lives = 10;

        gameWin = false;
        gameOver = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        heartsButtonText.text = MG_DataPersistance.PlayerStats.currentLives.ToString();
        bombsButtonText.text = MG_DataPersistance.PlayerStats.currentBombs.ToString();

        if (MG_DataPersistance.PlayerStats.currentLives < 0)
        {
            gameOver = true;
        }
        if (MG_DataPersistance.PlayerStats.currentLevel > 10)
        {
            gameWin = true;
        }

        if (gameOver)
        {
            scenesManagerScript.GameOver();
        }
        if (gameWin)
        {
            scenesManagerScript.GameVictory();
        }

        if(dragAndShootScript.rb.velocity.magnitude < 0.01f && !dragAndShootScript.canShoot && !hasUsedBomb)
        {
            LooseLive();
            dragAndShootScript.canShoot = true;
            dragAndShootScript.rb.constraints = RigidbodyConstraints2D.None;
        }

        if (hasUsedBomb)
        {
            dragAndShootScript.canShoot = true;
            dragAndShootScript.rb.constraints = RigidbodyConstraints2D.None;
            hasUsedBomb = false;
        }

        if (hasUsedHeart)
        {
            dragAndShootScript.canShoot = true;
         
            if (hasUsedHeartButton)
            {
                dragAndShootScript.rb.constraints = RigidbodyConstraints2D.None;
                hasUsedHeartButton = false;
            }
            hasUsedHeart = false;
        }
        if (dragAndShootScript.levelWon == true)
        {
            NextLevel();
            Debug.Log(dragAndShootScript.levelWon);
        }    
    }

    public void HeartButton()
    {
        LooseLive();
        dragAndShootScript.canShoot = true;
        hasUsedHeartButton = true;

    }

    public void BombsButton()
    {
        if (bombs < 1)
        {
            bombs = 0;
            return;
        }
        else
        {
            LooseBomb();
        }       
    }

    public void LooseLive()
    {
        MG_DataPersistance.PlayerStats.currentLives--;
        ball.transform.position = startPos;
        dragAndShootScript.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        hasUsedHeart = true;
        obstacleReset = true;
    }
    public void LooseBomb()
    {
        MG_DataPersistance.PlayerStats.currentBombs--;
        ball.transform.position = startPos;
        dragAndShootScript.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        hasUsedBomb = true;
        NextLevel();
    }

    public void NextLevel()
    { 
        MG_DataPersistance.PlayerStats.currentLevel++;
        scenesManagerScript.NextLevel(MG_DataPersistance.PlayerStats.currentLevel); 
        dragAndShootScript.levelWon = false;
    }

    public void RestartRun()
    {
        level = 1;
        lives = 10;
        bombs = 5;
        Debug.Log(level);
        pauseMenuScript.PauseGame();
        scenesManagerScript.NextLevel(level);
        MG_DataPersistance.PlayerStats.currentLevel = level;
        MG_DataPersistance.PlayerStats.currentBombs = bombs;
        MG_DataPersistance.PlayerStats.currentLives = lives;
    }
}
