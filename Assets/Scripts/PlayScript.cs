using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayScript : MonoBehaviour
{

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Alley");
    }


}
