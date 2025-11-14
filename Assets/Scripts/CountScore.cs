using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Text Scoretext;
    int score = 0;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Scoretext.text =  score.ToString()+ "POINTS"; 
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
