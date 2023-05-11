using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryScene", menuName = "Data/New Story Scene")]
[System.Serializable]
public class StoryScene : GameScene
{
    public List<Sentence> Sentences;
    public Sprite BackGround;
    public GameScene NextScene;

    [System.Serializable]
    public struct Sentence
    {
        public string Text;
        public Speaker Speaker;
        public List<Action> Actions;

        public AudioClip Music;
        public AudioClip Sound;

        [System.Serializable]
        public struct Action
        {
            public Speaker Speaker;
            public int SpriteIndex;
            public Type ActionType;
            public Vector2 Coords;
            public float MoveSpeed;

            [System.Serializable]
            public enum Type
            {
                NONE,
                APPEAR,
                MOVE,
                DISAPPEAR
            }
        }
    }
}

public class GameScene : ScriptableObject
{

}
