using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    private LevelManager LevelManagerScript;


    private void Start()
    {
        LevelManagerScript = gameObject.GetComponent<LevelManager>();
        startSetting();
    }

    private void startSetting()
    {
        LevelManagerScript.startGame();
    }
    
}
