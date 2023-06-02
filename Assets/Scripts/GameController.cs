using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameScene CurrentScene;
    
    [SerializeField] private BottomBarController _bottomBar;
    [SerializeField] private SpriteSwitcher _backGroundController;
    [SerializeField] private ChooseController _chooseController;
    [SerializeField] private AudioController _audioController;

    private State _state = State.IDLE;

    private enum State
    {
        IDLE,
        ANIMATE,
        CHOOSE
    }

    private void Start()
    {
        if (CurrentScene is StoryScene)
        {
            StoryScene storyScene = CurrentScene as StoryScene;
            _bottomBar.PlayScene(storyScene);
            _backGroundController.SetImage(storyScene.BackGround);
            PlayAudio(storyScene.Sentences[0]);
        }
    }

    private void Update() 
    {
        if (_state == State.IDLE) 
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
            {
                if (_bottomBar.IsCompleted())
                {
                    _bottomBar.StopTyping();
                    if (_bottomBar.IsLastSentence())
                    {
                        PlayScene((CurrentScene as StoryScene).NextScene);
                    }
                    else
                    {
                        _bottomBar.PlayNextSentence();
                        PlayAudio((CurrentScene as StoryScene).Sentences[_bottomBar.GetSentenceIndex()]);
                    }
                    
                }
                else
                {
                    //_bottomBar.SpeedUp();
                }
            }    
        }
    }

    public void PlayScene(GameScene scene)
    {
        StartCoroutine(SwitchScene(scene));
    }

    private IEnumerator SwitchScene(GameScene scene)
    {
        _state = State.ANIMATE;
        CurrentScene = scene;
        _bottomBar.Hide();
        yield return new WaitForSeconds(1f);
        _bottomBar.ClearText();

        if (scene is StoryScene)
        {
            StoryScene storyScene = scene as StoryScene;

            _backGroundController.SwitchImages(storyScene.BackGround);
            PlayAudio(storyScene.Sentences[0]);
            yield return new WaitForSeconds(1f);
            
            _bottomBar.Show();
            yield return new WaitForSeconds(1f);

            _bottomBar.PlayScene(storyScene);
            _state = State.IDLE;
        }
        else if (scene is ChooseScene)
        {
            _state = State.CHOOSE;
            _chooseController.SetupChoose(scene as ChooseScene);
        }
    }

    private void PlayAudio(StoryScene.Sentence sentence)
    {
        _audioController.PlayAudio(sentence.Music, sentence.Sound);
    }

}
