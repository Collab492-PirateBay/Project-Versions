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

    public bool hasGameStarted = false;
    [SerializeField]
    private GameObject endScreenUIObjects;


    //COUNTDOWN FOR GAME START
    private float m_CountdownTimer;
    [SerializeField]
    private float m_CountdownDur = 3.0f;
    public Text m_CountdownText;
    public bool m_GameHasStarted;


    

    private GameManager Game_manager;

    [HideInInspector]
    public int goldEarned = 0;
    public Text goldEarnedText,
                goldEarnedTextShadow,
                totalGoldText,
                totalGoldTextShadow,
                totalGoldAdded,
                tutorialText;
    public GameObject notifyText;


	void Start () 
    {
        
        //PAUSE UI 
        pausedUIObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");

        //GAME UI 
        gameplayUIObjects = GameObject.FindGameObjectsWithTag("HideOnPause");


        m_CountdownTimer = m_CountdownDur;
        m_GameHasStarted = false;
        m_GameHasEnded = false;

        goldEarnedText.text = "" + goldEarned;
        goldEarnedTextShadow.text = "" + goldEarned;

       
        Game_manager = GameManager.GameManagerInstance;
        totalGoldText.text = "" + Game_manager.goldEarnedLifetime;
        totalGoldTextShadow.text = "" + Game_manager.goldEarnedLifetime;

    }
	

	void Update () 
    {
        if (m_FairiesObtained >= m_FairiesNeededToWin)
        {
            if(!m_GameHasEnded)
                 StartCoroutine("EndTimer");
            m_GameHasEnded = true;
            
            m_CountdownTimer = m_CountdownDur;

            totalGoldAdded.text = "+ " + goldEarned;
            //ShowEndScreen();
           
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


    IEnumerator EndTimer()
    {
        StartCoroutine("goldObtained");
        yield return new WaitForSeconds(2.5f);
        m_CountdownText.text = "You Win!";
        yield return new WaitForSeconds(1.5f);
        //Game_manager.setCurrentScene("AB_Lobby");
        endScreenUIObjects.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Game_manager.goldEarnedLifetime += goldEarned;
        totalGoldText.text = "" + Game_manager.goldEarnedLifetime;
        totalGoldTextShadow.text = "" + Game_manager.goldEarnedLifetime;
    }

    IEnumerator tutorialTextDisplay()
    {
        tutorialText.text = "Good Job!";
        yield return new WaitForSeconds(2.5f);
        tutorialText.text = "Find the next Fairy Nest!";
        yield return new WaitForSeconds(2.5f);
        tutorialText.text = "";
    }

    public IEnumerator goldObtained()
    {
        notifyText.SetActive(true);
        if(!m_GameHasEnded)
            StartCoroutine("tutorialTextDisplay");
        yield return new WaitForSeconds(2.5f);
        notifyText.SetActive(false);
        goldEarned += 500;
        goldEarnedText.text = "" + goldEarned;
        goldEarnedTextShadow.text = "" + goldEarned;
    }


}
