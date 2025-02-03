using UnityEngine;

public class wayPoint : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;

    [SerializeField]
    private float movespeed = 1f;

    private int waypointIndex = 0;

    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, movespeed * Time.deltaTime);
        }

        if (transform.position == waypoints[waypointIndex].transform.position ) 
        
        {
            waypointIndex += 1;
        }

    }
}