using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UmbrellaTask : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] TMP_Text _umbrellaAmountText;
    public Transform waypoint;  
    private Namwan namwan;       
    private AudioSource audioSource;
    private float _umbrellaAmount = 0;

    

    private void Start()
    {
        namwan = FindObjectOfType<Namwan>(); // get reference from player script
        audioSource = GetComponent<AudioSource>();

        _umbrellaAmountText.text = _umbrellaAmount.ToString();
    }

    private void OnMouseDown()
    {
        // when players click on this task, the character will move to this task  
        if (waypoint != null && namwan != null)
        {
            namwan.MoveToWaypoint(waypoint);
            namwan.currentTask = Namwan.CurrentTask.Umbrella;
            namwan.ChangeTask(namwan.currentTask);
        }
    }

    public void TakeUmbrella()
    {

    }

    public void ReturnUmbrella()
    {
        ++_umbrellaAmount;
        _umbrellaAmountText.text = _umbrellaAmount.ToString();

        //inform player that the umbrella amount has increase
    }

}
