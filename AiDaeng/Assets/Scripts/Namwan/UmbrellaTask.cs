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

    public float umbrellaAmount = 0;
    [SerializeField] float _maxUmbrellaAmount;

    private void Start()
    {
        namwan = FindObjectOfType<Namwan>(); // get reference from player script
        audioSource = GetComponent<AudioSource>();

        umbrellaAmount = _maxUmbrellaAmount;
        _umbrellaAmountText.text = umbrellaAmount.ToString();
    }

    private void OnMouseDown()
    {
        // when players click on this task, the character will move to this task  
        if (waypoint != null && namwan != null)
        {
            namwan.MoveToWaypoint(waypoint);            
            namwan.ChangeTask(Namwan.CurrentTask.Umbrella);
        }
    }

    public void UpdateOnUIText()
    {
        _umbrellaAmountText.text = umbrellaAmount.ToString();
    }

    public void ReturnUmbrella()
    {
        ++umbrellaAmount;
        UpdateOnUIText();

        //inform player that the umbrella amount has increase
    }

}
