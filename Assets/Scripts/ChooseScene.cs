using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewChooseScene", menuName = "Data/New Choose Scene")]
[System.Serializable]
public class ChooseScene : GameScene
{
    public List<ChooseLabel> Labels;

    [System.Serializable]
    public struct ChooseLabel
    {
        public string Text;
        public StoryScene NextScene;
    }
}
