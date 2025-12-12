using UnityEngine;

public class MusicLoop : MonoBehaviour
{
    public AudioClip[] tracks;

    private AudioSource audioSource;
    private int currentIndex = 0;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = false;
        audioSource.playOnAwake = false;
        // DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (tracks != null && tracks.Length > 0)
        {
            PlayCurrent();
        }
    }

    void Update()
    {
        if (!audioSource.isPlaying && tracks.Length > 0)
        {
            NextTrack();
        }
    }

    void PlayCurrent()
    {
        audioSource.clip = tracks[currentIndex];
        audioSource.Play();
    }

    void NextTrack()
    {
        currentIndex = (currentIndex + 1) % tracks.Length;
        PlayCurrent();
    }
}
