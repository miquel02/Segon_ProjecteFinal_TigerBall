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
    public int bombs = 5;
    public bool hasUsedBomb;
    
    public bool gameOver;

    public int level;

    private DragAndShoot dragAndShootScript;
    private PauseMenu pasueMenuScript;

    public GameObject ball;

    public Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        dragAndShootScript = GameObject.Find("Ball").GetComponent<DragAndShoot>();
        pasueMenuScript = GameObject.Find("Game Manager").GetComponent<PauseMenu>();

        gameOver = false;
        hasUsedBomb = false;
        hasUsedHeartButton = false;
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

        if(dragAndShootScript.rb.velocity.magnitude < 0.1f && !dragAndShootScript.canShoot && !hasUsedBomb)
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
        }

    }

    public void HeartButton()
    {
        LooseLive();
        dragAndShootScript.canShoot = true;
        hasUsedHeartButton = true;
        //lives++; // Demanar maria per arrelgar

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
        dragAndShootScript.levelWon = false;
        Debug.Log(level);
        hasUsedHeart = true;
    }
}
