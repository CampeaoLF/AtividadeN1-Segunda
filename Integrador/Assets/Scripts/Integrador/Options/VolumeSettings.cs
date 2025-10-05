using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider musicaSlider;

    public void SetMusicVolume()
    {
        float volume = musicaSlider.value;
        mixer.SetFloat("musica", volume);
    }

}
