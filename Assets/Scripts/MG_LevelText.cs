using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MG_LevelText : MonoBehaviour
{

    public TextMeshProUGUI levelText;

    void Update()
    {
        levelText.text = MG_DataPersistance.PlayerStats.currentLevel.ToString();
    }
}
