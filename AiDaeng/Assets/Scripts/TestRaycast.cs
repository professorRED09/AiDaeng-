using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRaycast : MonoBehaviour
{
    public bool detect;
    public float length;
    public LayerMask layerMask;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, new Vector2(transform.localScale.x, 0), length, layerMask);
        if(hit.collider != null)
        {
            detect = true;
        }
        else
        {
            detect = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Vector3 origin = transform.position + offset;
        Gizmos.DrawLine(origin, origin + (Vector3)(direction * length));
    }
}
