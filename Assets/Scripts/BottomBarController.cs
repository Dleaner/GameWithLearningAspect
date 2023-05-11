using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottomBarController : MonoBehaviour
{
    public TextMeshProUGUI BarText;
    public TextMeshProUGUI PersonName;
    public GameObject SpritesPrefab;
    
    private Dictionary<Speaker, SpriteController> Sprites;
    private State _state = State.COMPLETED;
    private StoryScene _currentScene;
    private int _sentenceIndex = -1;
    private Animator _animator;
    private bool _isHidden = false;

    private Coroutine _typingRoutine;
    private float _speedFactor = 1f;



    private enum State
    {
        PLAYING,
        SPEEDED_UP,
        COMPLETED
    }

    private void Awake() 
    {
        Sprites = new Dictionary<Speaker, SpriteController>();
        _animator = GetComponent<Animator>();
    }

    public int GetSentenceIndex()
    {
        return _sentenceIndex;
    }

    public void Hide()
    {
        if (!_isHidden)
        {
            _animator.SetTrigger("Hide");
            _isHidden = true;
        }
    }

    public void Show()
    {
        if (_isHidden)
        {
            _animator.SetTrigger("Show");
            _isHidden = false;
        }
    }

    public void ClearText()
    {
        BarText.text = "";
        PersonName.text = "";
    }

    public bool IsCompleted()
    {
        return _state == State.COMPLETED || _state == State.SPEEDED_UP;
    }

    public bool IsLastSentence()
    {
        return _sentenceIndex + 1 == _currentScene.Sentences.Count;
    }

    public void PlayScene(StoryScene scene)
    {
        _currentScene = scene;
        _sentenceIndex = -1;
        PlayNextSentence();
    }

    public void SpeedUp()
    {
        _state = State.SPEEDED_UP;
        _speedFactor = 0.25f;
    }

    public void StopTyping()
    {
        _state = State.COMPLETED;
        StopCoroutine(_typingRoutine);
    }

    public void PlayNextSentence() 
    {
        _speedFactor = 1f;
        _typingRoutine = StartCoroutine(TypeText(_currentScene.Sentences[++_sentenceIndex].Text));   
        PersonName.text = _currentScene.Sentences[_sentenceIndex].Speaker.SpeakerName;
        PersonName.color = _currentScene.Sentences[_sentenceIndex].Speaker.TextColor;
        ActionSpeakers();
    }

    private IEnumerator TypeText(string text)
    {
        BarText.text = "";
        _state = State.PLAYING;
        int wordIndex = 0;

        while (_state != State.COMPLETED)
        {
            BarText.text += text[wordIndex];
            yield return new WaitForSeconds(_speedFactor * 0.05f);
            if (++wordIndex == text.Length)
            {
                _state = State.COMPLETED;
                break;
            }
        }
    }

    private void ActionSpeakers()
    {
        List<StoryScene.Sentence.Action> actions = _currentScene.Sentences[_sentenceIndex].Actions;

        for (int i = 0; i < actions.Count; i++)
        {
            ActSpeaker(actions[i]);
        }
    }

    private void ActSpeaker(StoryScene.Sentence.Action action)
    {
        SpriteController controller = null;
        switch (action.ActionType)
        {
            case StoryScene.Sentence.Action.Type.APPEAR:
                if (!Sprites.ContainsKey(action.Speaker))
                {
                    controller = Instantiate(action.Speaker.prefab.gameObject, SpritesPrefab.transform).GetComponent<SpriteController>();
                    Sprites.Add(action.Speaker, controller);
                }
                else
                {
                    controller = Sprites[action.Speaker];
                }
                controller.Setup(action.Speaker.sprites[action.SpriteIndex]);
                controller.Show(action.Coords);
                return;
            case StoryScene.Sentence.Action.Type.MOVE:
                if (Sprites.ContainsKey(action.Speaker))
                {
                    controller = Sprites[action.Speaker];
                    controller.Move(action.Coords, action.MoveSpeed);
                }
                break;
            case StoryScene.Sentence.Action.Type.DISAPPEAR:
                if (Sprites.ContainsKey(action.Speaker))
                {
                    controller = Sprites[action.Speaker];
                    controller.Hide();
                }
                break;
            case StoryScene.Sentence.Action.Type.NONE:
                if (Sprites.ContainsKey(action.Speaker))
                {
                    controller = Sprites[action.Speaker];
                }
                break;
        }
        if (controller != null)
        {
            controller.SwitchSprite(action.Speaker.sprites[action.SpriteIndex]);
        }
    }
}
