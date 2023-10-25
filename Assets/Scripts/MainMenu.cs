using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    //get menu buttons
    [SerializeField] private Button startButton, quitButton, highScoresButton;


    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(GameManager.gameMan.StartGame);
        quitButton.onClick.AddListener(GameManager.gameMan.CloseApplication);
        //highScoresButton.onClick.AddListener(GameManager.gameMan...);
    }
}
