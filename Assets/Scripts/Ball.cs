using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;

    [SerializeField] private float maxBallVelocity = 3f;
    [SerializeField] private float ballAccelerationRate = 0.01f;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] bounceSounds;


    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        source = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Brick"))
        {
            source.PlayOneShot(bounceSounds[2]);
        }
        else
        if (other.gameObject.CompareTag("Player"))
        {
            source.PlayOneShot(bounceSounds[1]);
        }
        else
            source.PlayOneShot(bounceSounds[0]);

        var velocity = m_Rigidbody.velocity;

        //after a collision we accelerate a bit
        velocity += velocity.normalized * ballAccelerationRate;

        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.right * 0.3f : Vector3.left * 0.3f;
        }

        //max velocity
        if (velocity.magnitude > maxBallVelocity)
        {
            velocity = velocity.normalized * maxBallVelocity;
        }

        m_Rigidbody.velocity = velocity;
    }
}
