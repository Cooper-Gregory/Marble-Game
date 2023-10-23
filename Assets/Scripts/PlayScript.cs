using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayScript : MonoBehaviour
{

    public Button playButton;


    void Start()
    {
        playButton.onClick.AddListener(LoadGameScene);

    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("Alley");
    }


}
