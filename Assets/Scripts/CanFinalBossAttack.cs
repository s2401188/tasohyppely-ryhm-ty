using UnityEngine;
using UnityEngine.Events;

public class CanFinalBossAttack : MonoBehaviour
{

    public bool HasEntered = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasEntered = true;
            Debug.Log("Player has entered the final boss area");
        }
    }
}