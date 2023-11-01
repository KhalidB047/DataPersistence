using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public UnityEvent<int> onDestroyed;

    [SerializeField] private GameObject[] breakParticles;
    
    public int PointValue;

    void Start()
    {
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        switch (PointValue)
        {
            case 1 :
                block.SetColor("_BaseColor", Color.white);
                break;
            case 2:
                block.SetColor("_BaseColor", Color.green);
                break;
            case 3:
                block.SetColor("_BaseColor", Color.yellow);
                break;
            case 5:
                block.SetColor("_BaseColor", Color.red);
                break;
            case 7:
                block.SetColor("_BaseColor", Color.blue);
                break;
            case 10:
                block.SetColor("_BaseColor", Color.black);
                break;
            case 15:
                block.SetColor("_BaseColor", Color.cyan);
                break;
            default:
                block.SetColor("_BaseColor", Color.gray);
                break;
        }
        renderer.SetPropertyBlock(block);
    }

    private void OnCollisionEnter(Collision other)
    {
        onDestroyed.Invoke(PointValue);
        //slight delay to be sure the ball have time to bounce
        Instantiate(breakParticles[Random.Range(0, breakParticles.Length)], transform.position, Quaternion.identity);
        Destroy(gameObject, 0.05f);
    }

    
}
