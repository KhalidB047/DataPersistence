using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{


    public Brick BrickPrefab;
    public Rigidbody Ball;
    public Rigidbody ball2;

    //level variables
    [SerializeField] private float levelWidth = 4f;
    [SerializeField] private float brickStartHeight = 2.5f;
    [SerializeField] private float brickLineSpacing = 0.3f;
    [SerializeField] private int LineCount = 6;
    [SerializeField] private float stepSize = 0.6f;
    [SerializeField] private int[] pointValues;
    [SerializeField] private int bricksPerLine;
    [SerializeField] private int bricksLeft = 1;




    void Start()
    {
        GameManager.gameMan.started = false;
        bricksPerLine = Mathf.FloorToInt(levelWidth / stepSize);
        BuildLevel();
    }

    private void Update()
    {
        
        bricksLeft = FindObjectsOfType<Brick>().Length;
        if (bricksLeft == 0) GameManager.gameMan.LoadNextNevel();

        if (GameManager.gameMan.started) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartLevel();
        }

    }


    public void BuildLevel()
    {
        bricksPerLine = Mathf.FloorToInt(levelWidth / stepSize);

        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < bricksPerLine; ++x)
            {
                Vector3 position;

                if (bricksPerLine % 2 == 0)
                {
                    position = new Vector3(-(bricksPerLine/2) * stepSize + stepSize * x + (stepSize/2), 
                        brickStartHeight + i * brickLineSpacing, 0);
                } else
                {
                    position = new Vector3(-(bricksPerLine / 2) * stepSize + stepSize * x,
                        brickStartHeight + i * brickLineSpacing, 0);
                }
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointValues[i];
                brick.onDestroyed.AddListener(GameManager.gameMan.AddScore);
            }
        }
    }




    private void StartLevel()
    {
        GameManager.gameMan.started = true;
        float randomDirection = Random.Range(-1.0f, 1.0f);
        Vector3 forceDir = new Vector3(randomDirection, 1, 0);
        forceDir.Normalize();

        Ball.transform.SetParent(null);
        Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);


        if(ball2 != null)
        {
            randomDirection = Random.Range(-1.0f, 1.0f);
            forceDir = new Vector3(randomDirection, 1, 0);
            forceDir.Normalize();
            ball2.transform.SetParent(null);
            ball2.AddForce(forceDir * 0.6f, ForceMode.VelocityChange);
        }
    }



}
