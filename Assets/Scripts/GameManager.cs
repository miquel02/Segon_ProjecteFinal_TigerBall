using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI heartsButtonText;
    public int lives = 10;
    public bool hasUsedHeartButton;
    public bool hasUsedHeart;

    public TextMeshProUGUI bombsButtonText;
    private int bombs = 5;
    public bool hasUsedBomb;
    
    public bool gameOver;
    public bool gameWin;

    public int level;

    private DragAndShoot dragAndShootScript;
    private PauseMenu pauseMenuScript;
    private ScenesManager scenesManagerScript;

    public GameObject ball;

    public Vector2 startPos;

    void Start()
    {
        dragAndShootScript = GameObject.Find("Ball").GetComponent<DragAndShoot>();
        pauseMenuScript = GameObject.Find("Pause Manager").GetComponent<PauseMenu>();
        scenesManagerScript = GameObject.Find("Game Manager").GetComponent<ScenesManager>();

        gameOver = false;
        hasUsedBomb = false;
        hasUsedHeartButton = false;
        level = 1;

        bombs = 5;
        lives = 10;
    }

    // Update is called once per frame
    void Update()
    {
        heartsButtonText.text = DataPersistance.PlayerStats.currentLives.ToString();
        bombsButtonText.text = DataPersistance.PlayerStats.currentBombs.ToString();

        if (DataPersistance.PlayerStats.currentLives < 0)
        {
            gameOver = true;
        }
        if (DataPersistance.PlayerStats.currentLives < 11)
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

        if(dragAndShootScript.rb.velocity.magnitude < 0.1f && !dragAndShootScript.canShoot && !hasUsedBomb)
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
        DataPersistance.PlayerStats.currentLives--;
        ball.transform.position = startPos;
        dragAndShootScript.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        hasUsedHeart = true;
    }
    public void LooseBomb()
    {
        DataPersistance.PlayerStats.currentBombs--;
        ball.transform.position = startPos;
        dragAndShootScript.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        hasUsedBomb = true;
        NextLevel();
    }

    public void NextLevel()
    { 
        DataPersistance.PlayerStats.currentLevel++;
        scenesManagerScript.NextLevel(DataPersistance.PlayerStats.currentLevel); 
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
        DataPersistance.PlayerStats.currentLevel = level;
        DataPersistance.PlayerStats.currentBombs = bombs;
        DataPersistance.PlayerStats.currentLives = lives;
    }
}
