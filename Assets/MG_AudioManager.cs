using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_AudioManager : MonoBehaviour
{
    //Script to keep the music playing through
    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if(musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
