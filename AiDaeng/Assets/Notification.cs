using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    private NotificationManager notiManager;
    private Animator animator;

    private void Awake()
    {
        notiManager = FindObjectOfType<NotificationManager>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Leave());
    }

    public IEnumerator Leave()
    {
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("noti");  //animation out        
        Destroy(this.gameObject, 2f);        //destroy from the scene
    }

    //public void Leave()
    //{
        
    //    animator.SetTrigger("noti");  //animation out        
    //    Destroy(gameObject, 5f);        //destroy from the scene
    //}
}
