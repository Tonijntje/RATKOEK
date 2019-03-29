using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{

    public float timeLeft = 60.0f;
    public bool isGameOver = false;

    public GameObject youWinObj;
    public GameObject gameOverObj;
    public GameObject countDownObj;
    public GameObject tijdWegObj;

    private string displayTimeLeft, oldDisplayTimeLeft = "";

    void Start()
    {
        //youWinObj = GameObject.FindWithTag("youwin");
        //gameOverObj = GameObject.FindWithTag("gameover");
        //countDownObj = GameObject.FindWithTag("countdown");
        //tijdWegObj = GameObject.FindWithTag("tijdweg");
        gameOverObj.SetActive(false);
        youWinObj.SetActive(false);
        tijdWegObj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            timeLeft -= Time.deltaTime;
            displayTimeLeft = System.Math.Round(timeLeft, 0).ToString("F0");
            if (displayTimeLeft != oldDisplayTimeLeft)
            {
                countDownObj.GetComponent<Text>().text = displayTimeLeft;
                oldDisplayTimeLeft = displayTimeLeft;
            }

            if (timeLeft < 0)
            {
                if (!isGameOver)
                {
                    GameOver(); // call once

                }

            }
        }
        else //gameover, test for keys
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                SceneManager.LoadScene("hellyea");
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                //SceneManager.LoadScene("menuscene");
                Application.Quit();
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
        gameOverObj.SetActive(true);
        tijdWegObj.SetActive(false);
        isGameOver = true;
    }

    public void YouWin()
    {
        Debug.Log("YOU WIN!");
        youWinObj.SetActive(true);
        tijdWegObj.SetActive(false);
        isGameOver = true;
    }
}
