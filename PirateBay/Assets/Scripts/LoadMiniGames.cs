using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadMiniGames : MonoBehaviour {

    public void OnFairyDusterClicked()
    {
        SceneManager.LoadScene("OtherSceneName", LoadSceneMode.Additive);

    }
}
