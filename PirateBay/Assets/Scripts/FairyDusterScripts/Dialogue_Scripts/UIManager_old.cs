/* UI MANAGEMENT */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager_old : MonoBehaviour 
{
    //in Inpsector, drop-down the elements and match the button with the appropriate GameObject w/the tag "ShowOnPause"
    GameObject[] pausedUIObjects;

    GameObject[] gameplayUIObjects;


    //START & END SCREEN UI
    public bool hasGameStarted = false;

    GameObject[] startScreenUIObjects;
    GameObject[] endScreenUIObjects;

    //public PlayerMovement playerFinished;

    //public JarOrbit jarsLoot;
    //private float goldEarned;
    //public Text goldEarnedText;


    //...............................................................
    //..................................................... * START *
	void Start () 
    {
        Time.timeScale = 1;

        //..................................
        //UI that appears when game is paused (the Pause Menu)
        pausedUIObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");

        HideWhenNotPaused();


        //................................
        //GAME UI that disappears when game is paused
        gameplayUIObjects = GameObject.FindGameObjectsWithTag("HideOnPause");

        //................................
        //MENU UI for start screen and end-of-game screen
        startScreenUIObjects = GameObject.FindGameObjectsWithTag("StartScreenElements");

        //................................
        //MENU UI for start screen and end-of-game screen
        endScreenUIObjects = GameObject.FindGameObjectsWithTag("EndScreenElements");

        ShowWhenPlaying();

        ////FINDING OUT IF PLAYER HAS MET WIN CONDITION
        //GameObject m_PlayerFinishedObject = GameObject.FindGameObjectWithTag("Player");
        //playerFinished = m_PlayerFinishedObject.GetComponent<PlayerMovement>();

        ////FINDING OUT WHAT PLAYER WON
        //GameObject rewardObject = GameObject.FindGameObjectWithTag("Jar");
        //jarsLoot = rewardObject.GetComponent<JarOrbit>();
	}
	
    //...............................................................
    //.................................................... * UPDATE *
	void Update () 
    {
        //if (playerFinished.m_GameHasEnded == true)
        //{
        //    GamePause();
        //}

	}

    //...............................................................
    //.......................................... * METHOD : RESTART *
    //RESTART
    public void GameRestart()
    {
        //SceneManager.LoadScene(LoadLevel(SceneManager.LoadScene(LoadLevel(m_ThisScene))));
        Application.LoadLevel(Application.loadedLevel);
    }

    //...............................................................
    //............................................ * METHOD : PAUSE *
    //PAUSE
    public void GamePause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;

            //if (playerFinished.m_GameHasEnded == false)
           // {
                ShowOnPause();

                HideWhenPaused();

               // HideEndScreen();

            //}
            //else if (playerFinished.m_GameHasEnded == true)
            //{
            //    goldEarned = jarsLoot.m_TotalScore;
            //    goldEarnedText = GameObject.Find("GoldEarnedText").GetComponent<Text>();
            //    goldEarnedText.text = "+" + goldEarned;


            //    ShowEndScreen();
            //}
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;

            HideWhenNotPaused();

            ShowWhenPlaying();
        }
    }

    //...............................................................
    //......................... * METHOD : WHAT TO SHOW WHEN PAUSED *
    //ELEMENTS SHOWN ON PAUSE
    public void ShowOnPause()
    {
        foreach (GameObject i in pausedUIObjects)
        {
            i.SetActive(true);
        }
    }

    //...............................................................
    //........................ * METHOD : WHAT TO SHOW WHEN PLAYING *
    //ELEMENTS HIDDEN WHEN NOT PAUSED
    public void HideWhenNotPaused()
    {
        foreach (GameObject i in pausedUIObjects)
        {
            i.SetActive(false);
        }
    }

    //...............................................................
    //....................................... * METHOD : LOAD LEVEL *
    //LOADING LEVEL
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }


    //OPPOSITE
    //...............................................................
    //......................... * METHOD : WHAT TO SHOW WHEN PLAYING *
    //ELEMENTS SHOWN ON PAUSE
    public void ShowWhenPlaying()
    {
        foreach (GameObject i in gameplayUIObjects)
        {
            i.SetActive(true);
        }
    }

    //...............................................................
    //......................... * METHOD : WHAT TO HIDE WHEN PAUSED *
    //ELEMENTS HIDDEN WHEN NOT PAUSED
    public void HideWhenPaused()
    {
        foreach (GameObject i in gameplayUIObjects)
        {
            i.SetActive(false);
        }
    }




    //...............................................................
    //............................. * METHOD : SHOW ON START SCREEN *
    public void ShowStartScreen()
    {
        hasGameStarted = false;

        foreach (GameObject i in startScreenUIObjects)
        {
            i.SetActive(true);
        }
    }

    //...............................................................
    //................................ * METHOD : HIDE START SCREEN *
    public void HideStartScreen()
    {
        foreach (GameObject i in startScreenUIObjects)
        {
            if (hasGameStarted == false)
            {
                //REMOVE THIS ONCE YOU PUT THE DIALOGUE IN
                hasGameStarted = true;
                i.SetActive(false);
            }
        }
    }


    //...............................................................
    //............................... * METHOD : SHOW ON END SCREEN *
    public void ShowEndScreen()
    {
        foreach (GameObject i in endScreenUIObjects)
        {
            i.SetActive(true);
        }
    }

    //...............................................................
    //.................................. * METHOD : HIDE END SCREEN *
    public void HideEndScreen()
    {
        foreach (GameObject i in endScreenUIObjects)
        {
            i.SetActive(false);
        }
    }



}
