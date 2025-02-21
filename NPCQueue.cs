using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQueue : MonoBehaviour
{
    private List<Vector3> positionList = new List<Vector3>(); // ตำแหน่งคิว
    private List<CM_Order> orderList = new List<CM_Order>();  // NPC ในคิว
    private Vector3 entrancePosition; // จุดเข้าคิว
    public Transform orderPoint; // จุดสั่งดอกไม้
    private CM_Order currentOrderingNPC = null; // NPC ที่กำลังรอรับดอกไม้

    void Start()
    {
        // สามารถกำหนดค่าเริ่มต้นของคิวได้ที่นี่
    }

    void Update()
    {
        // สามารถเพิ่มฟังก์ชันเช็คอินพุตเพื่อให้ผู้เล่นกดปุ่มให้ดอกไม้ได้ที่นี่
    }

    public void Initialize(List<Vector3> positions, Transform orderTarget)
    {
        if (positions == null || positions.Count == 0)
        {
            Debug.LogError("Position list is empty! Cannot initialize NPCQueue.");
            return;
        }

        positionList = positions;
        orderList = new List<CM_Order>();
        entrancePosition = positionList[positionList.Count - 1] + new Vector3(-8f, 0);
        orderPoint = orderTarget;
    }

    public bool CanAddOrder()
    {
        return orderList.Count < positionList.Count;
    }

    public void PlaceOrder(Npc guest)
    {
        if (!CanAddOrder())
        {
            Debug.Log("Queue is full! Cannot place order.");
            return;
        }

        GameObject orderObj = new GameObject("Order");
        CM_Order newOrder = orderObj.AddComponent<CM_Order>();
        newOrder.Initialize(guest, orderList.Count + 1, "Flower");

        orderList.Add(newOrder);

        newOrder.MoveTo(entrancePosition, () =>
        {
            int index = orderList.IndexOf(newOrder);
            if (index >= 0 && index < positionList.Count)
            {
                newOrder.MoveTo(positionList[index]);
            }

            if (index == 0)
            {
                StartCoroutine(OrderFlower(newOrder));
            }
        });
    }

    private IEnumerator OrderFlower(CM_Order npcOrder)
    {
        yield return new WaitForSeconds(1f);

        npcOrder.MoveTo(orderPoint.position, () =>
        {
            Debug.Log(npcOrder.orderID + " กำลังสั่งดอกไม้!");
            currentOrderingNPC = npcOrder;
        });
    }

    public void GiveFlowerToNPC()
    {
        if (currentOrderingNPC == null)
        {
            Debug.Log("ไม่มี NPC ที่กำลังรอรับดอกไม้!");
            return;
        }

        Debug.Log(currentOrderingNPC.orderID + " ได้รับดอกไม้!");
        StartCoroutine(OnFlowerReceived(currentOrderingNPC));
    }

    private IEnumerator OnFlowerReceived(CM_Order npcOrder)
    {
        yield return new WaitForSeconds(2f);

        orderList.Remove(npcOrder);
        Destroy(npcOrder.gameObject);
        currentOrderingNPC = null;

        RelocateAllOrders();

        if (orderList.Count > 0)
        {
            StartCoroutine(OrderFlower(orderList[0]));
        }
    }

    private void RelocateAllOrders()
    {
        for (int i = 0; i < orderList.Count; i++)
        {
            orderList[i].MoveTo(positionList[i]);
        }
    }
}
