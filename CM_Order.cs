using System.Collections;
using UnityEngine;

public class CM_Order : MonoBehaviour
{
    [SerializeField] private string orderType;
    [SerializeField] private string orderID;
    private Npc guest;

    public void Initialize(Npc guest, int id, string type)
    {
        if (guest == null)
        {
            Debug.LogError("Guest cannot be null!");
            return;
        }

        this.guest = guest;
        this.orderType = type;
        this.orderID = "orderID";  

        Debug.Log("Created Order: " + orderID);  
    }

    public void MoveTo(Vector3 targetPosition, System.Action onComplete = null)
    {
        StartCoroutine(MoveOrderRoutine(targetPosition, onComplete));
    }

    private IEnumerator MoveOrderRoutine(Vector3 target, System.Action onComplete)
    {
        float speed = 5f;
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        onComplete?.Invoke();
    }
}

