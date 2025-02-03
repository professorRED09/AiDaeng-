using UnityEngine;

public class Oder : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints; // จุดที่ NPC จะเดินผ่าน

    [SerializeField]
    private Transform exitPoint; // จุดสุดท้ายที่ NPC จะเดินไปหลังจากได้ Order

    [SerializeField]
    private float moveSpeed = 1f; // ความเร็วในการเดิน

    private int waypointIndex = 0; // ดัชนีของ waypoint ปัจจุบัน
    private bool isWaitingForOrder = false; // สถานะว่ารอ Order หรือไม่
    private bool hasReceivedOrder = false; // สถานะว่าได้รับ Order แล้วหรือยัง

    private void Start()
    {
        // ตั้งตำแหน่งเริ่มต้นที่ waypoint แรก
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[waypointIndex].position;
        }
    }

    private void Update()
    {
        if (!isWaitingForOrder && !hasReceivedOrder)
        {
            MoveToNextWaypoint();
        }
        else if (hasReceivedOrder)
        {
            MoveToExit();
        }
    }

    private void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0 || waypointIndex >= waypoints.Length)
            return;

        // เดินไปยัง waypoint ปัจจุบัน
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, moveSpeed * Time.deltaTime);

        // ตรวจสอบว่าถึง waypoint หรือยัง
        if (Vector2.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f)
        {
            isWaitingForOrder = true; // หยุดเพื่อรอการโต้ตอบกับผู้เล่น
            Debug.Log("NPC: ฉันต้องการ Order! คลิกที่ฉันเพื่อส่ง Order");
        }
    }

    private void MoveToExit()
    {
        // เดินไปยังจุดออก
        transform.position = Vector2.MoveTowards(transform.position, exitPoint.position, moveSpeed * Time.deltaTime);

        // ตรวจสอบว่าถึงจุดออกหรือยัง
        if (Vector2.Distance(transform.position, exitPoint.position) < 0.1f)
        {
            Debug.Log("NPC: ฉันเดินออกจากพื้นที่แล้ว!");
            this.enabled = false; // ปิดสคริปต์เมื่อเดินถึงจุดสุดท้าย
        }
    }

    // ตรวจจับการคลิกเมาส์เมื่อผู้เล่นคลิกที่ NPC
    private void OnMouseDown()
    {
        if (isWaitingForOrder)
        {
            Debug.Log("NPC: ขอบคุณสำหรับ Order!");
            isWaitingForOrder = false;
            hasReceivedOrder = true;
        }
    }
}
