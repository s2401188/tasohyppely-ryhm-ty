using UnityEngine;

public class Delayed_Sounds : MonoBehaviour
{
    private bool timeRunning = false;
    private float timePassed = 0.0f;
    public float TargetTime = 5.0f;
    private AudioSource audioSource;
    public AudioClip SOUND;


    void Start()
    {
        timeRunning = true;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (timeRunning == true)
        {


            if (timePassed < TargetTime)
                timePassed += Time.deltaTime;

            if (timePassed >= TargetTime)
            {
                
                timeRunning = false;
                timePassed = 0.0f;
                audioSource.clip = SOUND;
                audioSource.Play();
            }
        }
    }
}
