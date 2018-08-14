/* UI CONTROLLER */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SharkDrop_UIController : MonoBehaviour 
{


    ////...............................................................
    //// VARIABLES

    //GameObject[] startScreenUIObjects;
    //GameObject[] dialogueUIObjects;

    //GameObject[] instructionScreenUIObjects;

    //GameObject[] gameplayUIObjects;
    //GameObject[] pausedUIObjects;

    //GameObject[] victoryUIObjects;
    //GameObject[] endScreenUIObjects;

    ////...............................................................
    //// RELATIVE SCRIPTS
    //public Collector m_CollectorReference;

    ////...............................................................
    ////..................................................... * START *

    //void Start()
    //{
    //    Time.timeScale = 1;

    //    startScreenUIObjects = GameObject.FindGameObjectsWithTag("StartScreenElements");

    //    gameplayUIObjects = GameObject.FindGameObjectsWithTag("HideWhenPaused");

    //    pausedUIObjects = GameObject.FindGameObjectsWithTag("ShowWhenPaused");

    //    endScreenUIObjects = GameObject.FindGameObjectsWithTag("EndScreenElements");

    //    ShowWhenPlaying();

    //    HideDuringGameplay();


    //    GameObject collectorObject = GameObject.FindGameObjectWithTag("Collector");
    //    m_CollectorReference = collectorObject.GetComponent<Collector>();
    //}

    ////...............................................................
    ////.................................................... * UPDATE *

    //void Update()
    //{
    //    if (m_CollectorReference.m_GameIsOver == true)
    //    {
    //        HideOnStartScreen();
    //        HideWhenPaused();
    //        HideDuringGameplay();

    //        ShowOnEndScreen();
    //    }
    //}

    ////...............................................................
    ////.......................................... * METHOD : RESTART *

    //public void GameRestart()
    //{
    //    Application.LoadLevel(Application.loadedLevel);
    //}

    ////...............................................................
    ////....................................... * METHOD : LOAD LEVEL *

    //public void LoadLevel(string level)
    //{
    //    SceneManager.LoadScene(level);
    //}

    ////...............................................................
    ////............................................ * METHOD : PAUSE *

    //public void GamePause()
    //{
    //    if (Time.timeScale == 1)
    //    {
    //        Time.timeScale = 0;

    //        if (m_CollectorReference.m_GameIsOver == false)
    //        {
    //            ShowOnPause();
    //        }

    //        HideWhenPaused();

    //    }
    //    else if (Time.timeScale == 0)
    //    {
    //        Time.timeScale = 1;

    //        ShowWhenPlaying();

    //        HideDuringGameplay();
    //    }
    //}

    ////...............................................................
    ////......................... * METHOD : WHAT TO SHOW WHEN PAUSED *

    //public void ShowOnPause()
    //{
    //    foreach (GameObject i in pausedUIObjects)
    //    {
    //        i.SetActive(true);
    //    }
    //}

    ////...............................................................
    ////........................ * METHOD : WHAT TO SHOW WHEN PLAYING *

    //public void HideDuringGameplay()
    //{
    //    foreach (GameObject i in pausedUIObjects)
    //    {
    //        i.SetActive(false);
    //    }
    //}

   

    ////...............................................................
    ////................ * METHOD : ELEMENTS VISIBLE DURINIG GAMEPLAY *

    //public void ShowWhenPlaying()
    //{
    //    foreach (GameObject i in gameplayUIObjects)
    //    {
    //        i.SetActive(true);
    //    }
    //}

    ////...............................................................
    ////......................... * METHOD : WHAT TO HIDE WHEN PAUSED *

    //public void HideWhenPaused()
    //{
    //    foreach (GameObject i in gameplayUIObjects)
    //    {
    //        i.SetActive(false);
    //    }
    //}

    ////...............................................................
    ////............................. * METHOD : SHOW ON START SCREEN *

    //public void ShowOnStartScreen()
    //{
    //    foreach (GameObject i in startScreenUIObjects)
    //    {
    //        i.SetActive(true);
    //    }
    //}

    ////...............................................................
    ////............................. * METHOD : HIDE ON START SCREEN *

    //public void HideOnStartScreen()
    //{
    //    foreach (GameObject i in startScreenUIObjects)
    //    {
    //        i.SetActive(false);
    //    }
    //}

    ////...............................................................
    ////............................... * METHOD : SHOW ON END SCREEN *

    //public void ShowOnEndScreen()
    //{
    //    foreach (GameObject i in endScreenUIObjects)
    //    {
    //        i.SetActive(true);
    //    }
    //}

    ////...............................................................
    ////............................... * METHOD : HIDE ON END SCREEN *

    //public void HideOnEndScreen()
    //{
    //    foreach (GameObject i in endScreenUIObjects)
    //    {
    //        i.SetActive(false);
    //    }
    //}
}
