using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Button _start = null;
    [SerializeField] Button _exit = null;
    // Start is called before the first frame update

    public void startGame()
    {
        LevelNum._numOfLevel = 3;
        SceneManager.LoadScene(LevelNum._numOfLevel);
      }

    public void exitGame()
    {
        Application.Quit();
    }
}
