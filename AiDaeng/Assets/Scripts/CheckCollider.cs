using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {        
        if (col.tag == "Player")
        {
            Debug.Log("Player arrived at waypoint: " + col.gameObject.name);
        }
    }
}
