using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(menuName = "InstructorPage", fileName = "New instructor page")]

public class InstructorPage : ScriptableObject
{
    public string topicText;

    [TextArea(3,10)]
    public string narrativeText;
    public VideoClip videoClip;

    //[TextArea(3, 10)]
    //public string 

    //private void Awake()
    //{
    //    topicText.text = 
    //}
}
