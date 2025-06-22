using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixturePanel : MonoBehaviour
{
    public bool isOpened;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenPanel()
    {
        isOpened = true;
        animator.SetBool("isOpened", isOpened);
    }

    public void ClosePanel()
    {
        isOpened = false;
        animator.SetBool("isOpened", isOpened);
    }


}
