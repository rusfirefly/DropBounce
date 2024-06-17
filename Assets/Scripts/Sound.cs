using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private HudHandler _hudHandler;

    private bool _isSoundOn;

    public void Initialize(bool isSound)
    {
        _isSoundOn = isSound;
        SetSoundVolume(isSound);
    }

    public void ChangeSoundVolume()
    {
        _isSoundOn = !_isSoundOn;

        _hudHandler.ChangeImageSound(_isSoundOn);
        SetSoundVolume(_isSoundOn);

        YandexGame.savesData.IsSound = _isSoundOn;
        YandexGame.SaveProgress();
        
    }
    
    private void SetSoundVolume(bool isSound)
    {
        float soundVolume = isSound ? soundVolume = 0 : soundVolume = -80;
        _audioMixer.SetFloat("Volume", soundVolume);
    }
}
