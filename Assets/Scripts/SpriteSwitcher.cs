using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwitcher : MonoBehaviour
{
    public bool IsSwitched = false;
    public Image Image1;
    public Image Image2;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SwitchImages(Sprite sprite)
    {
        if (!IsSwitched)
        {
            Image2.sprite = sprite;
            _animator.SetTrigger("SwitchFirst");
        }
        else
        {
            Image1.sprite = sprite;
            _animator.SetTrigger("SwitchSecond");
        }

        IsSwitched = !IsSwitched;
    }

    public void SetImage(Sprite sprite)
    {
        if (!IsSwitched)
        {
            Image1.sprite = sprite;
        }
        else
        {
            Image2.sprite = sprite;
        }
    }

    public Sprite GetImage()
    {
        if (!IsSwitched)
        {
            return Image1.sprite;
        }
        else
        {
            return Image2.sprite;
        }
    }
}
