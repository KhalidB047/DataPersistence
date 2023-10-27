using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EndButtons : MonoBehaviour
{
    //get menu buttons
    [SerializeField]
    private Button restartButton, quitToMenuButton,
         highScoresButton;

    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(GameManager.gameMan.RestartGame);
        quitToMenuButton.onClick.AddListener(GameManager.gameMan.QuitToMainMenu);
        //highScoresButton.onClick.AddListener(GameManager.gameMan...);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
