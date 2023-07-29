using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayScript : MonoBehaviour
{

    public Button playButton;
    public Button menuButton;

    void Start()
    {
        playButton.onClick.AddListener(LoadGameScene);
        menuButton.onClick.AddListener(LoadMenuScene);
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("Alley");
    }

    void LoadMenuScene()
    {
        SceneManager.LoadScene("AlleyLoadingScreen");
    }
}
