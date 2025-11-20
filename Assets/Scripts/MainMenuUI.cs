using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public int ScenenNumero = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void GoToScene()
    {
        SceneManager.LoadScene(ScenenNumero);
    }
}
