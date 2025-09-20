using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SongkranTrivia", fileName = "New trivia")]
public class SongkranTrivia : ScriptableObject
{
    public string topicText;

    [TextArea(3, 10)]
    public string narrativeText;
    public Sprite sprite;
}
