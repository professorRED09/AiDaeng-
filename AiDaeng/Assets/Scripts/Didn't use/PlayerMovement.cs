//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerMovement : MonoBehaviour
//{
//    public float speed = 5f; // Movement speed
//    public LayerMask obstacleLayer; // Layer for obstacles
//    private Transform targetWaypoint; // The current target waypoint
//    private bool isMoving = false; // Is the player moving?
//    private bool moveHorizontal = true; // Move along the X-axis first

//    void Update()
//    {
//        if (isMoving && targetWaypoint != null)
//        {
//            Vector3 currentPosition = transform.position;
//            Vector3 targetPosition = targetWaypoint.position;

//            // Determine movement direction and check for obstacles
//            if (moveHorizontal)
//            {
//                // Check if there's an obstacle along the X-axis
//                if (Mathf.Abs(currentPosition.x - targetPosition.x) > 0.1f)
//                {
//                    if (CanMove(currentPosition, Vector2.right * Mathf.Sign(targetPosition.x - currentPosition.x)))
//                    {
//                        // Move horizontally
//                        float step = speed * Time.deltaTime * Mathf.Sign(targetPosition.x - currentPosition.x);
//                        transform.position = new Vector3(currentPosition.x + step, currentPosition.y, currentPosition.z);
//                    }
//                    else
//                    {
//                        Debug.Log("Obstacle detected on X-axis!");
//                        isMoving = false;
//                        return;
//                    }
//                }
//                else
//                {
//                    moveHorizontal = false; // Switch to vertical movement
//                }
//            }
//            else
//            {
//                // Check if there's an obstacle along the Y-axis
//                if (Mathf.Abs(currentPosition.y - targetPosition.y) > 0.1f)
//                {
//                    if (CanMove(currentPosition, Vector2.up * Mathf.Sign(targetPosition.y - currentPosition.y)))
//                    {
//                        // Move vertically
//                        float step = speed * Time.deltaTime * Mathf.Sign(targetPosition.y - currentPosition.y);
//                        transform.position = new Vector3(currentPosition.x, currentPosition.y + step, currentPosition.z);
//                    }
//                    else
//                    {
//                        Debug.Log("Obstacle detected on Y-axis!");
//                        isMoving = false;
//                        return;
//                    }
//                }
//                else
//                {
//                    // Movement complete
//                    isMoving = false;
//                    OnTaskComplete();
//                }
//            }
//        }
//    }

//    // Check for obstacles using a raycast
//    private bool CanMove(Vector3 currentPosition, Vector2 direction)
//    {
//        RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, speed * Time.deltaTime, obstacleLayer);
//        return hit.collider == null; // Returns true if no obstacle is detected
//    }

//    // Start moving toward the target waypoint
//    public void MoveToWaypoint(Transform waypoint)
//    {
//        targetWaypoint = waypoint;
//        isMoving = true;
//        moveHorizontal = true; // Always start with horizontal movement
//    }

//    // Task completion logic
//    private void OnTaskComplete()
//    {
//        Debug.Log("Task completed at: " + targetWaypoint.name);
//        // Implement task-specific logic here
//    }
//}
