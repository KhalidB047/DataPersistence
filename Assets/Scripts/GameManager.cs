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
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;


    //working data
    [SerializeField] private int currentScore;
    public bool gameOver;
    public bool started;
    public bool paused;


    //working data, same as save data
    public int highScore;
    public string playerName;


    //game manager will persist through scenes
    private void Awake()
    {
        if(gameMan == null)
        {
            gameMan = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadGame();
    }

    //private void Start()
    //{
    //}



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
        if(currentScore >= highScore)
        {
            highScore = currentScore;
            highScoreText.text = $"High Score\n{playerName}\n{highScore}";
        }
    }

    public void StartGame()
    {
        //started = true handled at different time
        gameOver = false;
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


    public void RestartGame()
    {
        ResumeGame();
        started = false;
        AddScore(-currentScore);
        gameOverScreen.SetActive(false);
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
        public string playerName;
        public int highScore;
    }


    public void SaveGame()
    {
        //create a new savedata class instance and
        //give it the necessary save data
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.highScore = highScore;

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
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
            highScore = data.highScore;
            highScoreText.text = "High Score\n" + playerName + "\n" + highScore;

        }
        else
        {
            playerName = "";
            highScore = 0;
        }
    }
}
