using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Threading;
using System;
using System.Diagnostics;
using UnityEngine.SocialPlatforms.Impl;

public class GroundScore : MonoBehaviour
{
    public bool PlayerHitsTheGround = false;
    public int GetScoreOnce = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GetScoreOnce < 1)
            {

                GetScoreOnce++;
            }
        }
    }
}