using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;

    public void PlayAudio(AudioClip music, AudioClip sound)
    {
        if (sound != null)
        {
            _soundSource.clip = sound;
            _soundSource.Play();
        }

        if (music != null && _musicSource.clip != music)
        {
            StartCoroutine(SwitchMusic(music));
        }
    }

    private IEnumerator SwitchMusic(AudioClip music)
    {
        if (_musicSource.clip != null)
        {
            while(_musicSource.volume > 0)
            {
                _musicSource.volume -= 0.05f;
                yield return new WaitForSeconds(0.05f);
            }
        }
        else 
        {
            _musicSource.volume = 0;
        }

        _musicSource.clip = music;
        _musicSource.Play();

        while(_musicSource.volume < 0.5f)
            {
                _musicSource.volume += 0.05f;
                yield return new WaitForSeconds(0.05f);
            }
    }
}
