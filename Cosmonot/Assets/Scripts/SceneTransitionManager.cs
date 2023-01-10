using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public void GoToMainScreen()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void GoToOptionsScreen()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    public void GoToGameScreen()
    {
        SceneManager.LoadScene("GameScene");
    }
}
