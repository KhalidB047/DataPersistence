using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    //game manager accesible from any other script
    public static GameManager gameMan;

    //object access
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI newHighScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;


    //working data
    [SerializeField] private int currentScore;
    public string playerName = "";
    public bool gameOver;
    public bool started;
    public bool paused;


    //working data, same as save data
    public HighScore[] scoresList = new HighScore[9];



    //game manager will persist through scenes
    private void Awake()
    {
        if (gameMan == null)
        {
            gameMan = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadGame(); //optional w/ button
    }





    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PauseGame();
        }
    }


    public void AddScore(int points)
    {
        currentScore += points;
        currentScoreText.text = $"{currentScore}\n{playerName}\nScore";
    }


    public void UpdateHighScore()
    {
        for (int i = 0; i < scoresList.Length; i++)
        {
            if (currentScore > scoresList[i].score)
            {
                for (int j = scoresList.Length - 1; j > i; j--)
                {
                    scoresList[j].name = scoresList[j - 1].name;
                    scoresList[j].score = scoresList[j - 1].score;
                }
                scoresList[i].name = playerName;
                scoresList[i].score = currentScore;
                newHighScoreText.gameObject.SetActive(true);
                newHighScoreText.text = $"NEW #{i + 1} HIGH SCORE";
                highScoreText.text = $"High Score\n{scoresList[0].name}\n{scoresList[0].score}";
                return;
            }
        }



        //if(currentScore >= highScore)
        //{
        //    highScore = currentScore;
        //    highScoreText.text = $"High Score\n{playerName}\n{highScore}";
        //}
    }

    public void StartGame()
    {
        //started = true handled at different time
        gameOver = false;
        currentScoreText.gameObject.SetActive(true);
        playerName = FindObjectOfType<TMP_InputField>().text;
        AddScore(0);
        SceneManager.LoadScene(1);
    }

    public void PauseGame()
    {
        if (gameOver) return;
        if (!started) return;
        if (paused)
        {
            ResumeGame();
        }
        else
        {
            Time.timeScale = 0f;
            paused = true;
            pauseScreen.SetActive(true);
        }

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        paused = false;
        pauseScreen.SetActive(false);
    }


    public void LoadNextNevel()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(buildIndex);
    }

    public void ViewLeaderboard()
    {
        highScoreText.gameObject.SetActive(false);
        SceneManager.LoadScene("Leaderboard");
    }


    public void RestartGame()
    {
        ResumeGame();
        started = false;
        gameOver = false;
        AddScore(-currentScore);
        gameOverScreen.SetActive(false);
        newHighScoreText.gameObject.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void GameOver()
    {
        UpdateHighScore();
        gameOver = true;
        gameOverScreen.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        ResumeGame();
        started = false;
        AddScore(-currentScore);
        gameOverScreen.SetActive(false);
        newHighScoreText.gameObject.SetActive(false);
        currentScoreText.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(true);
        SceneManager.LoadScene(0);
    }


    public void CloseApplication()
    {
        SaveGame();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }





    //the class containing the data to be saved between sessions
    [System.Serializable]
    public class SaveData
    {
        public HighScore[] savedScores = new HighScore[9];
    }

    [System.Serializable]
    public class HighScore
    {
        public string name = "AAA";
        public int score = 0;
    }


    public void SaveGame()
    {
        //create a new savedata class instance and
        //give it the necessary save data
        SaveData data = new SaveData();
        for (int i = 0; i < data.savedScores.Length; i++)
        {
            data.savedScores[i] = scoresList[i]; 
        }
        //convert the data to a json string
        //then write that string to a file
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadGame()
    {
        //make a path to the save file location
        //and check if it exists
        //if so, get a json string from reading the file path
        //then convert that json string to savedata
        //then set the local data to that data
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);


            for (int i = 0; i < data.savedScores.Length; i++)
            {
                scoresList[i] = data.savedScores[i];
            }
            
            highScoreText.text = "High Score\n" + scoresList[0].name + "\n" + scoresList[0].score;
        }
    }
}
