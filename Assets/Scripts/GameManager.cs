using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI heartsButtonText;
    public int lives = 10;

    public TextMeshProUGUI bombsButtonText;
    public int bombs = 5;
    public bool hasUsedBomb;

    public bool gameOver;

    private DragAndShoot dragAndShootScript;

    public GameObject ball;

    public Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        dragAndShootScript = GameObject.Find("Ball").GetComponent<DragAndShoot>();

        gameOver = false;
        hasUsedBomb = false;
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

        if(dragAndShootScript.rb.velocity.magnitude < 0.1f && dragAndShootScript.canShoot == false)
        {
            if (hasUsedBomb)
            {
                LooseLive();
                //hasUsedBomb = true;
            }
            dragAndShootScript.canShoot = true;
        }

    }

    public void HeartButton()
    {
        LooseLive();       
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
        dragAndShootScript.rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        ball.transform.position = startPos;
    }
    public void LooseBomb()
    {
        bombs --;
        dragAndShootScript.rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        ball.transform.position = startPos;
        hasUsedBomb = true;
    }
}
