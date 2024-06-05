using UnityEngine;
using UnityEngine.Audio;
using YG;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    private bool _isSoundOn;

    public void Initialize(float volume)
    {
        SetSoundVolume(volume);
    }

    public void ChangeSoundVolume()
    {
        float volume = 1;
        if(_isSoundOn == false)
            volume = 0;
      
        YandexGame.savesData.IsSound = _isSoundOn;
        YandexGame.SaveProgress();
      
        SetSoundVolume(volume);
    }
    
    public void SetSoundVolume(float volume)
    {
        _audioMixer.SetFloat("Master", volume);
    }
}
