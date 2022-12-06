using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MG_GameManager : MonoBehaviour
{
    //Script to manage the game
    //Lives
    public TextMeshProUGUI heartsButtonText;
    public bool hasUsedHeartButton;
    public bool hasUsedHeart;
    //Bombs
    public TextMeshProUGUI bombsButtonText;
    private int bombs = 5;
    public bool hasUsedBomb;
    //Variables to change game state
    private bool gameOver;
    private bool gameWin;
    //We acces other scripts
    private MG_DragAndShoot dragAndShootScript;
    private MG_PauseMenu pauseMenuScript;
    private MG_ScenesManager scenesManagerScript;
    //We acces the player
    public GameObject ball;
    //Variable to set start pos of each level
    [SerializeField]private Vector2 startPos;
    //Variables to reset obstacles
    public bool obstacleReset;
    //Sounds
    public AudioSource WinSoundEffect;
    public AudioSource ButtonSoundEffect;
    //Particles
    public ParticleSystem winParticles;

    void Start()
    {
        //Acces other scripts
        dragAndShootScript = GameObject.Find("Ball").GetComponent<MG_DragAndShoot>();
        pauseMenuScript = GameObject.Find("Pause Manager").GetComponent<MG_PauseMenu>();
        scenesManagerScript = GameObject.Find("Game Manager").GetComponent<MG_ScenesManager>();

        gameOver = false;
        hasUsedBomb = false;
        hasUsedHeartButton = false;
        gameWin = false;       
    }

    // Update is called once per frame
    void Update()
    {
        heartsButtonText.text = MG_DataPersistance.PlayerStats.currentLives.ToString();//Show the current number of lives
        bombsButtonText.text = MG_DataPersistance.PlayerStats.currentBombs.ToString();//Show the current number of lives

        if (MG_DataPersistance.PlayerStats.currentLives < 0)
        {
            gameOver = true;//When lives reach below 0 we loose
        }
        if (MG_DataPersistance.PlayerStats.currentLevel > 10)
        {
            gameWin = true;// if we reach max level we win
        }
        if (gameOver)
        {
            scenesManagerScript.GameOver();//when game over change scenes
        }
        if (gameWin)
        {
            scenesManagerScript.GameVictory();//when game win change scenes
        }

        if(dragAndShootScript.rb.velocity.magnitude < 0.01f && !dragAndShootScript.canShoot && !hasUsedBomb)//We reset ball and loose live when we stop moving
        {
            LooseLive();
            dragAndShootScript.canShoot = true;
            dragAndShootScript.rb.constraints = RigidbodyConstraints2D.None;
        }

        if (hasUsedBomb)//if we used bomb allow to shoot again
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
            NextLevel();//Wen we win a level change level
        }    
    }

    public void HeartButton()//We use heart button
    {
        LooseLive();
        dragAndShootScript.canShoot = true;
        hasUsedHeartButton = true;
        ButtonSoundEffect.Play();

    }

    public void BombsButton()//We use bombs button
    {
        if (MG_DataPersistance.PlayerStats.currentBombs < 1)//only loose bomb if we have bombs
        {
            MG_DataPersistance.PlayerStats.currentBombs = 0;
            return;
        }
        else
        {
            LooseBomb();
            ButtonSoundEffect.Play();
        }       
    }

    public void LooseLive()// We loose a live
    {
        MG_DataPersistance.PlayerStats.currentLives--;
        ball.transform.position = startPos;
        dragAndShootScript.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        hasUsedHeart = true;
        obstacleReset = true;
    }
    public void LooseBomb()// we loose a bomb
    {
        MG_DataPersistance.PlayerStats.currentBombs--;
        ball.transform.position = startPos;
        dragAndShootScript.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        hasUsedBomb = true;
        NextLevel();
    }

    public void NextLevel()//next level
    { 
        dragAndShootScript.levelWon = false;
        StartCoroutine(ChangeLevel());
    }

    public void RestartRun()//restart the run
    {
        pauseMenuScript.PauseGame();//Unpause the game
        
        MG_DataPersistance.PlayerStats.currentLevel = 1;//Set the correct value
        MG_DataPersistance.PlayerStats.currentBombs = 5; ;//Set the correct value
        MG_DataPersistance.PlayerStats.currentLives = 10; ;//Set the correct value
        scenesManagerScript.NextLevel(MG_DataPersistance.PlayerStats.currentLevel);//Change scnene
    }

    private IEnumerator ChangeLevel()//Courtine to allow sound and particles to play before level change
    {
        WinSoundEffect.Play();
        winParticles.Play();
        yield return new WaitForSeconds(0.5f);
        
        MG_DataPersistance.PlayerStats.currentLevel++;
        scenesManagerScript.NextLevel(MG_DataPersistance.PlayerStats.currentLevel);
    }
}
