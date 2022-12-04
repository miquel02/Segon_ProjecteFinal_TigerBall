using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_PlaceSquares : MonoBehaviour
{
    public GameObject obstacles;

    private MG_GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<MG_GameManager>();
        Instantiate(obstacles);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.obstacleReset)
        {
            Destroy(GameObject.FindWithTag("Obstacles"));
            Instantiate(obstacles);
            gameManagerScript.obstacleReset = false;
        }
    }
}
