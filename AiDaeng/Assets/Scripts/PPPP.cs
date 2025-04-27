using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPPP : MonoBehaviour
{
    public Transform counterWP;
    public float speed;    

    // Update is called once per frame
    void Update()
    {
        WalkToCounter();
    }

    void WalkToCounter()
    {
        Vector3 direction = (counterWP.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
