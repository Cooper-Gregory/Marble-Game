using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowObjectOwned : MonoBehaviour
{
    public TextMeshProUGUI objectHUDtext;
    public string textToShow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowText()
    {
        objectHUDtext.text = textToShow;
    }
}
