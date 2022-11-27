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

        if(dragAndShootScript.rb.velocity.magnitude < 0.1f && dragAndShootScript.canShoot == false && !hasUsedBomb)
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

    }

    public void HeartButton()
    {
        dragAndShootScript.canShoot = true;
        lives++; // Demanar maria per arrelgar
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
        ball.transform.position = startPos;
        dragAndShootScript.rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    public void LooseBomb()
    {
        bombs --; 
        ball.transform.position = startPos;
        dragAndShootScript.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        hasUsedBomb = true;
    }
}
