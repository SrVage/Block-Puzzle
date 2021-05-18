using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RetryLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public void retryLevel()
    {
        SceneManager.LoadScene(LevelNum._numOfLevel);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
