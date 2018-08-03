/* SCENE MANAGER */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagement_SD : MonoBehaviour
{
    //........................................................
    // SCENES

    public bool m_StartScreenActive = true;
    public bool m_DialogueActive = false;
    public bool m_InstructionsScreenActive = false;
    public bool m_CalibrationActive = false;
    public bool m_CountdownActive = false;
    public bool m_GameplayActive = false;
    public bool m_PauseScreenActive = false;
    public bool m_VictoryScreenActive = false;
    public bool m_ResultsScreenActive = false;

    public int m_OrderInScene;

    // BUTTONS
    public GameObject startScreen_BGSplash = null;
    public GameObject startScreen_LobbyButton = null;
    public GameObject startScreen_TapAnywhereImage = null;
    public GameObject startScreen_LogoImage = null;

    public GameObject instructionScreen_BGSplash = null;
    public GameObject instructionScreen_BackButton = null;
    public GameObject instructionScreen_PlayButton = null;
    public GameObject instructionScreen_TextBox = null;


    public GameObject gameplayScreen_PauseButton = null;
    public GameObject gameplayScreen_ScoreBox = null;

    public GameObject pauseScreen_PlayButton = null;
    public GameObject pauseScreen_ReplayButton = null;
    public GameObject pauseScreen_LobbyButton = null;

    public GameObject resultsScreen_ReplayButton = null;
    public GameObject resultsScreen_LobbyButton = null;
    public GameObject resultsScreen_GoldImageBox = null;
    public Text m_GoldText;
    public GameObject menuButton = null;

    public Text middleScreen_Text;

    //.....
    // DIALOGUE

    //[SerializeField] private GameObject m_DialogueBox = null;
    //public Text m_DialogueText;

    //[SerializeField] private GameObject m_NPCavatar = null;
    //public string m_NPCName;
    //public Text m_NPCNameText;

    //[SerializeField] private GameObject m_NextIndicator = null;


    // RELATIVE SCRIPTS

    public Collector m_CollectorReading;

    //............................................................
    //.................................................. * START *

    void Start()
    {
        m_OrderInScene = 1;

        GameObject collectorObject = GameObject.FindGameObjectWithTag("Collector");
        m_CollectorReading = collectorObject.GetComponent<Collector>();
    }

    //............................................................
    //................................................. * UPDATE *

    void Update()
    {
        // START SCREEN
        if (m_OrderInScene == 1)
        {
            m_StartScreenActive = true;
            middleScreen_Text.text = "";
            startScreen_BGSplash.SetActive(true);
            startScreen_LobbyButton.SetActive(true);
            startScreen_TapAnywhereImage.SetActive(true);
            startScreen_LogoImage.SetActive(true);
        }
        else if (m_OrderInScene != 1)
        {
            m_StartScreenActive = false;
            startScreen_BGSplash.SetActive(false);
            startScreen_LobbyButton.SetActive(false);
            startScreen_TapAnywhereImage.SetActive(false);
            startScreen_LogoImage.SetActive(false);
        }

        // DIALOGUE SEGMENT
        if (m_OrderInScene == 2)
        {
            m_DialogueActive = true;
            instructionScreen_BGSplash.SetActive(true);
            middleScreen_Text.text = "";
        }
        else if (m_OrderInScene != 2)
        {
            m_DialogueActive = false;
        }

        // INSTRUCTIONS SCREEN
        if (m_OrderInScene == 3)
        {
            m_InstructionsScreenActive = true;
            instructionScreen_BGSplash.SetActive(true);
            middleScreen_Text.text = "";
            instructionScreen_BackButton.SetActive(true);
            instructionScreen_PlayButton.SetActive(true);
            instructionScreen_TextBox.SetActive(true);
        }
        else if (m_OrderInScene != 3)
        {
            m_InstructionsScreenActive = false;
            instructionScreen_BackButton.SetActive(false);
            instructionScreen_PlayButton.SetActive(false);
            instructionScreen_TextBox.SetActive(false);
        }

        // CALIBRATION SEGMENT
        if (m_OrderInScene == 4)
        {
            m_CalibrationActive = true;
            middleScreen_Text.text = "Keep Steady!";
        }
        else if (m_OrderInScene != 4)
        {
            m_CalibrationActive = false;
        }

        // COUNTDOWN SEGMENT
        if (m_OrderInScene == 5)
        {
            m_CountdownActive = true;
        }
        else if (m_OrderInScene != 5)
        {
            m_CountdownActive = false;
        }

        // MAIN GAMEPLAY
        if (m_OrderInScene == 6)
        {
            m_GameplayActive = true;
            instructionScreen_BGSplash.SetActive(false);
            middleScreen_Text.text = "";
            gameplayScreen_PauseButton.SetActive(true);
            gameplayScreen_ScoreBox.SetActive(true);
        }
        else if (m_OrderInScene != 6)
        {
            m_GameplayActive = false;
            gameplayScreen_PauseButton.SetActive(false);
            gameplayScreen_ScoreBox.SetActive(false);
        }

        // PAUSE SCREEN
        if (m_OrderInScene == 7)
        {
            m_PauseScreenActive = true;
            middleScreen_Text.text = "P A U S E";
            pauseScreen_PlayButton.SetActive(true);
            pauseScreen_ReplayButton.SetActive(true);
            pauseScreen_LobbyButton.SetActive(true);
        }
        else if (m_OrderInScene != 7)
        {
            m_PauseScreenActive = false;
            pauseScreen_PlayButton.SetActive(false);
            pauseScreen_ReplayButton.SetActive(false);
            pauseScreen_LobbyButton.SetActive(false);
        }

        // VICTORY
        if (m_OrderInScene == 8)
        {
            m_VictoryScreenActive = true;
            middleScreen_Text.text = "Y O U  W I N !";
        }
        else if (m_OrderInScene != 8)
        {
            m_VictoryScreenActive = false;
        }

         //RESULTS
        if (m_OrderInScene == 9)
        {
            m_ResultsScreenActive = true;
            instructionScreen_BGSplash.SetActive(true);
            middleScreen_Text.text = "";
            resultsScreen_LobbyButton.SetActive(true);
            resultsScreen_ReplayButton.SetActive(true);
            startScreen_BGSplash.SetActive(true);
            resultsScreen_GoldImageBox.SetActive(true);
            menuButton.SetActive(true);
            m_GoldText.text = "+200";
        }
        else if (m_OrderInScene != 9)
        {
            m_ResultsScreenActive = false;
            resultsScreen_LobbyButton.SetActive(false);
            resultsScreen_ReplayButton.SetActive(false);
            resultsScreen_GoldImageBox.SetActive(false);
            m_GoldText.text = "";
            menuButton.SetActive(false);
        }

        if (m_CollectorReading.m_GameIsOver == true)
        {
            m_OrderInScene = 9;
        }

    }

    //............................................................
    //................................................... * NEXT *

    public void ProgressClick()
    {
        m_OrderInScene += 1;
    }

    //............................................................
    //................................................... * BACK *

    public void BackClick()
    {
        m_OrderInScene -= 1;
    }

    //............................................................
    //................................................ * JUMP TO *

    public void JumpClick(int orderNumber)
    {
        m_OrderInScene = orderNumber;
    }

    //............................................................
    //....................................... * TRIGGER DIALOGUE *

    public void TriggerDialogue()
    {
        
    }
}