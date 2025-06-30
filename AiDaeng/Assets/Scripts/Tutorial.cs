using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public List<GameObject> TutorialPages;
    int currentIndex = 0;
    GameObject currentPage;

    private void Start()
    {
        currentPage = TutorialPages[0];
        currentPage.GetComponent<Animator>().SetBool("isOpened", true);
    }

    IEnumerator ChangePage(int index)
    {
        // exit current page
        currentPage.GetComponent<Animator>().SetBool("isOpened", false);
        // delay time before enter new one
        yield return new WaitForSeconds(0.5f);
        // set current page to the new one
        currentPage = TutorialPages[index];
        // enter new one
        currentPage.GetComponent<Animator>().SetBool("isOpened", true);       
    }

    public void OnNextButton()
    {
        if (currentIndex >= TutorialPages.Count-1) return;
        currentIndex += 1;
        StartCoroutine(ChangePage(currentIndex));
    }
    public void OnPreviousButton()
    {
        if (currentIndex <= 0) return;
        currentIndex -= 1;
        StartCoroutine(ChangePage(currentIndex));
    }
}
