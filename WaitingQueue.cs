using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingQueue : MonoBehaviour
{
    private List<Vector3> positionList = new List<Vector3>(); // ตำแหน่งที่รอในคิว
    private List<CM_Order> orderList = new List<CM_Order>();  // รายการ Order ในคิว
    private Vector3 entrancePosition; // จุดรับคำสั่ง

    public void Initialize(List<Vector3> positions)
    {
        if (positions == null || positions.Count == 0)
        {
            Debug.LogError("Position list is empty! Cannot initialize WaitingQueue.");
            return;
        }

        positionList = positions;
        orderList = new List<CM_Order>();
        entrancePosition = positionList[positionList.Count - 1] + new Vector3(-8f, 0);

        // สร้าง Sprite สำหรับแต่ละตำแหน่งในคิว
        foreach (Vector3 position in positionList)
        {
            CreateSprite(position, Color.green);
        }
        CreateSprite(entrancePosition, Color.magenta);
    }

    // ตรวจสอบว่าสามารถเพิ่ม Order ได้หรือไม่
    public bool CanAddOrder()
    {
        return orderList.Count < positionList.Count;
    }

    // สร้าง Order ใหม่และเพิ่มลงในคิว
    public void PlaceOrder(Npc guest)
    {
        if (!CanAddOrder())
        {
            Debug.Log("Queue is full! Cannot place order.");
            return;
        }

        GameObject orderObj = new GameObject("Order");
        CM_Order newOrder = orderObj.AddComponent<CM_Order>();
        newOrder.Initialize(guest, orderList.Count + 1, "Food");

        orderList.Add(newOrder);

        // เคลื่อนย้ายไปที่จุดเริ่มต้น จากนั้นไปยังตำแหน่งคิวที่เหมาะสม
        newOrder.MoveTo(entrancePosition, () =>
        {
            int index = orderList.IndexOf(newOrder);
            if (index >= 0 && index < positionList.Count)
            {
                newOrder.MoveTo(positionList[index]);
            }
        });

        Debug.Log("Order placed successfully!");
    }

    // รับ Order ออกจากคิว
    public CM_Order ReceiveOrder()
    {
        if (orderList.Count == 0)
        {
            Debug.Log("No orders in queue!");
            return null;
        }

        CM_Order order = orderList[0];
        orderList.RemoveAt(0);

        // รีเรียง Order ที่เหลือ
        RelocateAllOrders();

        Debug.Log("Order received: " + order.orderID);
        return order;
    }

    // เคลื่อนย้าย Order ทั้งหมดที่เหลือให้อยู่ในตำแหน่งที่เหมาะสม
    private void RelocateAllOrders()
    {
        for (int i = 0; i < orderList.Count; i++)
        {
            orderList[i].MoveTo(positionList[i]);
        }
    }

    // ฟังก์ชันช่วยสร้าง Sprite สำหรับคิว
    private void CreateSprite(Vector3 position, Color color)
    {
        GameObject spriteObj = new GameObject("QueueSlot");
        spriteObj.transform.position = position;
        SpriteRenderer sr = spriteObj.AddComponent<SpriteRenderer>();
        sr.color = color;
    }

    public CM_Order GetFirstInQueue()
    {
        if (orderList.Count == 0)
        {
            Debug.Log("Queue is empty!");
            return null;
        }

        CM_Order firstOrder = orderList[0];
        orderList.RemoveAt(0);

        // รีเรียงออเดอร์ที่เหลือ
        RelocateAllOrders();

        return firstOrder;
    }
}
