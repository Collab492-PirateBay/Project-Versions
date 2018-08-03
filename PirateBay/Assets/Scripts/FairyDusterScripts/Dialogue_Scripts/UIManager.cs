using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
    //in Inpsector, drop-down the elements and match the button with the appropriate GameObject w/the tag "ShowOnPause"
    private GameObject[] pausedUIObjects;

    private GameObject[] gameplayUIObjects;

    //GAME REQUIREMENTS
    [SerializeField] 
    private int m_FairiesNeededToWin = 3;
    public int m_FairiesObtained = 0;
    public Text m_GameReqText;

    public bool m_GameHasEnded;


    //START & END SCREEN UI
    [HideInInspector]
    public bool hasGameStarted = false;

    private GameObject[] endScreenUIObjects;


    //COUNTDOWN FOR GAME START
    private float m_CountdownTimer;
    private float m_CountdownDur = 3.0f;
    public Text m_CountdownText;
    public bool m_GameHasStarted;


    

    private GameManager Game_manager;

    //public JarOrbit jarsLoot;
    //private float goldEarned;
    //public Text goldEarnedText;


	void Start () 
    {
        
        //PAUSE UI 
        pausedUIObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");

        //GAME UI 
        gameplayUIObjects = GameObject.FindGameObjectsWithTag("HideOnPause");


        //VICTORY UI 
        endScreenUIObjects = GameObject.FindGameObjectsWithTag("EndScreenElements");


        m_GameHasStarted = false;
        m_GameHasEnded = false;

        Game_manager = GameManager.GameManagerInstance;
	}
	

	void Update () 
    {
        if (m_FairiesObtained == m_FairiesNeededToWin)
        {
            m_GameHasEnded = true;

            m_CountdownTimer = m_CountdownDur;
            m_CountdownText.text = "END!";

            ShowEndScreen();
            StartCoroutine(EndTimer());
        }

        //.............................................. * COUNTDOWN TIMER to begin game *
        if (hasGameStarted == true)
        {
            
            m_CountdownTimer -= Time.deltaTime;

            if (m_CountdownTimer <= 0)
            {
                m_GameHasStarted = true;
                
            }
        }

        if (m_GameHasEnded == false)
        {
            if ((m_CountdownTimer >= 1))
            {
                m_CountdownText.text = (m_CountdownTimer % 60).ToString("00");
            }
            else
                if ((m_CountdownTimer < 1) && (m_CountdownTimer >= 0))
            {
                m_CountdownText.text = "GO!";
            }
            else
                if (m_CountdownTimer < 0)
            {
                m_CountdownText.text = "";
            }
        }

        //SCORE DISPLAY
        m_GameReqText.text = (m_FairiesObtained + " / " + m_FairiesNeededToWin);
	}

    public void GameStart()
    {
        hasGameStarted = true;
    }

    //PAUSE THE GAME
    public void GamePause()
    {
        if (Time.timeScale == 1)
        {
            Game_manager.CurrentState = GameManager.GameState.Pause;

            foreach (GameObject i in pausedUIObjects)
            {
            i.SetActive(true);
            }

            foreach (GameObject i in gameplayUIObjects)
            {
            i.SetActive(false);
            }
 
        }
        else if (Time.timeScale == 0)
        {
            Game_manager.CurrentState = GameManager.GameState.Game;

            foreach (GameObject i in pausedUIObjects)
            {
            i.SetActive(false);
            }

            foreach (GameObject i in gameplayUIObjects)
            {
            i.SetActive(true);
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

    IEnumerator EndTimer()
    {
        yield return new WaitForSeconds(5.0f);
        Game_manager.setCurrentScene("AB_Lobby");
    }



}
