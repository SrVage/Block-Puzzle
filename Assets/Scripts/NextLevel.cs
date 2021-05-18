using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] Button _next = null;
    [SerializeField] Button _exit = null;
    [SerializeField] Text _complete = null;
    // Start is called before the first frame update
    private void Awake()
    {
        _complete.text = "Level " + (LevelNum._numOfLevel - 2) + " complete";
    }

    public void nextLevel()
    {
        LevelNum._numOfLevel++;
        //GameObject.Find("Level").GetComponent<LevelNum>()._numOfLevel=3;
        SceneManager.LoadScene(LevelNum._numOfLevel);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
