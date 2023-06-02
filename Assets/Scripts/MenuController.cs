using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI MusicValue;
    public TextMeshProUGUI SoundsValue;

    [SerializeField] private int _chooseScene;
    private Animator _animator;
    private AudioMixer _musicMixer;
    private AudioMixer _soundsMixer;

    private void Start() 
    {
        _animator = GetComponent<Animator>();    
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideOptions();
        }
    }

    public void OnMusicChanged(float value)
    {
        MusicValue.SetText(value + "%");
    }

    public void OnSoundsChanged(float value)
    {
        SoundsValue.SetText(value + "%");
    }

    public void ShowOptions()
    {
        _animator.SetTrigger("ShowOptions");
    }

    private void HideOptions()
    {
        _animator.SetTrigger("HideOptions");
    }
    
    public void NewGame()
    {
        SceneManager.LoadScene(_chooseScene, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
