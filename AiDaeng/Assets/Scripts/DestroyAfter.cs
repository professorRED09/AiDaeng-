using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float timeToDestroy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    
}
