using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{

    public Button quitButton;


    void Start()
    {
        
        Button btn = quitButton.GetComponent<Button>();
        btn.onClick.AddListener(QuitGameFunction);

    }

    void QuitGameFunction()
    {
        Application.Quit();
    }

}
