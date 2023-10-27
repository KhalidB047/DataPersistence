using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour
{
    [SerializeField] private float maxX;
    [SerializeField] private float maxY;
    [SerializeField] private float minY;


    [SerializeField] private GameObject[] fireWorks;


    private bool launching = true;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.gameMan.gameOver = true;
        //GameManager.gameMan.UpdateHighScore();
        StartCoroutine(LaunchFireworks());
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private IEnumerator LaunchFireworks()
    {
        while(launching)
        {
            Instantiate(fireWorks[Random.Range(0, fireWorks.Length)], RandomPosition(), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0.10f, 0.6f));
        }
    }



    private Vector3 RandomPosition()
    {
        float randomX = Random.Range(-maxX, maxX);
        float randomY = Random.Range(minY, maxY);

        Vector3 pos = new Vector3(randomX, randomY, 0f);
        return pos;
    }

}
