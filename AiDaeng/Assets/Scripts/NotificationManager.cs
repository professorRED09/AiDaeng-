using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    //public List<GameObject> notif = new List<GameObject>();
    //public GameObject currentNoti;
    public GameObject noti;
    public static NotificationManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void AddNoti(string message)
    {        
        GameObject newNoti = Instantiate(noti, gameObject.transform);        
        newNoti.GetComponentInChildren<TMP_Text>().text = message;
    }
}
