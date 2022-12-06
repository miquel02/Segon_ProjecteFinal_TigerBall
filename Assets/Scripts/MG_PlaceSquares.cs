using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_PlaceSquares : MonoBehaviour
{
    //Script to replace the moved obstacles with new ones once the player restarts the level

    [SerializeField]private GameObject obstacles;

    private MG_GameManager gameManagerScript;

    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<MG_GameManager>();//We acces the game manager script to know when we need to respawn them
        Instantiate(obstacles);// Instantiate them when the scene starts
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.obstacleReset)//If the variables obstacles reset activates 
        {
            Destroy(GameObject.FindWithTag("Obstacles"));//Destroy the current obstacles
            Instantiate(obstacles);//Instantiate new ones
            gameManagerScript.obstacleReset = false;//Change the value pf the variable
        }
    }
}
