using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Tutorial : MonoBehaviour
{
    public List<GameObject> TutorialPages;    
    private VideoPlayer videoPlayer;
    int currentIndex = 0;
    GameObject currentPage;

    private void Awake()
    {
        videoPlayer = FindObjectOfType<VideoPlayer>();        
    }
    private void Start()
    {
        currentPage = TutorialPages[0];
        if (TutorialPages[0].GetComponent<TutorialPage>().videoClip != null)
        {
            videoPlayer.clip = TutorialPages[0].GetComponent<TutorialPage>().videoClip;
            videoPlayer.Play();
        }
        currentPage.SetActive(true);
    }

    void ChangePage(int index)
    {
        GameObject tempPreviousPage = currentPage;
        tempPreviousPage.SetActive(false);
        // exit current page
        //currentPage.SetActive(false);
        //// delay time before enter new one
        //yield return new WaitForSeconds(0.5f);
        //// set current page to the new one
        currentPage = TutorialPages[index];
        currentPage.SetActive(true);
        //// enter new one
        //currentPage.GetComponent<Animator>().SetBool("isOpened", true);
        if(TutorialPages[index].GetComponent<TutorialPage>().videoClip != null)
        {
            videoPlayer.clip = TutorialPages[index].GetComponent<TutorialPage>().videoClip;
            videoPlayer.Play();
        }
        
    }

    public void OnNextButton()
    {
        if (currentIndex >= TutorialPages.Count-1) return;
        currentIndex += 1;
        //StartCoroutine(ChangePage(currentIndex));
        ChangePage(currentIndex);
    }
    public void OnPreviousButton()
    {
        if (currentIndex <= 0) return;
        currentIndex -= 1;
        //StartCoroutine(ChangePage(currentIndex));
        ChangePage(currentIndex);
    }

    public void OnBackButton()
    {
        gameObject.SetActive(false);
    }
}
