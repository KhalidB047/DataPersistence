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
        if(restartButton != null)
        restartButton.onClick.AddListener(GameManager.gameMan.RestartGame);
        if(quitToMenuButton != null)
        quitToMenuButton.onClick.AddListener(GameManager.gameMan.QuitToMainMenu);
        if(highScoresButton != null)
        highScoresButton.onClick.AddListener(GameManager.gameMan.ViewLeaderboard);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
