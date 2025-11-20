using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float rotationSpeed = 90f; 

    void Update()
    {
        // Pyöritetään objektia ympäri Z-akselia
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
