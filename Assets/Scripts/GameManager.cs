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

    public int level;
    public int currentLevel;

    private DragAndShoot dragAndShootScript;
    private PauseMenu pauseMenuScript;
    private ScenesManager scenesManagerScript;

    public GameObject ball;

    public Vector2 startPos;

    // Start is called before the first frame update
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

        level = PlayerPrefs.GetInt("LEVELS");
        bombs = PlayerPrefs.GetInt("BOMBS");
        lives = PlayerPrefs.GetInt("LIVES");
    }

    // Update is called once per frame
    void Update()
    {
        heartsButtonText.text = lives.ToString();
        bombsButtonText.text = bombs.ToString();

        if (lives == 0)
        {
            gameOver = true;
        }

        if(dragAndShootScript.rb.velocity.magnitude < 0.1f && !dragAndShootScript.canShoot && !hasUsedBomb/* && dragAndShootScript.justShot*/)
        {
            LooseLive();
            dragAndShootScript.canShoot = true;
            dragAndShootScript.rb.constraints = RigidbodyConstraints2D.None;
            //pasueMenuScript.usingPause = false;
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

        

        PlayerPrefs.SetInt("LEVELS", level);
        PlayerPrefs.SetInt("BOMBS", bombs);
        PlayerPrefs.SetInt("LIVES", lives);

        
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
        lives--;
        ball.transform.position = startPos;
        dragAndShootScript.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        hasUsedHeart = true;
    }
    public void LooseBomb()
    {
        bombs --; 
        ball.transform.position = startPos;
        dragAndShootScript.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        hasUsedBomb = true;
        NextLevel();
    }

    public void NextLevel()
    {
        level++;
        scenesManagerScript.NextLevel(level);
        //Debug.Log(level);
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
    }
}
