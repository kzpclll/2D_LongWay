using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    
    private float lastEscapeTime = 0f;
    private float escapeInterval = 0.5f;

    private void Update()
    {
        QuitGame();
    }
    public void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            lastEscapeTime = Time.time;
            if (Time.time - lastEscapeTime < escapeInterval)
            {
                Application.Quit();
                Debug.Log("ÓÎÏ·½áÊø");
            }
        }
    }
}
