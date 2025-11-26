
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevel : MonoBehaviour
{
    public int Hard = 1;
    public int Easy = 0;
    public int Infinite = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void StartHard()
    {
        SceneManager.LoadScene(Hard);
    }
    public void StartEasy()
    {
        SceneManager.LoadScene(Easy);
    }
    public void StartInfinite()
    {
        SceneManager.LoadScene(Infinite);
    }
}
