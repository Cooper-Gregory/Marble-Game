using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Button menuButton;
    void Start()
    {
        menuButton.onClick.AddListener(LoadMenuScene);
    }



    void LoadMenuScene()
    {
        SceneManager.LoadScene("AlleyLoadingScreen");
    }
}
