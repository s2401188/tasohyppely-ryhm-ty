using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Threading;
using System;
using System.Diagnostics;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Events;

public class GroundScore : MonoBehaviour
{
   
    private int GetScoreOnce = 0;
   
    public UnityEvent GetTheScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GetScoreOnce == 0)
            {
               GetScoreOnce++;
            }
        }
    }
}