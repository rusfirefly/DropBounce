using UnityEngine;
using UnityEngine.Audio;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    

    public void SetSoundVolume(float volume)
    {
        _audioMixer.SetFloat("Master", volume);
    }

}
