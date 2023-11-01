using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private Button menuButton;
    [SerializeField] private TextMeshProUGUI highScoresListText;

    // Start is called before the first frame update
    void Start()
    {
        menuButton.onClick.AddListener(GameManager.gameMan.QuitToMainMenu);
        UpdateScores();
    }


    private void UpdateScores()
    {
        for (int i = 0; i < GameManager.gameMan.scoresList.Length ; i++)
        {
            highScoresListText.text += $"{i + 1}. {GameManager.gameMan.scoresList[i].name} {GameManager.gameMan.scoresList[i].score}\n";
        }
    }

}
