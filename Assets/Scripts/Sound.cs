using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private HudHandler _hudHandler;

    [SerializeField] private Image _soundOnImage;
    [SerializeField] private Image _soundOffImage;


    private bool _isSoundOn;

    public void Initialize(float volume)
    {
        SetSoundVolume(volume);
    }

    public void ChangeSoundVolume()
    {
        _isSoundOn = !_isSoundOn;
        float volume = 0;

        if(_isSoundOn == false)
            volume = -80;

        YandexGame.savesData.IsSound = _isSoundOn;
        YandexGame.SaveProgress();
      
        SetSoundVolume(volume);
    }
    
    public void SetSoundVolume(float volume)
    {
        Debug.Log(volume);
        _audioMixer.SetFloat("Volume", volume);
    }
}
