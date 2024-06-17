using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private HudHandler _hudHandler;

    [SerializeField] private Sprite _soundOnImage;
    [SerializeField] private Sprite _soundOffImage;


    private bool _isSoundOn;

    public void Initialize(float volume)
    {
        SetSoundVolume(volume);
    }

    public void ChangeSoundVolume()
    {
        _isSoundOn = !_isSoundOn;
        Sprite image = _soundOnImage;
        float volume = 0;

        if (_isSoundOn == false)
        {
            volume = -80;
           image = _soundOffImage;
        }

        _hudHandler.SetImageSound(image);
        SetSoundVolume(volume);

        YandexGame.savesData.IsSound = _isSoundOn;
        YandexGame.SaveProgress();
        
    }
    
    public void SetSoundVolume(float volume)
    {
        _audioMixer.SetFloat("Volume", volume);
    }
}
