using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewSpeaker", menuName = "Data/New Speaker")]
[System.Serializable]
public class Speaker : ScriptableObject
{
    public string SpeakerName;
    public Color TextColor;
    public List<Sprite> sprites;
    public SpriteController prefab;
}
