using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CM_handle : MonoBehaviour
{
    private WaitingQueue waitingQueue;
    private Vector3 firstPosition;
    private int orderCounter = 0;

    [SerializeField] private GameObject orderPrefab; // Prefab สำหรับ CM_Order

    private void Start()
    {
        List<Vector3> waitingQueuePositionList = new List<Vector3>();
        firstPosition = new Vector3(740, 280);
        float positionSize = 8f;

        for (int i = 0; i < 5; i++)
        {
            waitingQueuePositionList.Add(firstPosition + new Vector3(-1, 0) * positionSize * i);
        }

        // สร้าง GameObject สำหรับ WaitingQueue
        GameObject queueObject = new GameObject("WaitingQueue");
        waitingQueue = queueObject.AddComponent<WaitingQueue>();
        waitingQueue.Initialize(waitingQueuePositionList);

        // ปุ่ม UI สำหรับสั่งและส่ง Order
        CreateUIButton("Add Order", PlaceOrder);
        CreateUIButton("Take Order", DeliverOrder);
    }

    // ✅ ฟังก์ชันสั่ง Order (เพิ่ม Order ลงในคิว)
    private void PlaceOrder()
    {
        if (waitingQueue.CanAddOrder())  // ✅ เรียกใช้ waitingQueue ได้ถูกต้อง
        {
            Npc guest = Npc.GetGuestSequential();
            GameObject orderObj = Instantiate(orderPrefab, transform.position, Quaternion.identity);
            CM_Order cmOrder = orderObj.GetComponent<CM_Order>();

            if (cmOrder != null)
            {
                orderCounter++;  // ✅ เพิ่มค่า id ทุกครั้งที่สร้างออเดอร์
                cmOrder.Initialize(guest, orderCounter, "Food");
                waitingQueue.AddOrder(cmOrder);
                Debug.Log("Added Order: " + cmOrder.orderID);
            }
            else
            {
                Debug.LogError("CM_Order component missing from prefab!");
            }
        }
        else
        {
            Debug.Log("Queue is full! Cannot add more orders.");
        }
    }

    // ✅ ฟังก์ชันส่ง Order (ดึง Order ออกจากคิว)
    private void DeliverOrder()
    {
        CM_Order order = waitingQueue.GetFirstInQueue();
        if (order != null)
        {
            order.MoveTo(firstPosition + new Vector3(5000, 0)); // ส่งออกไปไกล
            Debug.Log("Delivered Order: " + order.orderID);
        }
        else
        {
            Debug.Log("No orders available.");
        }
    }

    // ✅ ฟังก์ชันสร้างปุ่ม UI
    private void CreateUIButton(string buttonText, System.Action onClick)
    {
        GameObject buttonObj = new GameObject(buttonText);
        buttonObj.transform.SetParent(GameObject.Find("Canvas").transform);

        Button button = buttonObj.AddComponent<Button>();
        button.onClick.AddListener(() => onClick?.Invoke());

        // ✅ สร้าง GameObject สำหรับ Text แยกจากปุ่ม
        GameObject buttonTextObj = new GameObject("Text");
        buttonTextObj.transform.SetParent(buttonObj.transform);

        Text buttonTextComp = buttonTextObj.AddComponent<Text>();
        buttonTextComp.text = buttonText;
        buttonTextComp.alignment = TextAnchor.MiddleCenter;

        // ปรับขนาดปุ่ม
        RectTransform rect = buttonObj.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(200, 50);

        RectTransform textRect = buttonTextObj.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(200, 50);
        textRect.localPosition = Vector3.zero;
    }
}
