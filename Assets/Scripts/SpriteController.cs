using System.Collections;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    private SpriteSwitcher _switcher;
    private Animator _animator;
    private RectTransform _rect;

    private void Awake() 
    {
        _switcher = GetComponent<SpriteSwitcher>();
        _animator = GetComponent<Animator>();
        _rect = GetComponent<RectTransform>();    
    }

    public void Setup(Sprite sprite)
    {
        _switcher.SetImage(sprite);
    }

    public void Show(Vector2 coords)
    {
        _animator.SetTrigger("Show");
        _rect.localPosition = coords;
    }

    public void Hide()
    {
        _animator.SetTrigger("Hide");
    }

    public void Move(Vector2 coords, float speed)
    {
        StartCoroutine(MoveCoroutine(coords, speed));
    }

    private IEnumerator MoveCoroutine(Vector2 coords, float speed)
    {
        while (_rect.localPosition.x != coords.x || _rect.localPosition.y != coords.y)
        {
            _rect.localPosition = Vector2.MoveTowards(_rect.localPosition, coords, 
                Time. deltaTime * 1000f * speed);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void SwitchSprite(Sprite sprite)
    {
        if (_switcher.GetImage() != sprite)
        {
            _switcher.SwitchImages(sprite);
        }
    }
}
