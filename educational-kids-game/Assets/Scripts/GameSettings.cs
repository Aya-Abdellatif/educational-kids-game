using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour {

   
    public void LoadScene(string sceneName)
    {
        MusicManager.Instance.PlaySFX(MusicManager.Instance.buttonClickSound);
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
