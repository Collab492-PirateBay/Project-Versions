using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // <summary>
    // Public variable for the current state of the game manager.
    // </summary>
    public GameState CurrentState
    {
        get
        {
            return m_currentState;
        }
        set
        {
            m_currentState = value;
        }
    }

    private int currentSceneindex;
    public void setCurrentScene(int index)
    {
        currentSceneindex = index;
        m_currentState = GameState.Load;
    }

    public AudioClip Menu_music,
                      Game_music;


    public AudioMixer MainAudio;

    [HideInInspector]
    public AudioSource BGM;

    // <summary>
    // Variable for the current game state upon load,
    // which is the Main Menu.
    // </summary>
    [SerializeField]
    private GameState m_currentState = GameState.Menu;

    // <summary>
    // Variable for the previous game state.
    // </summary>
    private GameState m_previousState;

    public static GameManager GameManagerInstance
    {
        get
        {
            return m_gameManagerInstance;
        }
    }

    // <summary>
    // Reference to the game state manager.
    // </summary>
    private static GameManager m_gameManagerInstance = null;

    public Scene[] SceneList;

    public enum GameState
    {
        Menu,
        Game,
        Load,
        Pause
    }

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        if (m_gameManagerInstance == null)
        {
            m_gameManagerInstance = this;
            BGM = GetComponent<AudioSource>();

            m_currentState = GameState.Menu;

            m_previousState = m_currentState;
        }
        else
        {
            DestroyImmediate(transform.gameObject);
        }


    }

    private void Update()
    {

        if (m_previousState != m_currentState)
        {

            switch (m_currentState)
            {
                case GameState.Menu:
                    {
                        //Debug.Log("Switch to the Main Menu state.");

                        //BGM.clip = Menu_music;
                        //BGM.Play();

                        if (m_previousState == GameState.Pause)
                        {
                            Time.timeScale = 1;
                        }
                        SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(0).name);

                        break;
                    }


                case GameState.Game:
                    {
                        //Debug.Log("Switch to the Game state");

                        if (m_previousState != GameState.Pause)
                        {
                            //BGM.clip = Game_music;
                            //BGM.Play();
                        }
                        if (m_previousState == GameState.Pause)
                        {
                            Time.timeScale = 1;
                            //BGM.Play();
                        }

                        break;
                    }

                case GameState.Load:
                    {
                        SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(currentSceneindex).name);
                        m_currentState = GameState.Game;
                    }

                    break;

                case GameState.Pause:
                    {
                        //Debug.Log("Switch to the Pause state.");

                        Time.timeScale = 0;
                        //BGM.Stop();
                        //Activate UI for pause menu

                        break;
                    }


                default:
                    {
                        Debug.Log("State Error. No State Assigned.");

                        m_currentState = GameState.Pause;

                        break;
                    }



            }
        }
        m_previousState = m_currentState;
    }

}
