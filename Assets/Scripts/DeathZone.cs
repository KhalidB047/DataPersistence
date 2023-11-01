using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private GameObject paddleParticles;
    [SerializeField] private GameObject ballParticles;
    [SerializeField] private AudioClip gameOverSound;


    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        Instantiate(ballParticles, other.gameObject.transform.position, Quaternion.identity);
        Instantiate(paddleParticles, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>().PlayOneShot(gameOverSound);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        GameManager.gameMan.GameOver();
    }
}