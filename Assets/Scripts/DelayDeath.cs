using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayDeath : MonoBehaviour
{

    private bool timeRunning = false;
    private float timePassed = 0.0f;
    public float TargetTime = 5.0f;
    public int AvattavaTaso = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRunning == true)
        {

            if (timePassed < TargetTime)
            {
                timePassed += Time.deltaTime;
            }
            if (timePassed >= TargetTime)
            {
                SceneManager.LoadScene(AvattavaTaso);
                timeRunning = false;
                timePassed = 0.0f;
            }

        }


    }
}
