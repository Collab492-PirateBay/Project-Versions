using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryIntroManager : MonoBehaviour 
{

    public TypeWriterAnimation m_DialogueRelation;

    //...............................................................
    //..................................................... * START *
    void Start()
    {
        GameObject m_StroyDialogue = GameObject.FindGameObjectWithTag("StoryIntro");
        m_DialogueRelation = m_StroyDialogue.GetComponent<TypeWriterAnimation>();
    }

    //...............................................................
    //.................................................... * UPDATE *
    void Update()
    {
        if (m_DialogueRelation.m_DialogueIsDone == true)
        {
            SceneManager.LoadScene("MiniGame_FairyDuster");
        }
    }

    //...............................................................
    //....................................... * METHOD : LOAD LEVEL *
    //LOADING LEVEL
    public void LoadLevel(string level)
    {
        Application.LoadLevel(level);
    }


}
