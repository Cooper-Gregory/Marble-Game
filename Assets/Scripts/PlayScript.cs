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
        Button btn = playButton.GetComponent<Button>();
        btn.onClick.AddListener(LoadNextScene);

    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("Alley");
    }
}
