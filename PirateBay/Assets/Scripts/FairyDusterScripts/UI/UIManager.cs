/* UI MANAGEMENT */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour 
{
    //in Inpsector, drop-down the elements and match the button with the appropriate GameObject w/the tag "ShowOnPause"
    GameObject[] pausedUIObjects;
    //[SerializeField] private Scene m_ThisScene;


    //...............................................................
    //..................................................... * START *
	void Start () 
    {
        Time.timeScale = 1;

        pausedUIObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");

        HideWhenNotPaused();
	}
	
    //...............................................................
    //.................................................... * UPDATE *
	void Update () 
    {
		
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
        print(Time.timeScale);

        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;

            ShowOnPause();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;

            HideWhenNotPaused();
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
            i.SetActive(true);
        }
    }

    //...............................................................
    //....................................... * METHOD : LOAD LEVEL *
    //LOADING LEVEL
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }


}
